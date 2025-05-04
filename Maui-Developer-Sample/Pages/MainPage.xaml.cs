namespace Maui_Developer_Sample.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnNavigateToHFP1Clicked(object? sender, EventArgs e)
    {
        App.AppShell.GoToAsync("//MainPage/HFP_Page1");
    }
}
