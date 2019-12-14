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
    }

    static class ImageLoader
    {
        static bool TryLoad(out Image image)
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
