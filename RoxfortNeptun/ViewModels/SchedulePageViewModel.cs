using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using RoxfortNeptun.Models;
using RoxfortNeptun.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RoxfortNeptun.ViewModels
{
    public partial class SchedulePageViewModel : BaseViewModel
    {
        private readonly IDbContext _db;

        [ObservableProperty]
        public IEnumerable<ClassTask> classTasks;

        [ObservableProperty]
        private bool isTeacher;

        [ObservableProperty]
        ClassTask selectedItem;

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

        [RelayCommand]
        async Task Delete()
        {
            if(SelectedItem != null)
            {
                _db.DeleteClassTaskAsync(SelectedItem);
                SelectedItem = null;
                await Application.Current.MainPage.DisplayAlert("Hiba", "Task törölve. Kérem frissítsen az oldalra.", "OK");
            }

            await Application.Current.MainPage.DisplayAlert("Hiba", "Kérem válasszon ki először egy Task-ot.", "OK" );
        }

        [RelayCommand]
        public async Task Edit()
        {
            if (SelectedItem == null)
            {
                await Application.Current.MainPage.DisplayAlert("Hiba", "Kérem válasszon ki egy Task-ot szerkesztéshez.", "OK");
                return;
            }

            // Prompt for new values (minimal inline editor)
            var newName = await Application.Current.MainPage.DisplayPromptAsync("Szerkesztés", "Név:", initialValue: SelectedItem.Name);
            if (string.IsNullOrEmpty(newName))
            {
                // cancel edit if name empty
                return;
            }

            var newPlace = await Application.Current.MainPage.DisplayPromptAsync("Szerkesztés", "Helyszín:", initialValue: SelectedItem.Place);

            SelectedItem.Name = newName;
            if (!string.IsNullOrEmpty(newPlace))
            {
                SelectedItem.Place = newPlace;
            }

            await _db.UpdateClassTaskAsync(SelectedItem);
            await LoadAsync();
            await Application.Current.MainPage.DisplayAlert("OK", "Task frissítve.", "OK");
        }
    }
}
