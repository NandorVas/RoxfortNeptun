using System;
using Microsoft.Maui.Controls;
using RoxfortNeptun.Models;
using RoxfortNeptun.Services;

namespace RoxfortNeptun.Views;

public partial class AddClassPage : ContentPage
{
    private readonly IAuthService _auth;

    public AddClassPage(IAuthService auth)
    {
        InitializeComponent();
        _auth = auth;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Basic validation
        var name = NameEntry.Text?.Trim();
        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlert("Validation", "Please enter a name.", "OK");
            return;
        }

        if (!int.TryParse(NumOfStudsEntry.Text, out var numOfStuds))
        {
            await DisplayAlert("Validation", "Please enter a valid number of students.", "OK");
            return;
        }

        var place = PlaceEntry.Text?.Trim() ?? string.Empty;

        var isClass = IsClassSwitch.IsToggled;
        var startTime = StartTimePicker.Time;
        var endTime = EndTimePicker.Time;

        var newTask = new ClassTask
        {
            Name = name,
            NumOfStuds = numOfStuds,
            Place = place,
            Teacher = _auth.CurrentUser.Id,
            IsClass = isClass,
            StartTime = startTime,
            EndTime = endTime
        };

        try
        {
            var db = new DbContext();
            await db.InsertClassTaskAsync(newTask);

            await DisplayAlert("Success", "Class saved.", "OK");

            // Navigate back
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