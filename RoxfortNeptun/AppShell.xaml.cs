using RoxfortNeptun.Views;


namespace RoxfortNeptun
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("ProfilPage", typeof(ProfilPage));
            Routing.RegisterRoute("SchedulePage", typeof(SchedulePage));
            Routing.RegisterRoute("TaskPage", typeof(TaskPage));
        }
    }
}
