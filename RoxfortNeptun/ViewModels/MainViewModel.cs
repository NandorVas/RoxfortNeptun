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

    public partial class MainViewModel : ObservableObject
    {
        private readonly AuthService authService;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;
        [ObservableProperty]
        private bool isLoggedIn;

        public MainViewModel(AuthService auth)
        {
            this.authService = auth;
            UpdateUserState();

            authService.AuthenticationStateChangedEvent += AuthServiceAuthenticationStateChangedEvent;
        }

        private void AuthServiceAuthenticationStateChangedEvent(object? sender, AuthenticationStateChangedEventArgs e)
        {
            UpdateUserState();
        }

        private void UpdateUserState()
        {

        }

        [RelayCommand]
        private async Task Login()
        {
            if (Application.Current is App app)
            {
                app.SwitchToMainApp();
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
