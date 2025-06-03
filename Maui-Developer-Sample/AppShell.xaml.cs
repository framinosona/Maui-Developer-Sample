using Maui_Developer_Sample.Pages.Vibrations;
using Maui_Developer_Sample.Pages.Sensors;

using Microsoft.Maui.Controls;

namespace Maui_Developer_Sample;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(HapticFeedback_Page), typeof(HapticFeedback_Page));
        Routing.RegisterRoute(nameof(Accelerometer_Page), typeof(Accelerometer_Page));

    }
}
