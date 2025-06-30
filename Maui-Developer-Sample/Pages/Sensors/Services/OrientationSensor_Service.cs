namespace Maui_Developer_Sample.Pages.Sensors.Services;

public class OrientationSensor_Service : BaseBindableSensor_Service
{
    protected override bool IsSupported() => OrientationSensor.IsSupported;
    
    protected override bool IsSensorMonitoring() => OrientationSensor.IsMonitoring;

    protected override void SubscribeToSensorEvents() => OrientationSensor.ReadingChanged += OnReadingChanged;

    protected override void UnsubscribeFromSensorEvents() => OrientationSensor.ReadingChanged -= OnReadingChanged;

    protected override void StartSensor() => OrientationSensor.Start(SensorSpeed);

    protected override void StopSensor() => OrientationSensor.Stop();

    public override string ToString() => nameof(OrientationSensor);
    
    private void OnReadingChanged(object? sender, OrientationSensorChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => {
            W = e.Reading.Orientation.W;
            X = e.Reading.Orientation.X;
            Y = e.Reading.Orientation.Y;
            Z = e.Reading.Orientation.Z;
        });
    }
    
    public float W
    {
        get => GetValue(0.0f);
        set => SetValue(value);
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
