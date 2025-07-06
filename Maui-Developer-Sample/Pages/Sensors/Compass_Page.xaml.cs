using Maui_Developer_Sample.Pages.Sensors.Services;

namespace Maui_Developer_Sample.Pages.Sensors;

public partial class Compass_Page
{
    public Compass_Page(Compass_Service compassService)
    {
        InitializeComponent();
        BindingContext = compassService;
    }
}
