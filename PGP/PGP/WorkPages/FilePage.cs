using PGP.CoderPGP;
using PGP.PlatformSpecificInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace PGP.WorkPages
{
    public class IncomingFile
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }

    public class FilePage : ContentPage
    {
        private string FileType;
        private string FileName;
        private string FilePath;

        private bool Option;
        public FilePage(IncomingFile IF)
        {
            FileType = IF.Type;
            FileName = IF.Name;
            FilePath = IF.Path;

            Option = FileType == "PGP";

            Title = "Файлы";
            Label LabelPage = new Label
            {
                Text = Option ? "Расшифровать файл:" : "Зашифровать файл:", 
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            BoxView LineCentr = new BoxView
            {
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Color.Black,
                HeightRequest = 1
            };

            Label FileNameLabel = new Label
            {
                Text = "Файл: \n" + IF.Name,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Label FileWayLabel = new Label
            {
                Text = "Расположение: \n" +IF.Path,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Label FileTypeLabel = new Label
            {
                Text = "Тип: \n" + IF.Type,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Label FileDateLabel = new Label
            {
                Text = "Создан: \n" + IF.DateCreated,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Button GetText = new Button
            {
                Text = Option ? "Расшифровать" : "Зашифровать",
                BackgroundColor = Color.CadetBlue,
                VerticalOptions = LayoutOptions.EndAndExpand,
            };

            GetText.Clicked += this.ProcessFile;

            StackLayout stackLayout = new StackLayout()
            {
                Padding = 15,
                Children = { LabelPage, LineCentr, FileNameLabel, FileWayLabel, FileTypeLabel, FileDateLabel, GetText }
            };

            ScrollView scrollView = new ScrollView();
            scrollView.Content = stackLayout;

            stackLayout.Spacing = 15;
            this.Content = scrollView;
        }

        void ProcessFile(object sender, EventArgs e)
        {
            GoPGP GoPgp = new GoPGP();

            IWorkWithFile WorkFile = DependencyService.Get<IWorkWithFile>();
            string WayHome = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            int index = FilePath.LastIndexOf('\\');
            string PathFolder = FilePath.Substring(0, index);
            Task.Run(() => WorkFile.MoveToAsync(FilePath, WayHome));
            Thread.Sleep(1500);
            WayHome = Option ? GoPgp.DecodeFile(WayHome + "\\" + FileName) : GoPgp.EncodeFile(WayHome + "\\" + FileName);
            Task.Run(() => WorkFile.MoveToAsync(WayHome, PathFolder));
            Thread.Sleep(1500);

            ICloseApplication closer = DependencyService.Get<ICloseApplication>();
            closer?.closeApplication();
        }
        public FilePage()
        {
            BoxView boxTop = new BoxView
            {
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 5
            };

            Label label1 = new Label()
            {
                Text = "Для шифрования файлов: ",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };



            Label label2 = new Label()
            {
                Text = "1.Выберите нужный файл в проводнике.",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Label label3 = new Label()
            {
                Text = "2.Кликните правой кнопкой мыши.",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Label label4 = new Label()
            {
                Text = "3.Откройте с помощью PGP.",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Label label5 = new Label()
            {
                Text = "4.В появившемся окне кликнете на кнопку Зашифровать.",
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
                Text = "Для расшифровки файлов:",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            Label label7 = new Label()
            {
                Text = "Просто откройте PGP файл и нажмите расшифровать.",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start
            };

            StackLayout stackLayout = new StackLayout()
            {
                Padding = 15,
                Children = { boxTop, label1, label2, label3, label4, label5, LineEnd, label6, label7 }
            };

            ScrollView scrollView = new ScrollView();
            scrollView.Content = stackLayout;

            Title = "Файлы";
            stackLayout.Spacing = 15;
            this.Content = scrollView;
        }
    }
}
