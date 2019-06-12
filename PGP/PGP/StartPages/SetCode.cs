using PGP.CoderPGP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PGP
{
	public class SetCode : ContentPage
	{
        private Editor PublicTextEditor;
        private Editor PrivateTextEditor;
        private Entry EntryPass;
        private Entry EntryEmail;

        public SetCode ()
		{
            BoxView boxTop = new BoxView
            {
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 5
            };

            Label label1 = new Label()
            {
                Text = "Ваш Публичный Ключ:",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            PublicTextEditor = new Editor { HeightRequest = 200 };

            Label label2 = new Label()
            {
                Text = "Ваш Приветный Ключ:",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            PrivateTextEditor = new Editor { HeightRequest = 200 };

            BoxView LineCentr = new BoxView
            {
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Black,
                HeightRequest = 1
            };

            Label label3 = new Label()
            {
                Text = "Введите PGP пароль: (При наличии)",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            EntryPass = new Entry { Placeholder = "Pass" };

            Label label4 = new Label()
            {
                Text = "Введите PGP Email: (При наличии)",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            EntryEmail = new Entry { Placeholder = "Email" };

            Label label5 = new Label()
            {
                Text = "PGP пароль - это пароль к вашим PGP ключам, при генерации ключей как правило его указывают, если при генерации вы не указывали пароль, оставте поле пустым.",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Label label6 = new Label()
            {
                Text = "PGP Email - это ваши контактные данные к вашим ключам, а так же ваша подпись к зашифрованным данным, является не обязательной.",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Button SevaCode = new Button
            {
                Text = "Сохранить",
                BackgroundColor = Color.CadetBlue,
                VerticalOptions = LayoutOptions.End,
            };
            SevaCode.Clicked += SetKeys;

            StackLayout stackLayoutButtone = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Children = { SevaCode }
            };

            StackLayout stackLayout = new StackLayout()
            {
                Padding = 15,
                Children = { boxTop, label1, PublicTextEditor, label2, PrivateTextEditor, label3, EntryPass, label4, EntryEmail, LineCentr, label5, label6, stackLayoutButtone }
            };

            ScrollView scrollView = new ScrollView();
            scrollView.Content = stackLayout;

            Title = "Генерация PGP ключей";
            stackLayout.Spacing = 15;
            this.Content = scrollView;
        }

        async void SetKeys(object sender, EventArgs e)
        {
            GoPGP PGP = new GoPGP(EntryEmail.Text, EntryPass.Text, PublicTextEditor.Text, PrivateTextEditor.Text);
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