using Maui_Developer_Sample.Pages.UI.ViewModels;

namespace Maui_Developer_Sample.Pages.UI;

public partial class AppTheme_Page
{

    public AppTheme_Page(AppThemeViewModel appThemeViewModel)
    {
        InitializeComponent();
        var viewModel = appThemeViewModel ?? throw new ArgumentNullException(nameof(appThemeViewModel));
        BindingContext = viewModel;
    }
}

