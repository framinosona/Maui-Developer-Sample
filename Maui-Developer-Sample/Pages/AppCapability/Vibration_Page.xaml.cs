using Maui_Developer_Sample.Pages.AppCapability.Services;

namespace Maui_Developer_Sample.Pages.AppCapability;

public partial class Vibration_Page : ContentPage
{
    public Vibration_Page(Vibration_Service vibrationService)
    {
        InitializeComponent();
        BindingContext = vibrationService;
    }
}
