using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoxfortNeptun.Models;

namespace RoxfortNeptun.Services
{
    public interface IAuthService
    {
        Task<LoginResult> LoginAsync(string neptunKod, string password);
        void Logout();
        bool IsAuthenticated { get; }
        IUser CurrentUser { get; }
        event EventHandler<AuthenticationStateChangedEventArgs> AuthenticationStateChanged;
    }

    public class LoginResult
    { 
        public bool Success { get; set; } 
        public string Message { get; set; }
        public IUser User { get; set; }
    }

    public class AuthenticationStateChangedEventArgs : EventArgs
    {
        public bool IsAuthenticated { get; set; }
        public IUser User { get; set; }
    }

}
