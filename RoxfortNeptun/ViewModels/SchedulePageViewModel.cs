using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using RoxfortNeptun.Models;
using System.Collections.Generic;
using RoxfortNeptun.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace RoxfortNeptun.ViewModels
{
    public partial class SchedulePageViewModel : BaseViewModel
    {
        private readonly IDbContext _db;

        [ObservableProperty]
        public IEnumerable<ClassTask> classTasks;

        [ObservableProperty]
        private bool isTeacher;

        public SchedulePageViewModel(IAuthService auth, IDbContext db) : base(auth)
        {
            _db = db;
            ClassTasks = new List<ClassTask>();

            UpdateTeacherState();
            _authService.AuthenticationStateChanged += (_, __) => UpdateTeacherState();
        }

        private void UpdateTeacherState()
        {
            IsTeacher = _authService.CurrentUser?.UserType == UserType.Teacher;
        }

        public async Task LoadAsync()
        {
            ClassTasks = await _db.GetClassTasksAsync(_authService.CurrentUser.Id);
        }

    }
}
