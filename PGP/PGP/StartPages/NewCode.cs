using PGP.CoderPGP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PGP
{
	public class NewCode : ContentPage
	{
        private Entry EntryEmail;
        private Entry EntryPass;
        public NewCode ()
		{
            Title = "Генерация PGP ключей";

            BoxView boxTop = new BoxView
            {
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 5
            };

            Label label1 = new Label()
            {
                Text = "Ваш email: (необязательно)",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            EntryEmail = new Entry { Placeholder = "Email" };

            Label label2 = new Label()
            {
                Text = "Ваш пароль к приватному PGP ключу: (необязательно)",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            EntryPass = new Entry { Placeholder = "Pass" };

            BoxView LineCentr = new BoxView
            {
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Black,
                HeightRequest = 1
            };

            Label label3 = new Label()
            {
                Text = "Позже вы сможете просмотреть эти данные в настройках приложения",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };
            
            Button ToGenerate = new Button
            {
                Text = "Генерировать",
                BackgroundColor = Color.CadetBlue,
                VerticalOptions = LayoutOptions.End,
            };
            ToGenerate.Clicked += CreateKeys;

            StackLayout stackLayoutButtone = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Children = { ToGenerate }
            };

            StackLayout stackLayout = new StackLayout()
            {
                Padding = 15,
                Children = { boxTop, label1, EntryEmail, label2, EntryPass, LineCentr, label3, stackLayoutButtone }
            };

            ScrollView scrollView = new ScrollView();
            scrollView.Content = stackLayout;

            stackLayout.Spacing = 15;
            this.Content = scrollView;
        }

        async void CreateKeys(object sender, EventArgs e)
        {
            GoPGP PGP = new GoPGP(EntryEmail.Text, EntryPass.Text); 
            PGP.CreatKeys();
            if (PGP.ValidationKey())
            {
                await Navigation.PushAsync(new HelloPage(true));
            }
            else
            {
                await DisplayAlert("Уведомление", "Ошибка ключей!", "ОK");
            }
        }
    }
}