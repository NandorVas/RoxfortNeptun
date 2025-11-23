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
        Students CurrentStudent { get; }
        event EventHandler<AuthenticationStateChangedEventArgs> AuthenticationStateChanged;
    }

    public class LoginResult
    { 
        bool Success { get; set; } 
        string Message { get; set; }
        IUser User { get; set; }
    }

    public class AuthenticationStateChangedEventArgs : EventArgs
    {
        public bool IsAuthenticated { get; set; }
        public IUser User { get; set; }
    }

}
