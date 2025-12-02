using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using RoxfortNeptun.Models;
using System.Collections.Generic;
using RoxfortNeptun.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RoxfortNeptun.ViewModels
{
    public partial class SchedulePageViewModel : BaseViewModel
    {
        private readonly IDbContext _db;

        [ObservableProperty]
        public IEnumerable<ClassTask> classTasks;

        public SchedulePageViewModel(IAuthService auth, IDbContext db) : base(auth)
        {
            _db = db;
            ClassTasks = new List<ClassTask>();
        }

        public async Task LoadAsync()
        {
            ClassTasks = await _db.GetClassTasksAsync(CurrentUser.Id);
        }
    }
}
