using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.ViewModels
{
    public partial class TaskPageViewModel : ObservableObject
    {
        [RelayCommand]
        private async void LogOut()
        {
            if (Application.Current is App app)
            {
                app.SwitchToLogInApp();
            }
        }
    }
}
