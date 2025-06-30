using Maui_Developer_Sample.Helpers;

namespace Maui_Developer_Sample.Pages.UI.ViewModels;

public class DrawArc_ViewModel : EnhancedBindableObject
{
    public float EndAngle
    {
        get => GetValue(90.0f);
        set => SetValue(value);
    }

    public float StartAngle
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    public float OffsetAngle
    {
        get => GetValue(90.0f);
        set => SetValue(value);
    }

    public bool Clockwise
    {
        get => GetValue(false);
        set => SetValue(value);
    }
}
