using Maui_Developer_Sample.Pages.Vibrations;

namespace Maui_Developer_Sample.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnNavigateToHapticFeedbackSimpleClicked(object? sender, EventArgs e)
    {
        App.AppShell.GoToAsync($"//{nameof(MainPage)}/{nameof(HapticFeedback_Page)}");
    }
    
    

}
