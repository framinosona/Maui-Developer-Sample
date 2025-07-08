using Maui_Developer_Sample.Pages.UI.ViewModels;

namespace Maui_Developer_Sample.Pages.UI;

public partial class DrawArc_Page
{
    private readonly DrawArcViewModel _viewModel;

    public DrawArc_Page(DrawArcViewModel drawArcViewModel)
    {
        InitializeComponent();
        _viewModel = drawArcViewModel ?? throw new ArgumentNullException(nameof(drawArcViewModel));
        BindingContext = _viewModel;
    }
}

