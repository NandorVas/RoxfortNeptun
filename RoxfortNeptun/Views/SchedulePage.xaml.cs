using RoxfortNeptun.ViewModels;

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
}