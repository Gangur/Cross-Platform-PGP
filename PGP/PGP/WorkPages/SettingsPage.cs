using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PGP.WorkPages
{
    class SettingsPage : ContentPage
    {
        Label label2;
        Label label4;
        Label label6;
        public SettingsPage()
        {
            string WayDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string Pass = File.ReadAllText(WayDir + "\\Pass");
            string Email = File.ReadAllText(WayDir + "\\Email");
            BoxView boxTop = new BoxView
            {
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 5
            };

            Label label1 = new Label()
            {
                Text = "Ваш PGP пароль:",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            label2 = new Label()
            {
                Text = Pass == "" ? "Не задан" : Pass,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };
            
            Label label3 = new Label()
            {
                Text = "Ваш PGP Email:",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            label4 = new Label()
            {
                Text = Email == "" ? "Не задан" : Email,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };
            
            Button GetEmail = new Button
            {
                Text = "Скопировать email",
                BackgroundColor = Color.CadetBlue,
                VerticalOptions = LayoutOptions.Start,
            };
            GetEmail.Clicked += GetEmailAsync;

            BoxView LineCentr = new BoxView
            {
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Black,
                HeightRequest = 1
            };

            Label label5 = new Label()
            {
                Text = "Ваш публичный PGP ключ:",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            label6 = new Label()
            {
                Text = File.ReadAllText(WayDir + "\\public.gopgp"),
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Button GetPublic = new Button
            {
                Text = "Скопировать Публичный Ключ",
                BackgroundColor = Color.CadetBlue,
                VerticalOptions = LayoutOptions.Start,
            };
            GetPublic.Clicked += GetPublicAsync;

            BoxView LineEnd = new BoxView
            {
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Black,
                HeightRequest = 1
            };

            Button NewKeyGoStart = new Button
            {
                Text = "Сбросить ключи",
                BackgroundColor = Color.CadetBlue,
                VerticalOptions = LayoutOptions.EndAndExpand,
            };
            NewKeyGoStart.Clicked += NewKeyGoStartAsync;

            StackLayout stackLayout = new StackLayout()
            {
                Padding = 15,
                Children = { boxTop, label1, label2, label3, label4, GetEmail, LineCentr, label5, label6, GetPublic, LineEnd, NewKeyGoStart }
            };

            ScrollView scrollView = new ScrollView();
            scrollView.Content = stackLayout;

            Title = "Параметры";
            stackLayout.Spacing = 15;
            this.Content = scrollView;
        }

        async void GetEmailAsync(object sender, EventArgs e) => await Clipboard.SetTextAsync(label4.Text);
        async void GetPublicAsync(object sender, EventArgs e) => await Clipboard.SetTextAsync(label6.Text);

        async void NewKeyGoStartAsync(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("ВНИМАНИЕ!", "Вы увереный? При сбросе вы утратите возможность расшифровать всю зашифрованню информацию текущими ключами...", "Да", "Нет");
            if (result)
                await Navigation.PushAsync(new StartPage());
        }
    }
}
