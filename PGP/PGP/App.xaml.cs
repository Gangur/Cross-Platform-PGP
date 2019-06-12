using PGP.CoderPGP;
using PGP.WorkPages;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PGP
{
    public partial class App : Application
    {

        public App()
        {
            //InitializeComponent();
            
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private IncomingFile _incomingFile;

        public IncomingFile IncomingFile
        {
            get { return _incomingFile; }
            set
            {
                _incomingFile = value;
                MainPage = new FilePage(_incomingFile);
                //(MainPage as FilePage).SetIncomingFile(_incomingFile);
            }
        }
    }
}
