﻿using Microsoft.UI.Xaml;

namespace Sokutatsu
{
    public partial class App : Application
    {
        private Window? mainWindow;
        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            mainWindow = new MainWindow();
            mainWindow.Activate();
        }
    }
}
