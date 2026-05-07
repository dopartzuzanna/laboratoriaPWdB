using Avalonia.Controls;
using lab9.ViewModels;
using lab9.Views;

namespace lab9
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void OnOpenHistoryClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                var hw = new HistoryWindow(vm);
                hw.Show(this);
            }
        }
    }
}