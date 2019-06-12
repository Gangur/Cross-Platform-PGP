    using PGP.WorkPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PGP
{
    public class MasterPage : ContentPage
	{
        public ListView ListView { get { return listView; } }

        ListView listView;

        public MasterPage()
		{
            var masterPageItems = new List<MasterPageItem>();
            
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Файлы",
                IconSource = "contacts.png",
                TargetType = typeof(FilePage)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Текст",
                IconSource = "todo.png",
                TargetType = typeof(TabbedPage)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Параметры",
                IconSource = "reminders.png",
                TargetType = typeof(SettingsPage)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Справка",
                IconSource = "reminders.png",
                TargetType = typeof(HelpPage)
            });

            listView = new ListView
            {
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    var image = new Image();
                    image.SetBinding(Image.SourceProperty, "IconSource");
                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(image);
                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };

            Icon = "hamburger.png";
            Title = "PGP Шифрование";
            Padding = new Thickness(0, 40, 0, 0);
            Content = new StackLayout
            {
                Children = { listView}
            };
        }
	}
}