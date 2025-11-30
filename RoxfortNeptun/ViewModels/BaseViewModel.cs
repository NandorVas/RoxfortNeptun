using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoxfortNeptun.Models;
using RoxfortNeptun.Services;
using Microsoft.Maui.Controls;

namespace RoxfortNeptun.ViewModels
{
    public partial class BaseViewModel: ObservableObject
    {
        protected readonly IAuthService _authService;

        [ObservableProperty]
        private IUser currentUser;

        [ObservableProperty]
        private bool isLoggedIn;

        [ObservableProperty]
        private string name;

        public BaseViewModel(IAuthService auth)
        {
            this._authService = auth;

            UpdateUserState();

            _authService.AuthenticationStateChanged += OnAuthStateChanged;
        }

        private void OnAuthStateChanged(object? sender, AuthenticationStateChangedEventArgs e)
        {
            UpdateUserState();
        }

        private void UpdateUserState()
        {
            CurrentUser = _authService.CurrentUser;
            IsLoggedIn = _authService.IsAuthenticated;
            Name = CurrentUser?.Name ?? "Vendég";
        }

        [RelayCommand]
        private async Task Logout()
        {
            _authService.Logout();

            // Use the App helper that switches MainPage back to the stored login page.
            if (Application.Current is App app)
            {
                app.SwitchToLogInApp();
                return;
            }

            // Fallback: if the app is running inside a Shell, navigate via Shell.
            if (Shell.Current != null)
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
        }
    }
}
