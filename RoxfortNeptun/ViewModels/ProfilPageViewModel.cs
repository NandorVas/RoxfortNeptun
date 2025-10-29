using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoxfortNeptun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoxfortNeptun.ViewModels
{
    public partial class ProfilPageViewModel: ObservableObject
    {
        private readonly IDbContext _database;

        [ObservableProperty]
        Students currentStudent;

        [ObservableProperty]
        public List<Students> allStudents;

        [ObservableProperty]
        public string databaseInfo;

        public ProfilPageViewModel(IDbContext database)
        {
            _database = database;
            LoadData();
        }

        private async void LoadData()
        {
            //await LoadCurrentStudent();
            //await LoadAllStudents();
            UpdateDatabaseInfo();
        }

        //private async Task LoadCurrentStudent()
        //{
        //    // Jelenlegi bejelentkezett hallgató

        //    if (CurrentStudent == null)
        //    {
        //        // Ha nincs bejelentkezve, betöltjük az elsőt demo célra
        //        var students = await _database.GetStudentsAsync();
        //        CurrentStudent = students.FirstOrDefault();
        //    }
        //}

        //private async Task LoadAllStudents()
        //{
        //    // Összes hallgató betöltése
        //    AllStudents = await _database.GetStudentsAsync();
        //}

        public void UpdateDatabaseInfo()
        {
            DatabaseInfo = $"Összes hallgató: {AllStudents?.Count ?? 0}";
        }

        [RelayCommand]
        public async Task RefreshData()
        {
            //await LoadAllStudents();
            UpdateDatabaseInfo();
        }

        [RelayCommand]
        public async Task ShowStudentDetails(Students student)
        {
            if (student != null)
            {
                await Shell.Current.DisplayAlert(
                    "Hallgató részletek",
                    $"Név: {student.Name}\n" +
                    $"Neptun: {student.NeptunKod}\n" +
                    $"Ház: {student.House}\n" +
                    $"Születés: {student.DateOfBirth:yyyy.MM.dd.}\n" +
                    $"Jelszó beállítva: {(string.IsNullOrEmpty(student.Password) ? "NEM" : "IGEN")}",
                    "OK");
            }
        }
    }
}
