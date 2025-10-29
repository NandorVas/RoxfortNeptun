namespace RoxfortNeptun
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();
            MainPage = mainPage;
        }

        public void SwitchToMainApp()
        {
            MainPage = new AppShell();
        }
    }
}