using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using RoxfortNeptun.Models;
using System.Collections.Generic;

namespace RoxfortNeptun.ViewModels
{
    public class SchedulePageViewModel : INotifyPropertyChanged
    {
        private readonly IDbContext _db;

        public ObservableCollection<ClassTask> ClassTasks { get; } = new();

        public SchedulePageViewModel(IDbContext db)
        {
            _db = db;
        }

        public async Task LoadAsync()
        {
            // Read current user Neptun code from secure storage
            var neptun = await SecureStorage.Default.GetAsync("CurrentUserNeptun");
            if (string.IsNullOrWhiteSpace(neptun))
            {
                // no current user stored
                return;
            }

            var student = await _db.GetByIdASync<Students>(neptun);
            if (student == null)
            {
                return;
            }

            var tasks = await _db.GetClassTasksForStudentByJoinAsync(student.Id);
            ClassTasks.Clear();
            foreach (var t in tasks)
            {
                ClassTasks.Add(t);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
