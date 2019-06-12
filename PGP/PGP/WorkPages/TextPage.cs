using PGP.CoderPGP;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PGP.WorkPages
{
    class TextPage : ContentPage
    {
        Editor TextEditor;
        Label EncryptedText;
        Label LabelEncryptedText;
        bool Option;

        public TextPage(bool Option)
        {
            this.Option = Option;
            Title = Option ? "Зашифровать" : "Расшифровать";

            BoxView boxTop = new BoxView
            {
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 5
            };

            Label LabelText = new Label()
            {
                Text = Option ? "Ввидите текст:" : "Ввидите зашифрованный текст:",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            TextEditor = new Editor { HeightRequest = 200 };

            BoxView LineCentr = new BoxView
            {
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Black,
                HeightRequest = 1
            };

            LabelEncryptedText = new Label()
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            EncryptedText = new Label()
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Button GetText = new Button
            {
                Text = Option ? "Зашифровать и скопировать" : "Расшифровать и скопировать",
                BackgroundColor = Color.CadetBlue,
                VerticalOptions = LayoutOptions.EndAndExpand,
            };
            GetText.Clicked += GetTextAsync;

            StackLayout stackLayout = new StackLayout()
            {
                Padding = 15,
                Children = { boxTop, LabelText, TextEditor, LineCentr, EncryptedText, GetText }
            };

            ScrollView scrollView = new ScrollView();
            scrollView.Content = stackLayout;

            stackLayout.Spacing = 15;
            this.Content = scrollView;
        }

        async void GetTextAsync(object sender, EventArgs e)
        {
            GoPGP GoPgp = new GoPGP();
            LabelEncryptedText.Text = "Результат:";

            string Result = "";
            try
            {
                Result = Option? GoPgp.EncodeText(TextEditor.Text) : GoPgp.DecryptText(TextEditor.Text);
                EncryptedText.Text = Option ? Result.Substring(0, 300) + "..." : Result;
                await Clipboard.SetTextAsync(Result);
            }
            catch
            {
                await DisplayAlert("Ошибка!", "Не коректный текст!", "ОK");
            }
        }
    }
}
