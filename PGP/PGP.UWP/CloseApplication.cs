using PGP.PlatformSpecificInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Xamarin.Forms;

[assembly: Dependency(typeof(PGP.UWP.CloseApplication))]
namespace PGP.UWP
{
    class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            Windows.UI.Xaml.Application.Current.Exit();
        }
    }
}