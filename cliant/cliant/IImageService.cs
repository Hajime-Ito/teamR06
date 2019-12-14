
using System;
using Xamarin.Forms;
using System.IO;
namespace ImageViewer.Services
{
    public interface IImageService
    {
        void ShowImageGallery(Action<Stream> imageSelectedHandler);
    }
}