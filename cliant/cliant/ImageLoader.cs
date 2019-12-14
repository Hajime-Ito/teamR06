using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Plugin.Media.Abstractions;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO.Compression;

namespace cliant
{
    public class Image
    {  
        public Image(MemoryStream stream)
        {
            Stream = stream;
        }

        public MemoryStream Stream { get; set; }

        public ImageSource ImageSouce
        {
            get
            {
                return ImageSource.FromStream(() => { return Stream; }); 
            }
        }

        public string ToBase64String()
        {
            byte[] bytes = null;
            using (var memoryStream = new MemoryStream())
            {
                Stream.Position = 0;
                Stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
                Stream.Position = 0;
            }


           return Convert.ToBase64String(bytes);
        }

        public static Image FromBass64String(string str)
        {
            return new Image(new MemoryStream(Convert.FromBase64String(str)));
        }

        public string ToCompBase64String()
        {
            byte[] bytes = null;
            using (MemoryStream outStream = new MemoryStream())
            {
                using (GZipStream compressionStream = new GZipStream(outStream, CompressionMode.Compress))
                {
                    Stream.Position = 0;
                    Stream.CopyTo(compressionStream);
                    bytes = outStream.ToArray();
                    Stream.Position = 0;
                }
            }

            return Convert.ToBase64String(bytes);
        }

        public static Image FromCompBass64String(string str)
        {
            var input = new MemoryStream(Convert.FromBase64String(str));
            Image image = null;
            using (MemoryStream outStream = new MemoryStream())
            {
                using (GZipStream compressionStream = new GZipStream(outStream, CompressionMode.Decompress))
                {
                    input.CopyTo(compressionStream);
                    image = new Image(outStream);
                }
            }

            return image;
        }
    }

    public static class ImageLoader
    {
        public static void LoadImage(Action<Image> loadAction )
        {
            DependencyService.Get<ImageViewer.Services.IImageService>().ShowImageGallery((newimage) => {
                var stream = new MemoryStream();
                newimage.CopyTo(stream);
                loadAction(new Image(stream));
            });
        }
    }
}
