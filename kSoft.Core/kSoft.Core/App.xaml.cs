using kSoft.Core.Contracts.Repository;
using kSoft.Core.Contracts.Services;
using kSoft.Core.Repository;
using kSoft.Core.Services;
using kSoft.Core.Views;
using Xamarin.Forms;
namespace kSoft.Core
{
    public partial class App : Application
    {
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
