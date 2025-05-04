using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Maui_Developer_Sample;

public partial class App : Application
{
    public static AppShell AppShell { get; private set; }
    
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        AppShell = new AppShell();
        return new Window(AppShell);
    }
}
