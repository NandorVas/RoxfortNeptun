using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using RoxfortNeptun.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.ViewModels
{

    public partial class MainViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isLogginIn;

        public MainViewModel(IAuthService auth) : base(auth)
        {
            this._authService = auth;
        }

        [RelayCommand]
        private async Task Login()
        {
            this.Username = Username;
            this.Password = Password;

            if (IsLogginIn) return;
           
            IsLogginIn = true;

            try
            {
                if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    await App.Current.MainPage.DisplayAlert("Hiba", "Kérem töltse ki az összes mezőt!", "OK");

                    return;
                }

                var result = await _authService.LoginAsync(Username, Password);

                if (result.Success)
                {
                    if(Application.Current is App app)
                    {
                        app.SwitchToMainApp();
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Hiba", result.Message, "OK");
                }

            }
            finally
            {
                IsLogginIn = false;
            }
        }

        [RelayCommand]
        private async Task ForgotPassword()
        {
            WeakReferenceMessenger.Default.Send(
                 new ValueChangedMessage<(string Title, string Content)>(("Csicska!", "Balfasz, szólj egy tanárnak.")));
        }
    }
}
