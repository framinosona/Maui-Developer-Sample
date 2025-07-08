using Maui_Developer_Sample.Pages.Sensors.Services;

namespace Maui_Developer_Sample.Pages.Sensors;

public partial class Magnetometer_Page : ContentPage
{
    public Magnetometer_Page(Magnetometer_Service magnetometerService)
    {
        InitializeComponent();
        BindingContext = magnetometerService;
    }

    protected override void OnDisappearing()
    {
        if (BindingContext is Magnetometer_Service sensorService)
        {
            sensorService.IsMonitoring = false;
        }
        base.OnDisappearing();
    }
}
