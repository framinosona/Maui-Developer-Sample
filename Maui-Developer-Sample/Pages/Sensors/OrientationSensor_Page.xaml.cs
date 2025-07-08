using Maui_Developer_Sample.Pages.Sensors.ViewModels;

namespace Maui_Developer_Sample.Pages.Sensors;

public partial class OrientationSensor_Page : ContentPage
{
    private readonly OrientationSensorViewModel _viewModel;

    public OrientationSensor_Page(OrientationSensorViewModel viewModel)
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
