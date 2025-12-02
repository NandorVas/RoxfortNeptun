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
        private ObservableCollection<ClassTask> tasks = new();

        public SchedulePageViewModel(IAuthService auth, IDbContext dbContext) : base(auth)
        {
            _dbContext = dbContext;
        }

        // Public loader you can call from the page
        public async Task LoadTasksAsync()
        {
            if (CurrentUser == null)
            {
                Tasks = new ObservableCollection<ClassTask>();
                return;
            }

            var list = await _dbContext.GetTasksForStudentAsync(CurrentUser.Id);
            Tasks = new ObservableCollection<ClassTask>(list);
        }

        // Optional command for pull-to-refresh or button
        [RelayCommand]
        private async Task Refresh() => await LoadTasksAsync();

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
