using Maui_Developer_Sample.Pages.AppCapability.ViewModels;

namespace Maui_Developer_Sample.Pages.AppCapability;

public partial class Vibration_Page : ContentPage
{
    private readonly VibrationViewModel _viewModel;

    public Vibration_Page(VibrationViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        BindingContext = _viewModel;
    }
}
