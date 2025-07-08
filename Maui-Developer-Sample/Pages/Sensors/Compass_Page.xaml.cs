using Maui_Developer_Sample.Pages.Sensors.ViewModels;

namespace Maui_Developer_Sample.Pages.Sensors;

public partial class Compass_Page : ContentPage
{
    private readonly CompassViewModel _viewModel;

    public Compass_Page(CompassViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        BindingContext = _viewModel;
    }

    protected override void OnDisappearing()
    {
        if (_viewModel.IsMonitoring)
        {
            _viewModel.IsMonitoring = false;
        }
        base.OnDisappearing();
    }
}
