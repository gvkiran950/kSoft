﻿using kSoft.Core.Views;
using Xamarin.Forms;
namespace kSoft.Core
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           // AppResources.Culture = CrossMultilingual.Current.DeviceCultureInfo;
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
