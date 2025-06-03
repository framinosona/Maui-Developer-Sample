using Maui_Developer_Sample.Pages.Vibrations;

using Microsoft.Maui.Controls;

namespace Maui_Developer_Sample;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(HapticFeedbackSimple_Page), typeof(HapticFeedbackSimple_Page));

    }
}
