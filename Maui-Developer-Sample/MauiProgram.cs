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
               .ConfigureViewModels()
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
        mauiAppBuilder.Services.AddSingleton<Services.AccelerometerSensorService>();
        mauiAppBuilder.Services.AddSingleton<Services.GyroscopeSensorService>();
        mauiAppBuilder.Services.AddSingleton<Services.MagnetometerSensorService>();
        mauiAppBuilder.Services.AddSingleton<Services.OrientationSensorService>();
        mauiAppBuilder.Services.AddSingleton<Services.CompassSensorService>();
        mauiAppBuilder.Services.AddSingleton<Services.BarometerSensorService>();

        // App Capabilities
        mauiAppBuilder.Services.AddSingleton<Services.HapticFeedbackService>();
        mauiAppBuilder.Services.AddSingleton<Services.VibrationService>();

        // UI
        mauiAppBuilder.Services.AddSingleton<Services.AppThemeService>();

        // Logging
        mauiAppBuilder.Services.AddLogging(logging => logging.SetMinimumLevel(LogLevel.Debug));

        return mauiAppBuilder;
    }

    public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        // Sensors
        mauiAppBuilder.Services.AddSingleton<Pages.Sensors.ViewModels.AccelerometerViewModel>();
        mauiAppBuilder.Services.AddSingleton<Pages.Sensors.ViewModels.GyroscopeViewModel>();
        mauiAppBuilder.Services.AddSingleton<Pages.Sensors.ViewModels.MagnetometerViewModel>();
        mauiAppBuilder.Services.AddSingleton<Pages.Sensors.ViewModels.OrientationSensorViewModel>();
        mauiAppBuilder.Services.AddSingleton<Pages.Sensors.ViewModels.CompassViewModel>();
        mauiAppBuilder.Services.AddSingleton<Pages.Sensors.ViewModels.BarometerViewModel>();

        // App Capabilities
        mauiAppBuilder.Services.AddSingleton<Pages.AppCapability.ViewModels.VibrationViewModel>();
        mauiAppBuilder.Services.AddSingleton<Pages.AppCapability.ViewModels.HapticFeedbackViewModel>();

        // UI
        mauiAppBuilder.Services.AddSingleton<Pages.UI.ViewModels.DrawArcViewModel>();
        mauiAppBuilder.Services.AddSingleton<Pages.UI.ViewModels.AppThemeViewModel>();

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
        mauiAppBuilder.Services.AddTransient<Pages.AppCapability.Vibration_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.AppCapability.HapticFeedback_Page>();

        // UI
        mauiAppBuilder.Services.AddTransient<Pages.UI.DrawArc_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.UI.AppTheme_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.UI.ParallaxGyroscope_Page>();
        mauiAppBuilder.Services.AddTransient<Pages.UI.ParallaxBinding_Page>();

        return mauiAppBuilder;
    }
}
