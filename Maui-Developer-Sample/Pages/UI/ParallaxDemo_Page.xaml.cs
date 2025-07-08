using Maui_Developer_Sample.Pages.UI.ViewModels;

namespace Maui_Developer_Sample.Pages.UI;

/// <summary>
/// Parallax demo page that showcases advanced UI effects using gyroscope sensor data.
/// </summary>
/// <remarks>
/// This page demonstrates how to create immersive user experiences by utilizing
/// device motion sensors to create parallax scrolling and rotation effects.
///
/// The page serves as a foundation for implementing complex motion-based UI interactions
/// and can be extended with additional visual effects and animations.
/// </remarks>
public partial class ParallaxDemo_Page : ContentPage
{
    private readonly ParallaxDemoViewModel _viewModel;

    /// <summary>
    /// Initializes a new instance of the ParallaxDemo_Page.
    /// </summary>
    /// <param name="viewModel">The parallax demo view model instance.</param>
    public ParallaxDemo_Page(ParallaxDemoViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        BindingContext = _viewModel;
    }

    /// <summary>
    /// Handles the page disappearing event.
    /// Ensures the gyroscope sensor is stopped when the user navigates away.
    /// </summary>
    protected override void OnDisappearing()
    {
        if (_viewModel.IsMonitoring)
        {
            _viewModel.IsMonitoring = false;
        }
        base.OnDisappearing();
    }
}
