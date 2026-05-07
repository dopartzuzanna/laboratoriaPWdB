using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace lab9.Views
{
    public class ConfirmDialogNoXaml : Window
    {
        public ConfirmDialogNoXaml(string message, string title = "Potwierdzenie")
        {
            Title = title;
            Width = 420;
            Height = 160;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var text = new TextBlock
            {
                Text = message,
                Margin = new Thickness(12),
                VerticalAlignment = VerticalAlignment.Center
            };

            var yes = new Button { Content = "Tak", Width = 80 };
            var no = new Button { Content = "Nie", Width = 80 };

            yes.Click += (_, __) => Close(true);
            no.Click += (_, __) => Close(false);

            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center, Spacing = 12 };
            buttons.Children.Add(yes);
            buttons.Children.Add(no);

            var root = new StackPanel { Spacing = 8 };
            root.Children.Add(text);
            root.Children.Add(buttons);

            Content = root;
        }
    }
}
