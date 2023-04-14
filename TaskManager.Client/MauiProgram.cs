using Microsoft.Extensions.Logging;
using TaskManager.Client.pages;
using TaskManager.Client.Services;
using TaskManager.Client.ViewModel;

namespace TaskManager.Client;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        var httpClientHandler = new HttpClientHandler();
        httpClientHandler.ServerCertificateCustomValidationCallback = 
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        
        builder.Services.AddScoped(cl=>new HttpClient(httpClientHandler)
        {
            BaseAddress = new Uri("https://192.168.0.122:7134")
        }  );
        
        builder.Services.AddSingleton<AuthService>();
        builder.Services.AddSingleton<HttpService>();
        

#if DEBUG
        builder.Logging.AddDebug();
#endif

        
        
        //views
        builder.Services.AddSingleton<EnterPage>();
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<CreatePages>();
        builder.Services.AddSingleton<Tasks>();
        
        
        //vm
        builder.Services.AddSingleton<EnterViewModel>();
        builder.Services.AddSingleton<CreatePageViewModel>();
        builder.Services.AddSingleton<TaskViewModel>();
        return builder.Build();
    }
}