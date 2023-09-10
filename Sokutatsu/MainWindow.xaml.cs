using System.Threading;
using System.IO;
using System.Net.Mime;
using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace Sokutatsu
{
    public sealed partial class MainWindow : Window
    {
        public string[] ContentTypes { get; set; }
        public string[] Methods { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            ContentTypes = new string[]{
                "application/json",
                "text/plain",
                // "text/html",
                // "text/javascript",
                // "text/css",
                // "application/pdf",
                // "application/x-tar",
                // "image/png",
                // "image/jpeg",
                // "image/gif",
                // "image/bmp",
                // "image/svg+xml",
                // "video/mp4"
            };

            Methods = new string[]{
                "GET",
                "POST",
                // "PUT",
                // "DELETE",
                // "CONNECT",
                // "OPTIONS",
                // "TRACE",
                // "PATCH"
            };
        }

        void ClickExit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        async void ClickSend(object sender, RoutedEventArgs e)
        {
            if(Uri.Text == string.Empty){
                Dialog.ErrorDialog(this, "Uri がありません");
                return;
            }

            string contentType = ContentType.SelectedIndex == -1 ? "" : ContentTypes[ContentType.SelectedIndex];
            Client client = new(Uri.Text, Body.Text, contentType);
            string method = Method.SelectedIndex == -1 ? "" : Methods[Method.SelectedIndex];
            if(method == string.Empty){
                Dialog.ErrorDialog(this, "メソッドが選択されていません");
                return;
            }

            if (method == "GET")
            {
                Progress.Visibility = Visibility.Visible;
                ResponseBody.Text = await client.Get(this);
                Progress.Visibility = Visibility.Collapsed;
            }
            else if (method == "POST")
            {
                if (contentType == string.Empty)
                {
                    Dialog.ErrorDialog(this, "Content-Type が選択されていません");
                    return;
                }
                Progress.Visibility = Visibility.Visible;
                ResponseBody.Text = await client.Post(this);
                Progress.Visibility = Visibility.Collapsed;
            }
        }

        void ChangeMethod(object sender, RoutedEventArgs e)
        {
            string method = Method.SelectedIndex == -1 ? "" : Methods[Method.SelectedIndex];
            OpenButton.IsEnabled = ContentType.IsEnabled = Body.IsEnabled = (method != "GET");
        }
    }
}
