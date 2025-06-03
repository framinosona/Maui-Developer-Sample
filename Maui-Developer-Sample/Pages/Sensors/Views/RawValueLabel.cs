namespace Maui_Developer_Sample.Pages.Sensors.Views;

public class RawValueLabel : Label
{
    public static void Refresh(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (RawValueLabel)bindable;
        if (control.Value > control.MaxValue * control.Tolerance)
        {
            control.TextColor = Colors.DarkGreen;
            control.Text = string.Format(control.Format, control.Value) + "🔺";
        }
        else if (control.Value < control.MinValue * control.Tolerance)
        {
            control.TextColor = Colors.DarkRed;
            control.Text = string.Format(control.Format, control.Value) + "🔻";
        }
        else
        {
            control.TextColor = Colors.Gray;
            control.Text = string.Format(control.Format, control.Value);
        }
    }
    
    public readonly static BindableProperty FormatProperty = BindableProperty.Create(nameof(Format),
             typeof(string),
             typeof(RawValueLabel),
             defaultValue: "{0}",
             propertyChanged: Refresh);

    public string Format
    {
        get => (string) GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }

    public readonly static BindableProperty ValueProperty = BindableProperty.Create(nameof(Value),
             typeof(float),
             typeof(RawValueLabel),
             defaultValue: 0.0f,
             propertyChanged: Refresh);

    public float Value
    {
        get => (float) GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    
    public readonly static BindableProperty MaxValueProperty = BindableProperty.Create(nameof(MaxValue),
             typeof(float),
             typeof(RawValueLabel),
             defaultValue: 1.0f,
             propertyChanged: Refresh);

    public float MaxValue
    {
        get => (float) GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    public readonly static BindableProperty MinValueProperty = BindableProperty.Create(nameof(MinValue),
             typeof(float),
             typeof(RawValueLabel),
             defaultValue: -1.0f,
             propertyChanged: Refresh);

    public float MinValue
    {
        get => (float) GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }

    public readonly static BindableProperty ToleranceProperty = BindableProperty.Create(nameof(Tolerance),
             typeof(float),
             typeof(RawValueLabel),
             defaultValue: 0.04f,
             propertyChanged: Refresh);

    public float Tolerance
    {
        get => (float) GetValue(ToleranceProperty);
        set => SetValue(ToleranceProperty, value);
    }
}