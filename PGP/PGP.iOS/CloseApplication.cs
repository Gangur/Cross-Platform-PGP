using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Foundation;
using PGP.PlatformSpecificInterface;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PGP.iOS.CloseApplication))]
namespace PGP.iOS
{
    class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}