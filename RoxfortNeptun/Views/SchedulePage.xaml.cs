namespace RoxfortNeptun.Views;

public partial class SchedulePage : ContentPage
{
    public SchedulePage(ViewModels.SchedulePageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}