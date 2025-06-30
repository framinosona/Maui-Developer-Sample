using Maui_Developer_Sample.Pages.UI.ViewModels;

namespace Maui_Developer_Sample.Pages.UI;

public partial class DrawArc_Page 
{
    public DrawArc_Page(DrawArc_ViewModel drawArcViewModel)
    {
        InitializeComponent();
        BindingContext = drawArcViewModel;
    }
}

