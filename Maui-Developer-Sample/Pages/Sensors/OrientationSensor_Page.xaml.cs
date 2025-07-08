using Maui_Developer_Sample.Pages.Sensors.Services;

namespace Maui_Developer_Sample.Pages.Sensors;

public partial class OrientationSensor_Page : ContentPage
{
    public OrientationSensor_Page(OrientationSensor_Service orientationSensorService)
    {
        InitializeComponent();
        BindingContext = orientationSensorService;
    }

    protected override void OnDisappearing()
    {
        if (BindingContext is OrientationSensor_Service sensorService)
        {
            sensorService.IsMonitoring = false;
        }
        base.OnDisappearing();
    }
}
