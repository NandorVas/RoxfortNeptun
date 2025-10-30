using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using RoxfortNeptun.ViewModels;

namespace RoxfortNeptun
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Register to receive the dialog message (title + content).
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<(string Title, string Content)>>(this, (r, msg) =>
            {
                var (title, content) = msg.Value;
                // Dispatch to UI thread and show alert
                Dispatcher.Dispatch(async () => await DisplayAlert(title, content, "OK"));
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Unregister to avoid duplicate handlers when the page reappears.
            WeakReferenceMessenger.Default.Unregister<ValueChangedMessage<(string Title, string Content)>>(this);
        }
    }
}
