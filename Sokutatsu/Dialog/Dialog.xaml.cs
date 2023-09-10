using System.Net.Mime;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace Sokutatsu
{
    public partial class Dialog : Page
    {
        public Dialog(string text)
        {
            InitializeComponent();
            Text.Text = text;
        }

        async public static void ErrorDialog(MainWindow mainWindow, string Message){
            StackPanel title = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            FontIcon fontIcon = new FontIcon
            {
                Glyph = "\uEB90",
                FontFamily = new FontFamily("Segoe MDL2 Assets"),
                Margin = new Thickness(0, 2, 8, 0),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 161, 38, 13))
            };
            TextBlock dialogText = new TextBlock
            {
                Text = "Error",
            };
            title.Children.Add(fontIcon);
            title.Children.Add(dialogText);

            ContentDialog dialog = new ContentDialog
            {
                XamlRoot = mainWindow.Content.XamlRoot,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
                Title = title,
                PrimaryButtonText = "OK",
                // SecondaryButtonText = "Don't Save",
                // CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                Content = new Dialog(Message)
            };
            ContentDialogResult result = await dialog.ShowAsync();
        }
    }
}
