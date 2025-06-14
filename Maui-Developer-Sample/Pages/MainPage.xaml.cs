﻿using Maui_Developer_Sample.Pages.Sensors;
using Maui_Developer_Sample.Pages.Vibrations;

namespace Maui_Developer_Sample.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnNavigateToHapticFeedbackSimpleClicked(object? sender, EventArgs e)
    {
        App.AppShell.GoToAsync($"//{nameof(MainPage)}/{nameof(HapticFeedback_Page)}");
    }
    
    private void OnNavigateToAccelerometerClicked(object? sender, EventArgs e)
    {
        App.AppShell.GoToAsync($"//{nameof(MainPage)}/{nameof(Accelerometer_Page)}");
    }

    private void OnNavigateToBarometerClicked(object? sender, EventArgs e)
    {
        App.AppShell.GoToAsync($"//{nameof(MainPage)}/{nameof(Barometer_Page)}");
    }

    private void OnNavigateToCompassClicked(object? sender, EventArgs e)
    {
        App.AppShell.GoToAsync($"//{nameof(MainPage)}/{nameof(Compass_Page)}");
    }

    private void OnNavigateToGyroscopeClicked(object? sender, EventArgs e)
    {
        App.AppShell.GoToAsync($"//{nameof(MainPage)}/{nameof(Gyroscope_Page)}");
    }

    private void OnNavigateToMagnetometerClicked(object? sender, EventArgs e)
    {
        App.AppShell.GoToAsync($"//{nameof(MainPage)}/{nameof(Magnetometer_Page)}");
    }

    private void OnNavigateToOrientationSensorClicked(object? sender, EventArgs e)
    {
        App.AppShell.GoToAsync($"//{nameof(MainPage)}/{nameof(OrientationSensor_Page)}");
    }
    

}
