using CommunityToolkit.Maui;

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
        mauiAppBuilder.Services.AddSingleton<Pages.Sensors.Services.Accelerometer_Service>();
        mauiAppBuilder.Services.AddSingleton<Pages.Sensors.Services.Gyroscope_Service>();
        mauiAppBuilder.Services.AddSingleton<Pages.Sensors.Services.Magnetometer_Service>();
        mauiAppBuilder.Services.AddSingleton<Pages.Sensors.Services.OrientationSensor_Service>();
        mauiAppBuilder.Services.AddSingleton<Pages.Sensors.Services.Compass_Service>();
        mauiAppBuilder.Services.AddSingleton<Pages.Sensors.Services.Barometer_Service>();

        // App Capabilities
        mauiAppBuilder.Services.AddSingleton<Services.HapticFeedbackService>();
        mauiAppBuilder.Services.AddSingleton<Services.VibrationService>();
        
        // UI
        mauiAppBuilder.Services.AddSingleton<Pages.UI.Services.AppTheme_Service>();
        
        // Logging
        mauiAppBuilder.Services.AddLogging(logging => logging.SetMinimumLevel(LogLevel.Debug));

        return mauiAppBuilder;
    }

    public static MauiAppBuilder ConfigurePages(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<Pages.MainPage>();
        // AppCapability ViewModels
        mauiAppBuilder.Services.AddTransient<Pages.AppCapability.ViewModels.HapticFeedbackViewModel>();
        mauiAppBuilder.Services.AddTransient<Pages.AppCapability.ViewModels.VibrationViewModel>();

        // Sensors
        mauiAppBuilder.Services.AddTransient<Pages.Sensors.Accelerometer_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.Sensors.Gyroscope_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.Sensors.Magnetometer_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.Sensors.OrientationSensor_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.Sensors.Compass_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.Sensors.Barometer_Page>();

        // App Capabilities
        mauiAppBuilder.Services.AddTransient<Pages.AppCapability.Vibration_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.AppCapability.HapticFeedback_Page>();

        // UI
        mauiAppBuilder.Services.AddTransient<Pages.UI.ViewModels.DrawArc_ViewModel>();
        mauiAppBuilder.Services.AddTransient<Pages.UI.DrawArc_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.UI.AppTheme_Page>();

        return mauiAppBuilder;
    }
}
