using RoxfortNeptun.ViewModels;
using RoxfortNeptun.Models;

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
}