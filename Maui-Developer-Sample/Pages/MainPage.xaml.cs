using Maui_Developer_Sample.Pages.AppCapability;
using Maui_Developer_Sample.Pages.Sensors;
using Maui_Developer_Sample.Pages.UI;

namespace Maui_Developer_Sample.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnNavigateToHapticFeedbackClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(MauiProgram.Services?.GetRequiredService<HapticFeedback_Page>());
    }

    private void OnNavigateToVibrationClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(MauiProgram.Services?.GetRequiredService<Vibration_Page>());
    }

    private void OnNavigateToAccelerometerClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(MauiProgram.Services?.GetRequiredService<Accelerometer_Page>());
    }

    private void OnNavigateToBarometerClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(MauiProgram.Services?.GetRequiredService<Barometer_Page>());
    }

    private void OnNavigateToCompassClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(MauiProgram.Services?.GetRequiredService<Compass_Page>());
    }

    private void OnNavigateToGyroscopeClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(MauiProgram.Services?.GetRequiredService<Gyroscope_Page>());
    }

    private void OnNavigateToMagnetometerClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(MauiProgram.Services?.GetRequiredService<Magnetometer_Page>());
    }

    private void OnNavigateToOrientationSensorClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(MauiProgram.Services?.GetRequiredService<OrientationSensor_Page>());
    }

    private void OnNavigateToDrawArcClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(MauiProgram.Services?.GetRequiredService<DrawArc_Page>());
    }

    private void OnNavigateToAppThemeClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(MauiProgram.Services?.GetRequiredService<AppTheme_Page>());
    }

    private void OnNavigateToParallaxBindingClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(MauiProgram.Services?.GetRequiredService<ParallaxBinding_Page>());
    }

    private void OnNavigateToParallaxGyroscopeClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(MauiProgram.Services?.GetRequiredService<ParallaxGyroscope_Page>());
    }

}
