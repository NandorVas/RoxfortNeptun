using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.ViewModels
{
    
    public partial class MainViewModel: ObservableObject
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [RelayCommand]
        private async Task Login()
        {
            if(Application.Current is App app)
            {
                app.SwitchToMainApp();
            }
        }

        [RelayCommand]
        private async Task ForgotPassword()
        {
            await Shell.Current.DisplayAlert("Csicska!",
                "Balfasz, szólj egy tanárnak.", "OK");
        }
    }
}
