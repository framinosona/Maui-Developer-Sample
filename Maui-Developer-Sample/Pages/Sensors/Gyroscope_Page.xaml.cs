using Maui_Developer_Sample.Pages.Sensors.Services;

namespace Maui_Developer_Sample.Pages.Sensors;

public partial class Gyroscope_Page : ContentPage
{
    public Gyroscope_Page(Gyroscope_Service gyroscopeService)
    {
        InitializeComponent();
        BindingContext = gyroscopeService;
    }
}

