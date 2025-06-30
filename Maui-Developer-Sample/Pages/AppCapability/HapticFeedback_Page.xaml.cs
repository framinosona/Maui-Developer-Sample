using Maui_Developer_Sample.Pages.AppCapability.Services;

namespace Maui_Developer_Sample.Pages.AppCapability;

public partial class HapticFeedback_Page : ContentPage
{
    public HapticFeedback_Page(HapticFeedback_Service hapticFeedbackService)
    {
        InitializeComponent();
        BindingContext = hapticFeedbackService;
    }
}
