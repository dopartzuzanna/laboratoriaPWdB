using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using System.Collections.ObjectModel;
using lab9.ViewModels;
using lab9.Models;

namespace lab9.Views
{
    public class HistoryWindow : Window
    {
        private ListBox _listBox;
        private MainWindowViewModel _mainVm;

        public HistoryWindow(MainWindowViewModel mainVm)
        {
            _mainVm = mainVm;
            Width = 600;
            Height = 400;
            Title = "Historia wniosków";
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var root = new StackPanel { Margin = new Thickness(12), Spacing = 8 };

            _listBox = new ListBox { Height = 280 };
            _listBox.ItemsSource = mainVm.GetHistoryItems();

            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Spacing = 8 };
            var load = new Button { Content = "Wczytaj" };
            var close = new Button { Content = "Zamknij" };

            load.Click += (s, e) =>
            {
                if (_listBox.SelectedItem is WniosekModel selected)
                {
                    _mainVm.LoadFromHistory(selected);
                }
            };

            close.Click += (s, e) => Close();

            buttons.Children.Add(load);
            buttons.Children.Add(close);

            root.Children.Add(_listBox);
            root.Children.Add(buttons);

            Content = root;
        }
    }
}
