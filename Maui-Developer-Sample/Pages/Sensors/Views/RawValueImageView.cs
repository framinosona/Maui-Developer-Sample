namespace Maui_Developer_Sample.Pages.Sensors.Views;

public class RawValueImageView : ContentView
{
    public static void Refresh(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (RawValueImageView) bindable;
        if (control.Value > control.MaxValue * control.Tolerance)
        {
            control.PositiveImage.IsVisible = true;
            control.NegativeImage.IsVisible = false;
            control.NeutralImage.IsVisible = false;
        }
        else if (control.Value < control.MinValue * control.Tolerance)
        {
            control.PositiveImage.IsVisible = false;
            control.NegativeImage.IsVisible = true;
            control.NeutralImage.IsVisible = false;
        }
        else
        {
            control.PositiveImage.IsVisible = false;
            control.NegativeImage.IsVisible = false;
            control.NeutralImage.IsVisible = true;
        }
    }

    #region Value

    public readonly static BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value),
                                typeof(float),
                                typeof(RawValueImageView),
                                defaultValue: 0.0f,
                                propertyChanged: Refresh);

    public float Value
    {
        get => (float) GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    #endregion

    #region Tolerance

    public readonly static BindableProperty ToleranceProperty =
        BindableProperty.Create(nameof(Tolerance),
                                typeof(float),
                                typeof(RawValueImageView),
                                defaultValue: 0.04f,
                                propertyChanged: Refresh);

    public float Tolerance
    {
        get => (float) GetValue(ToleranceProperty);
        set => SetValue(ToleranceProperty, value);
    }

    #endregion

    #region MaxValue

    public readonly static BindableProperty MaxValueProperty = BindableProperty.Create(nameof(MaxValue),
        typeof(float),
        typeof(RawValueImageView),
        defaultValue: 1.0f,
        propertyChanged: Refresh);

    public float MaxValue
    {
        get => (float) GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    #endregion

    #region MinValue

    public readonly static BindableProperty MinValueProperty = BindableProperty.Create(nameof(MinValue),
        typeof(float),
        typeof(RawValueImageView),
        defaultValue: -1.0f,
        propertyChanged: Refresh);

    public float MinValue
    {
        get => (float) GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }

    #endregion

    #region PositiveImage

    public readonly static BindableProperty PositiveImageSourceProperty =
        BindableProperty.Create(nameof(PositiveImageSource),
                                typeof(string),
                                typeof(RawValueImageView),
                                propertyChanged: (bindable, value, newValue) => {
                                    var control = (RawValueImageView) bindable;
                                    control.PositiveImage.Source = newValue.ToString();
                                });

    public string PositiveImageSource
    {
        get => (string) GetValue(PositiveImageSourceProperty);
        set => SetValue(PositiveImageSourceProperty, value);
    }

    private Image PositiveImage { get; set; }

    #endregion

    #region NegativeImage

    public readonly static BindableProperty NegativeImageSourceProperty =
        BindableProperty.Create(nameof(NegativeImageSource),
                                typeof(string),
                                typeof(RawValueImageView),
                                propertyChanged: (bindable, value, newValue) => {
                                    var control = (RawValueImageView) bindable;
                                    control.NegativeImage.Source = newValue.ToString();
                                });

    public string NegativeImageSource
    {
        get => (string) GetValue(NegativeImageSourceProperty);
        set => SetValue(NegativeImageSourceProperty, value);
    }

    private Image NegativeImage { get; set; }

    #endregion

    #region NeutralImage

    public readonly static BindableProperty NeutralImageSourceProperty =
        BindableProperty.Create(nameof(NeutralImageSource),
                                typeof(string),
                                typeof(RawValueImageView),
                                propertyChanged: (bindable, value, newValue) => {
                                    var control = (RawValueImageView) bindable;
                                    control.NeutralImage.Source = newValue.ToString();
                                });

    public string NeutralImageSource
    {
        get => (string) GetValue(NeutralImageSourceProperty);
        set => SetValue(NeutralImageSourceProperty, value);
    }

    private Image NeutralImage { get; set; }

    #endregion

    public RawValueImageView()
    {
        PositiveImage = new Image { IsVisible = false };
        NegativeImage = new Image { IsVisible = false };
        NeutralImage = new Image { IsVisible = true };

        Content = new Grid
        {
            Children = { PositiveImage, NegativeImage, NeutralImage }
        };
    }
}
