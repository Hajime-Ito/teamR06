using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace cliant
{
    public class Image
    {  
        public Image(Stream stream)
        {
            Stream = stream;
        }

        public Stream Stream { get; }

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
                Stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

           return Convert.ToBase64String(bytes);
        }

        public static Image FromBass64String(string str)
        {
            return new Image(new MemoryStream(Convert.FromBase64String(str)));
        }
    }

    public static class ImageLoader
    {
        public static bool TryLoad(out Image image)
        {
            bool ret = false;
            var roadImage = new Image(null);
            DependencyService.Get<ImageViewer.Services.IImageService>().ShowImageGallery((newimage) => {
                ret = true;
                roadImage = new Image(newimage);
            });

            image = roadImage;
            return ret;
        }
    }
}
