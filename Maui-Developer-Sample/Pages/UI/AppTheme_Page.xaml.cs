using Maui_Developer_Sample.Pages.UI.Services;

namespace Maui_Developer_Sample.Pages.UI;

public partial class AppTheme_Page
{
    public AppTheme_Page(AppTheme_Service appThemeService)
    {
        InitializeComponent();
        BindingContext = appThemeService;
    }
}

