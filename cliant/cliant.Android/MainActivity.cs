using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Database;
using Android.Provider;
using Android.Content;
using ImageViewer.Droid.Services;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using System.IO;

namespace cliant.Droid
{
    [Activity(Label = "cliant", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());


        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            try
            {
                base.OnActivityResult(requestCode, resultCode, data);

                // ImageGalleryからの戻り値
                if (requestCode == ImageService.RESULT_PICK_IMAGEFILE && resultCode == Result.Ok)
                {
                    var file = this.GetSelectedItemPath(data);
                    if (ImageService.ImageSelectedHandler != null)
                    {
                        ImageService.ImageSelectedHandler(file);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private Stream GetSelectedItemPath(Intent data)
        {
            Android.Net.Uri uri = data.Data;
            return ContentResolver.OpenInputStream(uri);
        }
    }
}