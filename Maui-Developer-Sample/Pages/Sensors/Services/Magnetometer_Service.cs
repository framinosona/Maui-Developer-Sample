namespace Maui_Developer_Sample.Pages.Sensors.Services;

public class Magnetometer_Service : BaseBindableSensor_Service
{
    protected override bool IsSupported() => Magnetometer.IsSupported;
    
    protected override bool IsSensorMonitoring() => Magnetometer.IsMonitoring;

    protected override void SubscribeToSensorEvents() => Magnetometer.ReadingChanged += OnReadingChanged;

    protected override void UnsubscribeFromSensorEvents() => Magnetometer.ReadingChanged -= OnReadingChanged;

    protected override void StartSensor() => Magnetometer.Start(SensorSpeed);

    protected override void StopSensor() => Magnetometer.Stop();

    public override string ToString() => nameof(Magnetometer);

    private void OnReadingChanged(object? sender, MagnetometerChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => {
            X = e.Reading.MagneticField.X;
            Y = e.Reading.MagneticField.Y;
            Z = e.Reading.MagneticField.Z;
        });
    }

    public float X
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }
    
    public float Y
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }
    
    public float Z
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }
}
