using Maui_Developer_Sample.Pages.Sensors.Services;

namespace Maui_Developer_Sample.Pages.Sensors;

public partial class Barometer_Page : ContentPage
{
    public Barometer_Page(Barometer_Service barometerService)
    {
        InitializeComponent();
        BindingContext = barometerService;
    }
}

