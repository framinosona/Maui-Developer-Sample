using Maui_Developer_Sample.Pages.Sensors.Services;

namespace Maui_Developer_Sample.Pages.Sensors;

public partial class Accelerometer_Page : ContentPage
{
    public Accelerometer_Page(Accelerometer_Service accelerometerService)
    {
        InitializeComponent();
        BindingContext = accelerometerService;
    }

    protected override void OnDisappearing()
    {
        if (BindingContext is BaseBindableSensor_Service sensorService)
        {
            sensorService.IsMonitoring = false;
        }
        base.OnDisappearing();
    }
}
