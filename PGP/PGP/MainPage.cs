using PGP.CoderPGP;
using PGP.WorkPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PGP
{
    public class MainPage : MasterDetailPage
    {
        MasterPage masterPage;
        TabbedPage tabbedPage = new TabbedPage
        {
            Title = "Текст",
            Children = {
                    new TextPage(true),
                    new TextPage(false)
                }
        };
        public MainPage()
        {
            bool Option;
            try
            {
                GoPGP pgp = new GoPGP();
                Option = pgp.ValidationKey();
            }
            catch
            {
                Option = false;
            }
            
            masterPage = new MasterPage();
            Master = masterPage;
            Detail = Option ? new NavigationPage(new HelloPage(false)) : new NavigationPage(new StartPage());

            masterPage.ListView.ItemSelected += OnItemSelected;

            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage(item.TargetType.Name == "TabbedPage" ? tabbedPage : (Page)Activator.CreateInstance(item.TargetType));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }

    }
}