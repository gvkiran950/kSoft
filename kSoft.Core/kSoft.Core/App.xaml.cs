using kSoft.Core.Contracts.Repository;
using kSoft.Core.Contracts.Services;
using kSoft.Core.Repository;
using kSoft.Core.Services;
using kSoft.Core.Views;
using System;
using System.IO;
using Xamarin.Forms;
namespace kSoft.Core
{
    public partial class App : Application
    {
        static LocalDatabaseService localDatabaseService;

        public static LocalDatabaseService LocalDatabaseService
        {
            get
            {
                if (LocalDatabaseService == null)
                {
                    localDatabaseService = new LocalDatabaseService(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.db3"));
                }
                return localDatabaseService;
            }
        }
        public App()
        {
            InitializeComponent();
            DependencyService.Register<IKSoftRepository, KSoftRepository>();
            DependencyService.Register<IAuthenticationService, AuthenticationService>();
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
