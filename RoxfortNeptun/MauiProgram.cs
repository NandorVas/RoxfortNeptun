using RoxfortNeptun.ViewModels;
using RoxfortNeptun.Views;
using Microsoft.Extensions.Logging;

namespace RoxfortNeptun
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // ViewModel-ek regisztrálása
            builder.Services.AddSingleton<MainViewModel>();

            // Page-ek regisztrálása
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<FirstPage>();


            return builder.Build();
        }
    }
}
