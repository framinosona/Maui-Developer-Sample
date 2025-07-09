namespace Maui_Developer_Sample.Pages.UI.Views;

/// <summary>
/// Parallax offset source that uses bindable properties for X and Y values.
/// </summary>
public class ParallaxOffsetFromBindingSource : ParallaxOffsetSource
{
    /// <summary>
    /// Initializes a new instance of the ParallaxOffsetFromBindingSource.
    /// </summary>
    public ParallaxOffsetFromBindingSource()
    {
    }

    /// <summary>
    /// Gets or sets the X offset value for parallax effect.
    /// </summary>
    public double ParallaxXValue
    {
        get => (double)GetValue(ParallaxXValueProperty);
        set => SetValue(ParallaxXValueProperty, value);
    }

    /// <summary>
    /// Bindable property for ParallaxXValue.
    /// </summary>
    public readonly static BindableProperty ParallaxXValueProperty =
        BindableProperty.Create(nameof(ParallaxXValue), typeof(double), typeof(ParallaxOffsetFromBindingSource), 0.0, propertyChanged: OnParallaxXValueChanged);

    /// <summary>
    /// Gets or sets the Y offset value for parallax effect.
    /// </summary>
    public double ParallaxYValue
    {
        get => (double)GetValue(ParallaxYValueProperty);
        set => SetValue(ParallaxYValueProperty, value);
    }

    /// <summary>
    /// Bindable property for ParallaxYValue.
    /// </summary>
    public readonly static BindableProperty ParallaxYValueProperty =
        BindableProperty.Create(nameof(ParallaxYValue), typeof(double), typeof(ParallaxOffsetFromBindingSource), 0.0, propertyChanged: OnParallaxYValueChanged);

    /// <summary>
    /// Handles changes to the ParallaxXValue property.
    /// </summary>
    protected static void OnParallaxXValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ParallaxOffsetFromBindingSource source)
        {
            source.OnParallaxValueChanged();
        }
    }

    /// <summary>
    /// Handles changes to the ParallaxYValue property.
    /// </summary>
    protected static void OnParallaxYValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ParallaxOffsetFromBindingSource source)
        {
            source.OnParallaxValueChanged();
        }
    }

    /// <summary>
    /// Notifies all listeners when either X or Y values change.
    /// </summary>
    protected void OnParallaxValueChanged()
    {
        NotifyListeners(ParallaxXValue, ParallaxYValue);
    }
}
