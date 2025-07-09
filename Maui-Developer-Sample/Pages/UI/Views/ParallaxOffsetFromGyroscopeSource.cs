using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.UI.Views;

/// <summary>
/// Parallax offset source that uses gyroscope sensor data to create movement effects.
/// </summary>
public class ParallaxOffsetFromGyroscopeSource : ParallaxOffsetSource, IDisposable
{
    private readonly GyroscopeSensorService _gyroscopeSensorService;

    public ParallaxOffsetFromGyroscopeSource()
    {
        _gyroscopeSensorService = MauiProgram.Services?.GetRequiredService<GyroscopeSensorService>() ?? throw new InvalidOperationException("GyroscopeSensorService is not registered in the service collection.");
        _gyroscopeSensorService.AddListener(OnGyroscopeDataChanged);
    }

    public void Dispose()
    {
        _gyroscopeSensorService.RemoveListener(OnGyroscopeDataChanged);
    }

    private void OnGyroscopeDataChanged(GyroscopeData update)
    {
        double x = 0, y = 0;
        float horizontalMovement = update.AngularVelocity.Y; // Pitch
        float verticalMovement = update.AngularVelocity.X; // Roll

        if (IsCumulative)
        {
            // If cumulative, add the new values to the existing offset
            x = OffsetX + horizontalMovement * Multiplier;
            y = OffsetY + verticalMovement * Multiplier;
        }
        else
        {
            // If not cumulative, just use the new values directly
            x = horizontalMovement * Multiplier;
            y = verticalMovement * Multiplier;
        }

        NotifyListeners(x, y);
    }

    /// <summary>
    /// Gets or sets whether the gyroscope values are cumulative or direct.
    /// </summary>
    public bool IsCumulative
    {
        get => (bool)GetValue(IsCumulativeProperty);
        set => SetValue(IsCumulativeProperty, value);
    }

    /// <summary>
    /// Bindable property for IsCumulative.
    /// </summary>
    public readonly static BindableProperty IsCumulativeProperty = BindableProperty.Create(nameof(IsCumulative), typeof(bool), typeof(ParallaxOffsetFromGyroscopeSource), false);

    /// <summary>
    /// Gets or sets the multiplier for gyroscope values.
    /// </summary>
    public double Multiplier
    {
        get => (double)GetValue(ParallaxGyroscopeMultiplierProperty);
        set => SetValue(ParallaxGyroscopeMultiplierProperty, value);
    }

    /// <summary>
    /// Bindable property for Multiplier.
    /// </summary>
    public readonly static BindableProperty ParallaxGyroscopeMultiplierProperty = BindableProperty.Create(nameof(Multiplier), typeof(double), typeof(ParallaxOffsetFromGyroscopeSource), 0.2d);
}
