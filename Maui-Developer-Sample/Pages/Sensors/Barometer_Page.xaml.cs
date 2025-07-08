using Maui_Developer_Sample.Pages.Sensors.ViewModels;

namespace Maui_Developer_Sample.Pages.Sensors;

public partial class Barometer_Page : ContentPage
{
    private readonly BarometerViewModel _viewModel;

    public Barometer_Page(BarometerViewModel viewModel)
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
