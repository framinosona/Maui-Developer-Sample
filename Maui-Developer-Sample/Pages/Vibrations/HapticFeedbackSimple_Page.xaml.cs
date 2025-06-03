namespace Maui_Developer_Sample.Pages.Vibrations;

public partial class HapticFeedbackSimple_Page : ContentPage
{
    public HapticFeedbackSimple_Page()
    {
        InitializeComponent();
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
                // Try multiple URIs in sequence of most specific to most general
                var uris = new List<Uri>
                {
                    // Most specific - direct vibration settings (Samsung, some other devices)
                    new Uri("android.settings.ACCESSIBILITY_VIBRATION_SETTINGS"),

                    // On some devices this might lead to vibration settings
                    new Uri("android.settings.VIBRATION_SETTINGS"),

                    // General sound & vibration page
                    new Uri("android.settings.SOUND_SETTINGS"),

                    // Some devices use this path for vibration
                    new Uri("android.settings.ACCESSIBILITY_SETTINGS")
                };

                bool launched = false;
                foreach (var uri in uris)
                {
                    if (!await Launcher.CanOpenAsync(uri))
                        continue;

                    await Launcher.OpenAsync(uri);
                    launched = true;
                    break;
                }

                if (!launched)
                {
                    // Last resort - open main settings
                    await Launcher.OpenAsync(new Uri("android.settings.SETTINGS"));
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Settings Error", $"Could not open settings: {ex.Message}", "OK");
        }
    }
}
