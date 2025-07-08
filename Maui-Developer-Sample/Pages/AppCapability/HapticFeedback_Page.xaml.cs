using Maui_Developer_Sample.Pages.AppCapability.ViewModels;

namespace Maui_Developer_Sample.Pages.AppCapability;

public partial class HapticFeedback_Page : ContentPage
{
    private readonly HapticFeedbackViewModel _viewModel;

    public HapticFeedback_Page(HapticFeedbackViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        BindingContext = _viewModel;
    }
}
