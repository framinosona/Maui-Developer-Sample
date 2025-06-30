namespace Maui_Developer_Sample.Pages.Sensors.Services;

public class Accelerometer_Service : BaseBindableSensor_Service
{
    protected override bool IsSupported() => Accelerometer.IsSupported;
    
    protected override bool IsSensorMonitoring() => Accelerometer.IsMonitoring;

    protected override void SubscribeToSensorEvents() => Accelerometer.ReadingChanged += OnReadingChanged;

    protected override void UnsubscribeFromSensorEvents() => Accelerometer.ReadingChanged -= OnReadingChanged;

    protected override void StartSensor() => Accelerometer.Start(SensorSpeed);

    protected override void StopSensor() => Accelerometer.Stop();

    public override string ToString() => nameof(Accelerometer);

    private void OnReadingChanged(object? sender, AccelerometerChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => {
            X = e.Reading.Acceleration.X;
            Y = e.Reading.Acceleration.Y;
            Z = e.Reading.Acceleration.Z;
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