using System.IO;
using System.Net.Mime;
using System;
using Microsoft.UI.Xaml;

namespace Sokutatsu
{
    public sealed partial class MainWindow : Window
    {
        public string[] ContentTypes {get; set;}
        public string[] Methods {get; set;}

        public MainWindow()
        {
            InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            ContentTypes = new string[]{
                "application/json",
                "text/plain",
                "text/html",
                "text/javascript",
                "text/css",
                "application/pdf",
                "application/x-tar",
                "image/png",
                "image/jpeg",
                "image/gif",
                "image/bmp",
                "image/svg+xml",
                "video/mp4"
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

        async void ClickSend(object sender, RoutedEventArgs e){
            string contentType = ContentType.SelectedIndex == -1 ? "" : ContentTypes[ContentType.SelectedIndex];
            Client client = new(Uri.Text, Body.Text, contentType);
            string method = Method.SelectedIndex == -1 ? "" : Methods[Method.SelectedIndex];
            if(method == "GET"){
                ResponseBody.Text = await client.Get();
            }else if(method == "POST"){
                if(contentType == string.Empty)
                {
                    return;
                }
                ResponseBody.Text = await client.Post();
            }
        }

        void ChangeMethod(object sender, RoutedEventArgs e){
            string method = Method.SelectedIndex == -1 ? "" : Methods[Method.SelectedIndex];
            if(method == "GET"){
                OpenButton.IsEnabled = false;
                ContentType.IsEnabled = false;
                Body.IsEnabled = false;
            }else{
                OpenButton.IsEnabled = true;
                ContentType.IsEnabled = true;
                Body.IsEnabled = true;
            }
        }
    }
}
