using RoxfortNeptun.ViewModels;

namespace RoxfortNeptun.Views;
public partial class ProfilPage : ContentPage
{
    // Let MAUI DI inject the VM (register it in MauiProgram), or pass an instance here.
    public ProfilPage(ProfilPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}