using RoxfortNeptun.Models;
using RoxfortNeptun.Services;

namespace RoxfortNeptun
{
    public partial class App : Application
    {
        private readonly IDbContext _context;
        private readonly IAuthService _authService;
        private MainPage login;

        public App(MainPage mainPage, IDbContext context, IAuthService authService)
        {
            InitializeComponent();
            _context = context;
            _authService = authService;

            Task.Run(async () => await InitializeDatabase());

            MainPage = mainPage;
            

            login = mainPage;
        }

        private async Task InitializeDatabase()
        {
            var success = await _context.InitializeAsync();
            if (success)
            {
                System.Diagnostics.Debug.WriteLine("Database initialized successfully!");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Database initialization failed!");
            }
        }

        public void SwitchToMainApp()
        {
            MainPage = new AppShell();
        }

        public void SwitchToLogInApp()
        {
            MainPage = login;
        }
    }
}