using CommunityToolkit.Maui;

using Maui_Developer_Sample.Pages.AppCapability;
using Maui_Developer_Sample.Pages.AppCapability.Services;
using Maui_Developer_Sample.Pages.Sensors.Services;
using Maui_Developer_Sample.Pages.UI;
using Maui_Developer_Sample.Pages.UI.ViewModels;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Maui_Developer_Sample;

public static class MauiProgram
{
    public static IServiceProvider? Services { get; private set; }

    public static IConfiguration? Configuration { get; private set; }
    
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
               .UseMauiCommunityToolkit()
               .ConfigureServices()
               .ConfigurePages()
               .ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
               });

        var app = builder.Build();
        Services = app.Services;
        Configuration = app.Configuration;
        return app;
    }

    public static MauiAppBuilder ConfigureServices(this MauiAppBuilder mauiAppBuilder)
    {
        // Sensors
        mauiAppBuilder.Services.AddSingleton<Accelerometer_Service>();
        mauiAppBuilder.Services.AddSingleton<Gyroscope_Service>();
        mauiAppBuilder.Services.AddSingleton<Magnetometer_Service>();
        mauiAppBuilder.Services.AddSingleton<OrientationSensor_Service>();
        mauiAppBuilder.Services.AddSingleton<Compass_Service>();
        mauiAppBuilder.Services.AddSingleton<Barometer_Service>();

        // App Capabilities
        mauiAppBuilder.Services.AddSingleton<HapticFeedback_Service>();
        mauiAppBuilder.Services.AddSingleton<Vibration_Service>();
        
        // UI
        mauiAppBuilder.Services.AddSingleton<DrawArc_ViewModel>();

        // Logging
        mauiAppBuilder.Services.AddLogging(logging => logging.SetMinimumLevel(LogLevel.Debug));

        return mauiAppBuilder;
    }

    public static MauiAppBuilder ConfigurePages(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<Pages.MainPage>();

        // Sensors
        mauiAppBuilder.Services.AddTransient<Pages.Sensors.Accelerometer_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.Sensors.Gyroscope_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.Sensors.Magnetometer_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.Sensors.OrientationSensor_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.Sensors.Compass_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.Sensors.Barometer_Page>();

        // App Capabilities
        mauiAppBuilder.Services.AddTransient<Vibration_Page>();
        mauiAppBuilder.Services.AddTransient<HapticFeedback_Page>();

        // UI
        mauiAppBuilder.Services.AddTransient<DrawArc_Page>();

        return mauiAppBuilder;
    }
}
