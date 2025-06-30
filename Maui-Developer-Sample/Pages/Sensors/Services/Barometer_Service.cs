namespace Maui_Developer_Sample.Pages.Sensors.Services;

public class Barometer_Service : BaseBindableSensor_Service
{
    protected override bool IsSupported() => Barometer.IsSupported;
    
    protected override bool IsSensorMonitoring() => Barometer.IsMonitoring;

    protected override void SubscribeToSensorEvents() => Barometer.ReadingChanged += OnReadingChanged;

    protected override void UnsubscribeFromSensorEvents() => Barometer.ReadingChanged -= OnReadingChanged;

    protected override void StartSensor() => Barometer.Start(SensorSpeed);

    protected override void StopSensor() => Barometer.Stop();

    public override string ToString() => nameof(Barometer);

    private void OnReadingChanged(object? sender, BarometerChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => { Pressure = e.Reading.PressureInHectopascals; });
    }

    public double Pressure
    {
        get => GetValue(0);
        set => SetValue(value);
    }
}
