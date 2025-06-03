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
        Routing.RegisterRoute(nameof(Barometer_Page), typeof(Barometer_Page));
        Routing.RegisterRoute(nameof(Compass_Page), typeof(Compass_Page));
        Routing.RegisterRoute(nameof(Gyroscope_Page), typeof(Gyroscope_Page));
        Routing.RegisterRoute(nameof(Magnetometer_Page), typeof(Magnetometer_Page));
        Routing.RegisterRoute(nameof(OrientationSensor_Page), typeof(OrientationSensor_Page));

    }
}
