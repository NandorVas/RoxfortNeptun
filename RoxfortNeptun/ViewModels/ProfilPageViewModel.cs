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
    public partial class ProfilPageViewModel : BaseViewModel, IConnectivity
    {
        public IUser user => CurrentUser;

        public IEnumerable<ConnectionProfile> ConnectionProfiles => throw new NotImplementedException();

        [ObservableProperty]
        public NetworkAccess networkAccess;

        [ObservableProperty]
        private string connectionType;

        public ProfilPageViewModel(IAuthService auth) : base(auth)
        {
        }

        public event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;

        [RelayCommand]
        private async Task CheckConnectivity()
        {
            NetworkAccess = Connectivity.Current.NetworkAccess;
            WriteOutConnectivity();
        }

        private async void WriteOutConnectivity()
        {
            if(NetworkAccess == NetworkAccess.None)
            {
                ConnectionType = "Nincs internet kapcsolat";
            }
            else if(NetworkAccess == NetworkAccess.Unknown)
            {
                ConnectionType = "Ismeretlen kapcsolat";
            }
            else if(NetworkAccess == NetworkAccess.Local)
            {
                ConnectionType = "Helyi hálózati kapcsolat";
            }
            else if(NetworkAccess == NetworkAccess.ConstrainedInternet)
            {
                ConnectionType = "Korlátozott internet kapcsolat";
            }
            else if(NetworkAccess == NetworkAccess.Internet)
            {
                ConnectionType = "Internet kapcsolat";
            }
        }

        [RelayCommand]
        private async void LogOut()
        {
            if (Application.Current is App app)
            {
                app.SwitchToLogInApp();
                ConnectionType = string.Empty;
            }
        }
    }
}
