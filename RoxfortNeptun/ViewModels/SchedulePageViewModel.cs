using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoxfortNeptun.Models;
using RoxfortNeptun.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RoxfortNeptun.ViewModels
{
    public partial class SchedulePageViewModel : BaseViewModel
    {
        private readonly IDbContext _dbContext;

        [ObservableProperty]
        private ObservableCollection<ClassTask> tasks;

        public SchedulePageViewModel(IAuthService auth, IDbContext dbContext) : base(auth)
        {
            _dbContext = dbContext;
            Tasks = new ObservableCollection<ClassTask>();
        }

        // Public loader you can call from the page
        public async Task LoadTasksAsync()
        {
            var Ids = _dbContext.GetTasksForStudentAsync(CurrentUser.Id); // ez sem fog majd kellenei.

        }


        [RelayCommand]
        private async void LogOut()
        {
            if (Application.Current is App app)
            {
                app.SwitchToLogInApp();
            }
        }
    }
}
