using RoxfortNeptun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoxfortNeptun.Services;
using RoxfortNeptun.Models;

namespace RoxfortNeptun.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbContext _context;

        public bool IsAuthenticated { get; set; }

        public IUser CurrentUser
        {
            get
            {
                return CurrentUser;
            }
            private set
            {
                CurrentUser = value;
            }
        }

        public event EventHandler<AuthenticationStateChangedEventArgs> AuthenticationStateChanged;

        public AuthService(IDbContext context)
        {
            this._context = context;
        }

        public Task<LoginResult> LoginAsync(string neptunKod, string password)
        {
            try
            {
                if(string.IsNullOrEmpty(neptunKod) || neptunKod.Length < 6)
                {
                    return Task.FromResult(new LoginResult
                    {
                        Success = false,
                        Message = "Érvénytelen Neptun kód.",
                        User = null
                    });
                }
                else if(string.IsNullOrEmpty(password))
                {
                    return Task.FromResult(new LoginResult
                    {
                        Success = false,
                        Message = "A jelszó nem lehet üres.",
                        User = null
                    });
                }

                var stud = _context.GetByIdASync<Students>(neptunKod).Result;
                var teaacher = _context.GetByIdASync<Teachers>(neptunKod).Result;

                var user = stud ?? (IUser)teaacher;

                bool isFirtsLogin = string.IsNullOrEmpty(CurrentUser.Password);

                if (isFirtsLogin)
                {
                    CurrentUser.Password = password;

                    if(user.UserType == 0)
                    {
                        _context.UpdateAsync<Students>((Students)user).Wait();
                    }

                    _context.UpdateAsync<Teachers>((Teachers)user).Wait();

                    SethAuthState(true, user);
                    return Task.FromResult(new LoginResult
                    {
                        Success = true,
                        Message = "Első bejelentkezés sikeres. Kérem, jegyezze meg jelszavát.",
                        User = user
                    });
                }

                if(password == user.Password)
                {
                    SethAuthState(true, user);

                    return Task.FromResult(new LoginResult
                    {
                        Success = true,
                        Message = "Sikeres bejelentkezés.",
                        User = user
                    });
                }
                else
                {
                    return Task.FromResult(new LoginResult
                    {
                        Success = false,
                        Message = "Hibás jelszó vagy felhasználónév.",
                        User = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new LoginResult
                {
                    Success = false,
                    Message = ex.Message,
                    User = null
                });
            }
        }

        private void SethAuthState(bool v, IUser user)
        {
            IsAuthenticated = v;
            CurrentUser = user;

            AuthenticationStateChanged?.Invoke(this, new AuthenticationStateChangedEventArgs
            {
                IsAuthenticated = v,
                User = user,
            });
        }

        public void Logout()
        {
            SethAuthState(false, null);
        }
    }
}
