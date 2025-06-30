using Maui_Developer_Sample.Pages;

namespace Maui_Developer_Sample;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var mainPage = MauiProgram.Services?.GetRequiredService<MainPage>();
        var rainbowPastelBrush = new LinearGradientBrush([
                                                             new GradientStop(Colors.LightCoral, 0.0f),
                                                             new GradientStop(Colors.LightSalmon, 0.2f),
                                                             new GradientStop(Colors.LightYellow, 0.4f),
                                                             new GradientStop(Colors.LightGreen, 0.6f),
                                                             new GradientStop(Colors.LightBlue, 0.8f),
                                                             new GradientStop(Colors.Lavender, 1.0f)
                                                         ],
                                                         new Point(0, 0), 
                                                         new Point(1, 0));
        var navigationPage = new NavigationPage(mainPage)
        {
            BarBackground = rainbowPastelBrush,
            BarTextColor = Colors.DarkSlateGray,
        };
        var rainbowBrush = new LinearGradientBrush([
                                                       new GradientStop(Colors.Red, 0.0f),
                                                       new GradientStop(Colors.Orange, 0.2f),
                                                       new GradientStop(Colors.Yellow, 0.4f),
                                                       new GradientStop(Colors.Green, 0.6f),
                                                       new GradientStop(Colors.Blue, 0.8f),
                                                       new GradientStop(Colors.Purple, 1.0f)
                                                   ],
                                                   new Point(0, 0), 
                                                   new Point(1, 0));
        var mainWindow = new Window(navigationPage)
        {
            TitleBar = new TitleBar
            {
                Title = "Maui Developer Sample",
                ForegroundColor = Colors.DarkSlateGray,
                Background = rainbowBrush,
               
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                HeightRequest = 40,
            }
        };
        return mainWindow;
    }
}
