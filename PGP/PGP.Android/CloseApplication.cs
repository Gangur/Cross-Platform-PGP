using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PGP.PlatformSpecificInterface;
using Xamarin.Forms;

[assembly: Dependency(typeof(PGP.Droid.CloseApplication))]
namespace PGP.Droid
{
    public class CloseApplication :  ICloseApplication
    {
        [Obsolete]
        public void closeApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}