using System;
using RoxfortNeptun.ViewModels;
using RoxfortNeptun.Models;
using CommunityToolkit.Mvvm.Input;

namespace RoxfortNeptun.Views;

public partial class SchedulePage : ContentPage
{
    private readonly SchedulePageViewModel _viewModel;

    public SchedulePage(ViewModels.SchedulePageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this._viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!_viewModel.IsTeacher)
        {
            // clear selection for non-teachers
            TasksCollectionView.SelectedItem = null;
            return;
        }

        // Clear selection so the item can be selected again later
        TasksCollectionView.SelectedItem = null;
    }

    private async void OnNewClassClicked(object sender, EventArgs e)
    {
        // Navigate to AddClassPage (route must be registered in AppShell)
        await Shell.Current.GoToAsync("AddClassPage");
    }

    private async void OnAddStudentToClassClicked(object sender, EventArgs e)
    {
        // open modal page for adding student to a class
        await Shell.Current.GoToAsync("AddStudentToClassPage");
    }
}