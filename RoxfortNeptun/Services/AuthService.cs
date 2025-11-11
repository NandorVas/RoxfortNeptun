using RoxfortNeptun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.Services
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Students Student { get; set; }
    }

    public class AuthenticationStateChangedEventArgs : EventArgs
    {
        public bool IsAuthenticated { get; set; }
        public Students CurrentStudent { get; set; }
    }

    public class AuthService
    {
        private readonly IDbContext context;

        public bool IsAuthenticated { get; set; }
        public Students Student { get; set; }

        public event EventHandler<AuthenticationStateChangedEventArgs> AuthenticationStateChangedEvent;


        public AuthService(IDbContext context)
        {
             this.context = context;
        }

        public async Task<LoginResult> LoginAsync(string neptunKod, string password)
        {
            try
            {
                // Validáció
                if (string.IsNullOrWhiteSpace(neptunKod) || neptunKod.Length != 6)
                {
                    return new LoginResult { Success = false, Message = "A Neptun kód 6 karakter hosszú kell legyen!" };
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    return new LoginResult { Success = false, Message = "A jelszó megadása kötelező!" };
                }

                // Hallgató keresése
                var student = context.GetByIdASync<Students>(neptunKod).Result;

                if (student == null)
                {
                    return new LoginResult { Success = false, Message = "Nem található ilyen Neptun kódú hallgató!" };
                }

                // Első bejelentkezés ellenőrzése
                bool isFirstLogin = string.IsNullOrEmpty(student.Password);

                if (isFirstLogin)
                {
                    // Első bejelentkezés - jelszó beállítása
                    student.Password = password;
                    await context.UpdateAsync(student);

                    SetAuthenticationState(true, student);
                    return new LoginResult { Success = true, Message = "Sikeres első bejelentkezés!", Student = student };
                }

                // Normál bejelentkezés
                if (password == student.Password)
                {
                    SetAuthenticationState(true, student);
                    return new LoginResult { Success = true, Message = "Sikeres bejelentkezés!", Student = student };
                }
                else
                {
                    return new LoginResult { Success = false, Message = "Hibás jelszó!" };
                }
            }
            catch (Exception ex)
            {
                return new LoginResult { Success = false, Message = $"Bejelentkezési hiba: {ex.Message}" };
            }
        }

        public void Logout()
        {
            SetAuthenticationState(false, null);
        }

        private void SetAuthenticationState(bool isAuthenticated, Students student)
        {
            IsAuthenticated = isAuthenticated;
            this.Student = student;

            // Esemény kiváltása minden változásnál
            AuthenticationStateChangedEvent?.Invoke(this, new AuthenticationStateChangedEventArgs
            {
                IsAuthenticated = isAuthenticated,
                CurrentStudent = student
            });
        }
    }
}
