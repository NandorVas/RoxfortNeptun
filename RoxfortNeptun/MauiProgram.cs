using RoxfortNeptun.ViewModels;
using RoxfortNeptun.Views;
using Microsoft.Extensions.Logging;
using RoxfortNeptun.Models;
using RoxfortNeptun.Services;

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

            //Servicek regisztálása
            builder.Services.AddSingleton<IDbContext, DbContext>();
            builder.Services.AddSingleton<IDbService, DbService>();

            // ViewModel-ek regisztrálása
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<ProfilPageViewModel>();

            // Page-ek regisztrálása
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<ProfilPage>();
            builder.Services.AddSingleton<SchedulePage>();
            builder.Services.AddSingleton<TaskPage>();


            return builder.Build();
        }
    }
}
