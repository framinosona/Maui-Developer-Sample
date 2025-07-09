using Maui_Developer_Sample.Helpers;

namespace Maui_Developer_Sample.Pages.UI.Views;

/// <summary>
/// Interface for objects that can listen to parallax offset changes.
/// </summary>
public interface IParallaxOffsetListener
{
    /// <summary>
    /// Called when the parallax offset values change.
    /// </summary>
    /// <param name="x">The horizontal offset value between -1 and 1.</param>
    /// <param name="y">The vertical offset value between -1 and 1.</param>
    void OnParallaxOffsetChanged(double x, double y);
}

/// <summary>
/// A content view that applies parallax translation effects to its child content.
/// </summary>
public class ParallaxLayer : ContentView, IParallaxOffsetListener
{
    /// <summary>
    /// Initializes a new instance of the ParallaxLayer class.
    /// </summary>
    public ParallaxLayer()
    {
        VerticalOptions = LayoutOptions.Fill;
        HorizontalOptions = LayoutOptions.Fill;
    }

    /// <summary>
    /// Gets or sets the current horizontal parallax offset value.
    /// </summary>
    public double ParallaxX { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the current vertical parallax offset value.
    /// </summary>
    public double ParallaxY { get; set; } = 0.0;

    /// <summary>
    /// Called when the parallax offset source provides new offset values.
    /// </summary>
    /// <param name="x">The horizontal offset value between -1 and 1.</param>
    /// <param name="y">The vertical offset value between -1 and 1.</param>
    public void OnParallaxOffsetChanged(double x, double y)
    {
        ParallaxX = x;
        ParallaxY = y;
        UpdateTranslation();
    }

    /// <summary>
    /// Updates the translation transform based on current parallax values and maximum distances.
    /// </summary>
    public void UpdateTranslation()
    {
        // Calculate translation based on parallax offset and maximum distance
        TranslationX = ParallaxX * ParallaxMaxDistanceX;
        TranslationY = ParallaxY * ParallaxMaxDistanceY;
    }

    /// <summary>
    /// Gets or sets the parallax offset source that provides the parallax data.
    /// </summary>
    public ParallaxOffsetSource? ParallaxOffsetSource
    {
        get => (ParallaxOffsetSource?)GetValue(ParallaxOffsetSourceProperty);
        set => SetValue(ParallaxOffsetSourceProperty, value);
    }

    /// <summary>
    /// Bindable property for ParallaxOffsetSource.
    /// </summary>
    public readonly static BindableProperty ParallaxOffsetSourceProperty =
        BindableProperty.Create(nameof(ParallaxOffsetSource), typeof(ParallaxOffsetSource), typeof(ParallaxLayer), null,
            propertyChanged: OnParallaxOffsetSourceChanged);

    /// <summary>
    /// Gets or sets the maximum horizontal distance for parallax movement in pixels.
    /// </summary>
    public double ParallaxMaxDistanceX
    {
        get => (double)GetValue(ParallaxMaxDistanceXProperty);
        set => SetValue(ParallaxMaxDistanceXProperty, value);
    }

    /// <summary>
    /// Bindable property for ParallaxMaxDistanceX.
    /// </summary>
    public readonly static BindableProperty ParallaxMaxDistanceXProperty =
        BindableProperty.Create(nameof(ParallaxMaxDistanceX), typeof(double), typeof(ParallaxLayer), 10.0,
            propertyChanged: OnParallaxDistanceChanged);

    /// <summary>
    /// Gets or sets the maximum vertical distance for parallax movement in pixels.
    /// </summary>
    public double ParallaxMaxDistanceY
    {
        get => (double)GetValue(ParallaxMaxDistanceYProperty);
        set => SetValue(ParallaxMaxDistanceYProperty, value);
    }

    /// <summary>
    /// Bindable property for ParallaxMaxDistanceY.
    /// </summary>
    public readonly static BindableProperty ParallaxMaxDistanceYProperty =
        BindableProperty.Create(nameof(ParallaxMaxDistanceY), typeof(double), typeof(ParallaxLayer), 10.0,
            propertyChanged: OnParallaxDistanceChanged);

    /// <summary>
    /// Handles changes to the parallax distance properties.
    /// </summary>
    private static void OnParallaxDistanceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ParallaxLayer layer)
        {
            layer.UpdateTranslation();
        }
    }

    /// <summary>
    /// Handles changes to the ParallaxOffsetSource property.
    /// </summary>
    private static void OnParallaxOffsetSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not ParallaxLayer layer)
            return;

        // Remove listener from old source
        if (oldValue is ParallaxOffsetSource oldSource)
        {
            oldSource.RemoveListener(layer);
        }

        // Add listener to new source
        if (newValue is ParallaxOffsetSource newSource)
        {
            newSource.AddListener(layer);
        }
    }
}
