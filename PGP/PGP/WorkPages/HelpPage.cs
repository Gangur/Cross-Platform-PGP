using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PGP.WorkPages
{
    class HelpPage : ContentPage
    {
        public HelpPage()
        {
            Title = "Справка";
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "Здесь справка",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
        }
    }
}
