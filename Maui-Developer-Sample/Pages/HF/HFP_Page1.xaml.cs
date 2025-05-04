using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

using System;

namespace Maui_Developer_Sample.Pages.HF;

public partial class HFP_Page1 : ContentPage
{
    public HFP_Page1()
    {
        // Add content relating to Microsoft.Maui.Devices.HapticFeedback
        InitializeComponent();
    }

    private void OnStandardClicked(object sender, EventArgs e)
    {
        // Performs a standard click haptic feedback
        HapticFeedback.Default.Perform(HapticFeedbackType.Click);
    }

    private void OnMediumClicked(object sender, EventArgs e)
    {
        // Performs a medium intensity haptic feedback
        HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
    }

    private async void OnOpenSettingsClicked(object sender, EventArgs e)
    {
        try
        {
            if (OperatingSystem.IsIOS())
            {
                // Try to open iOS Sound settings
                // Note: Apple limits direct access to specific settings pages
                // This will likely open the main Settings app
                await Launcher.OpenAsync(new Uri("App-Prefs:Sounds"));
            }
            else if (OperatingSystem.IsAndroid())
            {
                // On Android, try to open the Sound & Vibration settings
                // ACTION_SOUND_SETTINGS is available from API 16+
                await Launcher.OpenAsync(new Uri("android.settings.SOUND_SETTINGS"));

                // Alternative: For direct vibration settings (may not work on all devices)
                // await Launcher.OpenAsync(new Uri("android.settings.ACCESSIBILITY_VIBRATION_SETTINGS"));
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Settings Error", $"Could not open settings: {ex.Message}", "OK");
        }
    }
}
