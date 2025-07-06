namespace Maui_Developer_Sample.Pages.Sensors.Views;

public abstract class BaseIndicator : GraphicsView
{

    public readonly static BindableProperty ValueProperty = BindableProperty.Create(nameof(Value),
                                                                                    typeof(float),
                                                                                    typeof(BaseIndicator),
                                                                                    0.0f,
                                                                                    propertyChanged: (bindable, oldValue, newValue) => {
                                                                                        var control = (BaseIndicator) bindable;
                                                                                        control.Invalidate();
                                                                                    });

    public readonly static BindableProperty MaxValueProperty = BindableProperty.Create(nameof(MaxValue),
                                                                                       typeof(float),
                                                                                       typeof(BaseIndicator),
                                                                                       1.0f,
                                                                                       propertyChanged: (bindable, oldValue, newValue) => {
                                                                                           var control = (BaseIndicator) bindable;
                                                                                           control.Invalidate();
                                                                                       });

    public readonly static BindableProperty MinValueProperty = BindableProperty.Create(nameof(MinValue),
                                                                                       typeof(float),
                                                                                       typeof(BaseIndicator),
                                                                                       -1.0f,
                                                                                       propertyChanged: (bindable, oldValue, newValue) => {
                                                                                           var control = (BaseIndicator) bindable;
                                                                                           control.Invalidate();
                                                                                       });

    public readonly static BindableProperty ToleranceProperty = BindableProperty.Create(nameof(Tolerance),
                                                                                        typeof(float),
                                                                                        typeof(BaseIndicator),
                                                                                        0.02f,
                                                                                        propertyChanged: (bindable, oldValue, newValue) => {
                                                                                            var control = (BaseIndicator) bindable;
                                                                                            control.Invalidate();
                                                                                        });

    public float Value
    {
        get => (float) GetValue(ValueProperty);
        set => SetValue(ValueProperty, Math.Clamp(value, -MaxValue, MaxValue));
    }

    public float MaxValue
    {
        get => (float) GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    public float MinValue
    {
        get => (float) GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }

    public float Tolerance
    {
        get => (float) GetValue(ToleranceProperty);
        set => SetValue(ToleranceProperty, value);
    }
}
