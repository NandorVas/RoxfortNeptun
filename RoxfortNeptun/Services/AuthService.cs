using RoxfortNeptun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoxfortNeptun.Services;

namespace RoxfortNeptun.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbContext _context;

        public bool IsAuthenticated { get; set; }

        public IUser CurrentUser { get; private set; }

        public event EventHandler<AuthenticationStateChangedEventArgs> AuthenticationStateChanged;

        public AuthService(IDbContext context)
        {
            this._context = context;
        }

        public async Task<LoginResult> LoginAsync(string neptunKod, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(neptunKod) || neptunKod.Length < 6)
                    return new LoginResult { Success = false, Message = "Érvénytelen Neptun kód.", User = null };

                if (string.IsNullOrEmpty(password))
                    return new LoginResult { Success = false, Message = "A jelszó nem lehet üres.", User = null };

                var stud = await _context.GetByIdASync<Students>(neptunKod);
                var teacher = await _context.GetByIdASync<Teachers>(neptunKod);

                var user = stud ?? (IUser)teacher;
                if (user == null)
                    return new LoginResult { Success = false, Message = "Felhasználó nem található.", User = null };

                bool isFirstLogin = string.IsNullOrEmpty(user.Password);

                if (isFirstLogin)
                {
                    user.Password = password;
                    if (user.UserType == 0)
                        await _context.UpdateAsync<Students>((Students)user);
                    else
                        await _context.UpdateAsync<Teachers>((Teachers)user);

                    SethAuthState(true, user);
                    return new LoginResult { Success = true, Message = "Első bejelentkezés sikeres. Kérem, jegyezze meg jelszavát.", User = user };
                }

                if (password == user.Password)
                {
                    SethAuthState(true, user);
                    return new LoginResult { Success = true, Message = "Sikeres bejelentkezés.", User = user };
                }

                return new LoginResult { Success = false, Message = "Hibás jelszó vagy felhasználónév.", User = null };
            }
            catch (Exception ex)
            {
                return new LoginResult { Success = false, Message = ex.Message, User = null };
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
