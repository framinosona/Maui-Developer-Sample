using Maui_Developer_Sample.Pages.UI.ViewModels;

namespace Maui_Developer_Sample.Pages.UI;

public partial class AppTheme_Page
{
    private readonly AppThemeViewModel _viewModel;

    public AppTheme_Page(AppThemeViewModel appThemeViewModel)
    {
        InitializeComponent();
        _viewModel = appThemeViewModel ?? throw new ArgumentNullException(nameof(appThemeViewModel));
        BindingContext = _viewModel;
    }
}

