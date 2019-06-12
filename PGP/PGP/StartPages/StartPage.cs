using PGP.CoderPGP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PGP
{
	public class StartPage : ContentPage
    {
		public StartPage ()
		{
            Title = "Начало работы";

            BoxView boxTop = new BoxView
            {
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 20
            };

            Label label1 = new Label()
            {
                Text = "Добро пожаловать!",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center

            };

            Label label2 = new Label()
            {
                Text = "Выберите как начать",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            BoxView boxCentr = new BoxView
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                HeightRequest = 30
            };

            Button NewCode = new Button
            {
                Text = "У меня нет PGP ключей",
                VerticalOptions = LayoutOptions.End,
            };
            NewCode.Clicked += NewCodeTapAsync;

            Button SetCode = new Button
            {
                Text = "У меня есть свои PGP ключи",
                VerticalOptions = LayoutOptions.End
            };
            SetCode.Clicked += SetCodeTapAsync;

            StackLayout stackLayoutButtone = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Padding = 20,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Children = { NewCode, SetCode }
            };

            StackLayout stackLayout = new StackLayout()
            {
                Children = { boxTop, label1, label2, boxCentr, stackLayoutButtone },
            };

            ScrollView scrollView = new ScrollView();
            scrollView.Content = stackLayout;

            stackLayout.Spacing = 15;
            this.Content = scrollView;
        }

        private async void NewCodeTapAsync(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new NewCode());
        }

        private async void SetCodeTapAsync(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SetCode());
        }
    }
}