using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PGP
{
    class HelloPage : ContentPage
    {
        public HelloPage(bool Option)
        {
            BoxView boxTop = new BoxView
            {
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 5
            };

            Label label1 = new Label()
            {
                Text = Option ? "Отлично!" : "Добро пожаловать!",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };



            Label label2 = new Label()
            {
                Text = Option ? "Вы прошли PGP регистрацию ключей!" : "",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            BoxView LineCentr = new BoxView
            {
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Black,
                HeightRequest = 1
            };

            Label label3 = new Label()
            {
                Text = "Возможности: ",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Label label4 = new Label()
            {
                Text = "Текст - Откройте вкладку Текст и зашифруйте свои сообщения.",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Label label5 = new Label()
            {
                Text = "Файлы - запускайте файлы с помощью PGP и шифруйте свои файлы.",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            BoxView LineEnd = new BoxView
            {
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Black,
                HeightRequest = 1
            };

            Label label6 = new Label()
            {
                Text = "PGP - Компьютерная программа, также библиотека функций, позволяющая выполнять операции шифрования и цифровой подписи сообщений, файлов и другой информации, представленной в электронном виде, в том числе прозрачное шифрование данных на запоминающих устройствах, например, на жёстком диске.",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            StackLayout stackLayout = new StackLayout()
            {
                Padding = 15,
                Children = { boxTop, label1, label2, LineCentr, label3, label4, label5, LineEnd, label6 }
            };

            ScrollView scrollView = new ScrollView();
            scrollView.Content = stackLayout;

            Title = Option ? "Начало работы!" : "PGP шифрование";
            stackLayout.Spacing = 15;
            this.Content = scrollView;
        }
    }
}
