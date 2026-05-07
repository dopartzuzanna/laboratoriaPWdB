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
        private int _currentPage = 1;
        private const int PageSize = 10;
        private TextBox _nameFilterBox;
        private TextBox _dateFilterBox;
        private TextBlock _pageInfo;

        public HistoryWindow(MainWindowViewModel mainVm)
        {
            _mainVm = mainVm;
            Width = 600;
            Height = 400;
            Title = "Historia wniosków";
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var root = new StackPanel { Margin = new Thickness(12), Spacing = 8 };

            var filterPanel = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8 };
            _nameFilterBox = new TextBox { Width = 200, Watermark = "Filtruj po nazwisku..." };
            _dateFilterBox = new TextBox { Width = 140, Watermark = "Filtruj po dacie..." };
            var applyFilter = new Button { Content = "Filtruj" };
            applyFilter.Click += (_, __) => { _currentPage = 1; RefreshList(); };
            filterPanel.Children.Add(_nameFilterBox);
            filterPanel.Children.Add(_dateFilterBox);
            filterPanel.Children.Add(applyFilter);
            root.Children.Add(filterPanel);

            _listBox = new ListBox { Height = 240, Width = 560 };
            root.Children.Add(_listBox);

            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Spacing = 8 };
            var load = new Button { Content = "Wczytaj" };
            var edit = new Button { Content = "Edytuj" };
            var delete = new Button { Content = "Usuń" };
            var undo = new Button { Content = "Cofnij" };
            var close = new Button { Content = "Zamknij" };
            var clearAll = new Button { Content = "Wyczyść wszystko" };

            var paging = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center, Spacing = 8 };
            var prev = new Button { Content = "Poprzednia" };
            var next = new Button { Content = "Następna" };
            _pageInfo = new TextBlock { VerticalAlignment = VerticalAlignment.Center };
            paging.Children.Add(prev);
            paging.Children.Add(_pageInfo);
            paging.Children.Add(next);
            // <-- usunięto dopisek: root.Children.Add(paging);  (był duplikat)

            prev.Click += (s, e) =>
            {
                if (_currentPage > 1)
                {
                    _currentPage--;
                    RefreshList();
                }
            };

            next.Click += (s, e) =>
            {
                // check if there is next page
                var total = _mainVm.GetHistoryCount(_nameFilterBox.Text, _dateFilterBox.Text);
                var maxPage = (total + PageSize - 1) / PageSize;
                if (_currentPage < maxPage)
                {
                    _currentPage++;
                    RefreshList();
                }
            };

            load.Click += (s, e) =>
            {
                if (_listBox.SelectedItem is WniosekModel selected)
                {
                    _mainVm.LoadFromHistory(selected);
                }
            };

            edit.Click += (s, e) =>
            {
                if (_listBox.SelectedItem is WniosekModel selected)
                {
                    _mainVm.LoadFromHistory(selected);
                    Close(); // close history so user can edit in main window
                }
            };

            delete.Click += async (s, e) =>
            {
                if (_listBox.SelectedItem is WniosekModel selected)
                {
                    var dlg = new ConfirmDialogNoXaml("Usunąć wybrany wpis?", "Potwierdź usunięcie");
                    var result = await dlg.ShowDialog<bool?>(this);
                    if (result == true)
                    {
                        _mainVm.DeleteFromHistory(selected);
                        RefreshList();
                    }
                }
            };

            undo.Click += (s, e) =>
            {
                _mainVm.UndoDelete();
                RefreshList();
            };

            clearAll.Click += async (s, e) =>
            {
                var dlg = new ConfirmDialogNoXaml("Czy na pewno wyczyścić całą historię?", "Potwierdź czyszczenie");
                var result = await dlg.ShowDialog<bool?>(this);
                if (result == true)
                {
                    _mainVm.ClearHistory();
                    _currentPage = 1;
                    RefreshList();
                }
            };

            close.Click += (s, e) => Close();

            buttons.Children.Add(load);
            buttons.Children.Add(edit);
            buttons.Children.Add(delete);
            buttons.Children.Add(undo);
            buttons.Children.Add(clearAll);
            buttons.Children.Add(close);

            root.Children.Add(paging); // jedno poprawne dodanie
            root.Children.Add(buttons);
            Content = root;

            RefreshList();
        }

        private void RefreshList()
        {
            var items = _mainVm.GetHistoryPage(_currentPage, PageSize, _nameFilterBox.Text, _dateFilterBox.Text);
            _listBox.ItemsSource = items;
            var total = _mainVm.GetHistoryCount(_nameFilterBox.Text, _dateFilterBox.Text);
            var maxPage = (total + PageSize - 1) / PageSize;
            if (maxPage == 0) maxPage = 1;
            _pageInfo.Text = $"Strona {_currentPage} / {maxPage} (łącznie {total})";
        }
    }
}
