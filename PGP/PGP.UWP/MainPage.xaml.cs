using PGP.WorkPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace PGP.UWP
{
    public sealed partial class MainPage
    {
        PGP.App _app;
        public MainPage()
        {
            
            _app = new PGP.App();
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size { Height = 700, Width = 420 };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(200, 100));
            

            this.LoadApplication(_app);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            StorageFile storageFile = e.Parameter as StorageFile;
            if (storageFile != null)
            {
                string content;
                try
                {
                    content = await FileIO.ReadTextAsync(storageFile, UnicodeEncoding.Utf8);
                }
                catch
                {
                    content = await FileIO.ReadTextAsync(storageFile, UnicodeEncoding.Utf16BE);
                }

                var incomingFile = new IncomingFile
                {
                    Path = storageFile.Path,
                    Name = storageFile.Name,
                    DateCreated = storageFile.DateCreated.DateTime,
                    Type = storageFile.DisplayType,
                    Content = content
                };
                _app.IncomingFile = incomingFile;
            }
            base.OnNavigatedTo(e);
        }
    }
}
