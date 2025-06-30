namespace Maui_Developer_Sample.Pages.Sensors.Services;

public class Compass_Service : BaseBindableSensor_Service
{
    protected override bool IsSupported() => Compass.IsSupported;

    protected override bool IsSensorMonitoring() => Compass.IsMonitoring;

    protected override void SubscribeToSensorEvents() => Compass.ReadingChanged += OnReadingChanged;

    protected override void UnsubscribeFromSensorEvents() => Compass.ReadingChanged -= OnReadingChanged;

    protected override void StartSensor() => Compass.Start(SensorSpeed);

    protected override void StopSensor() => Compass.Stop();

    public override string ToString() => nameof(Compass);

    private void OnReadingChanged(object? sender, CompassChangedEventArgs e)
    {
        Heading = (float) e.Reading.HeadingMagneticNorth;
    }

    public float Heading
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

}
