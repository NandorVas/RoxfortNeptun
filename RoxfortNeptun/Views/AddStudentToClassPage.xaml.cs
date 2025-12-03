using System;
using Microsoft.Maui.Controls;
using RoxfortNeptun.Models;

namespace RoxfortNeptun.Views;

public partial class AddStudentToClassPage : ContentPage
{
    public AddStudentToClassPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var className = ClassNameEntry.Text?.Trim();
        var neptun = NeptunEntry.Text?.Trim()?.ToUpperInvariant();

        if (string.IsNullOrWhiteSpace(className))
        {
            await DisplayAlert("Validation", "Please enter the class name.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(neptun))
        {
            await DisplayAlert("Validation", "Please enter the student's Neptun code.", "OK");
            return;
        }

        try
        {
            var db = new DbContext();

            var classTask = await db.GetClassTaskByNameAsync(className);
            if (classTask == null)
            {
                await DisplayAlert("Not found", $"Class '{className}' not found.", "OK");
                return;
            }

            var student = await db.GetByIdASync<Students>(neptun);
            if (student == null)
            {
                await DisplayAlert("Not found", $"Student with Neptun '{neptun}' not found.", "OK");
                return;
            }

            var enrollment = new StudentClassTask(student.Id, classTask.Id);
            await db.InsertStudentClassTaskAsync(enrollment);

            await DisplayAlert("Success", "Student enrolled to class.", "OK");

            // close modal and return
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}