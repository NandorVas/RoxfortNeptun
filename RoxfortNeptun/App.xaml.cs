using RoxfortNeptun.Models;

namespace RoxfortNeptun
{
    public partial class App : Application
    {
        private readonly IDbContext _context;
        private MainPage login;

        public App(MainPage mainPage, IDbContext context)
        {
            InitializeComponent();
            _context = context;

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