using Maui_Developer_Sample.Pages.Sensors.Services;

namespace Maui_Developer_Sample.Pages.Sensors;

public partial class Accelerometer_Page : ContentPage
{
    public Accelerometer_Page(Accelerometer_Service accelerometerService)
    {
        InitializeComponent();
        BindingContext = accelerometerService;
    }
}
