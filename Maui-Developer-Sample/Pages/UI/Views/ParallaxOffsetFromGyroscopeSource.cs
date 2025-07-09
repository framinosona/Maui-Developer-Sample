using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.UI.Views;

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
        float horizontalMovement = update.AngularVelocity.Y; // Pitch - As if the screwdriver was on top of the phone
        float verticalMovement = update.AngularVelocity.X; // Roll - As if the screwdriver was on the side of the phone
        
        if(IsCumulative)
        {
            // If cumulative, add the new values to the existing offset
            x = OffsetX + horizontalMovement * Multiplier; // Normalize
            y = OffsetY + verticalMovement * Multiplier; // Normalize
        }
        else 
        {
            // If not cumulative, just use the new values directly
            x = horizontalMovement * Multiplier; // Normalize
            y = verticalMovement * Multiplier; // Normalize
        }
        
        // Clamp the values between -1 and 1 inside :
        NotifyListeners(x, y);
    }
    
    public bool IsCumulative
    {
        get => (bool) GetValue(IsCumulativeProperty);
        set => SetValue(IsCumulativeProperty, value);
    }
    
    public readonly static BindableProperty IsCumulativeProperty = BindableProperty.Create(nameof(IsCumulative), typeof(bool), typeof(ParallaxOffsetFromGyroscopeSource), false);

    public double Multiplier
    {
        get => (double) GetValue(ParallaxGyroscopeMultiplierProperty);
        set => SetValue(ParallaxGyroscopeMultiplierProperty, value);
    }

    public readonly static BindableProperty ParallaxGyroscopeMultiplierProperty = BindableProperty.Create(nameof(Multiplier), typeof(double), typeof(ParallaxOffsetFromGyroscopeSource), 0.2d);
}
