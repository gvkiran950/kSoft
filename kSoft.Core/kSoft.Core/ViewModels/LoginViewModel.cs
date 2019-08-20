using kSoft.Core.Contracts.Services;
using kSoft.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace kSoft.Core.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private string _language = string.Empty;
        private bool _isBusy = false;

        private IAuthenticationService _authenticationService = DependencyService.Get<IAuthenticationService>();
        public Command ChangeLanguageCommand { get; }
        public Command LoginCommand { get; }
        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login(), () => !IsBusy);
            ChangeLanguageCommand = new Command(async () => await ChangeLanguage(), () => !IsBusy);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public new bool IsBusy
        {
            get => _isBusy;
            set
            {
                SetProperty(ref _isBusy, value);
                LoginCommand.ChangeCanExecute();
                ChangeLanguageCommand.ChangeCanExecute();
            }
        }
        public string Language
        {
            get => _language;
            set => SetProperty(ref _language, value);
        }

        async Task Login()
        {
            IsBusy = true;
            var content = await _authenticationService.Authenticate(_userName, _password);
            await Task.Delay(4000);
            IsBusy = false;
            await Application.Current.MainPage.DisplayAlert("Login", "Login Sucessfull", "Ok");
        }

        async Task ChangeLanguage()
        {
            IsBusy = true;
            await Task.Delay(4000);
            _language = "English";
            IsBusy = false;
        }
    }
}
