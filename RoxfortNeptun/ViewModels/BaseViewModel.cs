using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoxfortNeptun.Models;
using RoxfortNeptun.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Name = CurrentUser.Name ?? "Vendég";
        }

        [RelayCommand]
        private async Task Logout()
        {
            _authService.Logout();
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
