using Maui_Developer_Sample.Pages.HF;

using Microsoft.Maui.Controls;

namespace Maui_Developer_Sample;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(HFP_Page1), typeof(HFP_Page1));

    }
}
