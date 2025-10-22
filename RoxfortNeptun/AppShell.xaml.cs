using RoxfortNeptun.Views;


namespace RoxfortNeptun
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("FirstPage", typeof(FirstPage));
        }
    }
}
