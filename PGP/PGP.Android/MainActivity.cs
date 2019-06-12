using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PGP.WorkPages;
using System.IO;
using Android.Content;

namespace PGP.Droid
{
    
    //[Activity(Label = "PGP.Droid", Icon = "@mipmap/icon", Theme = "@style/MainTheme",  MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [Activity(Label = "PGP.Droid", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    //[IntentFilter(new[] { Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = @"application/pdf")]
    
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var application = new App();

            string action = Intent.Action;
            string type = Intent.Type;

            if (action.Equals(action) && !String.IsNullOrEmpty(type))
            {
                Android.Net.Uri fileUri = Intent.Data;

                string fileContent = File.ReadAllText(fileUri.Path);
                string fileName = fileUri.LastPathSegment;

                var incomingFile = new IncomingFile { Name = fileName, Content = fileContent, Path = fileUri.Path, Type = "", DateCreated = new DateTime() };
                application.IncomingFile = incomingFile;
            }

            LoadApplication(application);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}