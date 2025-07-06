namespace Maui_Developer_Sample.Pages.Sensors.Services;

/// <summary>
/// Service for accessing device barometer (atmospheric pressure sensor) data.
/// Measures atmospheric pressure which can be used for altitude estimation and weather monitoring.
/// </summary>
/// <remarks>
/// The barometer measures atmospheric pressure in hectopascals (hPa), also known as millibars (mbar).
///
/// PRESSURE REFERENCE VALUES:
/// - Sea level standard: ~1013.25 hPa (1 atmosphere)
/// - Typical range: 950-1050 hPa depending on weather and altitude
/// - High pressure: >1020 hPa (often clear weather)
/// - Low pressure: <1000 hPa (often stormy weather)
///
/// ALTITUDE RELATIONSHIP:
/// - Pressure decreases ~1 hPa per 8.5m altitude gain
/// - Can be used for relative altitude changes
/// - Absolute altitude requires calibration to current sea level pressure
///
/// WEATHER APPLICATIONS:
/// - Rising pressure: Weather improving
/// - Falling pressure: Weather deteriorating
/// - Rapid changes: Significant weather changes approaching
/// </remarks>
public class Barometer_Service : BaseBindableSensor_Service
{
    public override bool IsSupported => Barometer.Default.IsSupported;

    /// <summary>
    /// Current atmospheric pressure reading in hectopascals (hPa).
    /// </summary>
    /// <value>
    /// Range: Typically 950-1050 hPa on Earth's surface
    ///
    /// Reference values:
    /// - 1013.25 hPa: Standard sea level pressure
    /// - 1000-1020 hPa: Normal pressure range
    /// - >1020 hPa: High pressure (often clear weather)
    /// - <1000 hPa: Low pressure (often stormy weather)
    /// - <950 hPa: Very low pressure (severe weather systems)
    /// - >1050 hPa: Very high pressure (strong high-pressure systems)
    ///
    /// Note: 1 hPa = 1 mbar = 100 Pa
    /// </value>
    public double PressureInHectopascals
    {
        get => GetValue(0);
        set => SetValue(value);
    }

    protected override bool IsSensorMonitoring()
    {
        return Barometer.Default.IsMonitoring;
    }

    protected override void SubscribeToSensorEvents()
    {
        Barometer.Default.ReadingChanged += OnReadingChanged;
    }

    protected override void UnsubscribeFromSensorEvents()
    {
        Barometer.Default.ReadingChanged -= OnReadingChanged;
    }

    protected override void StartSensor()
    {
        Barometer.Default.Start(SensorSpeed);
    }

    protected override void StopSensor()
    {
        Barometer.Default.Stop();
    }

    public override string ToString()
    {
        return nameof(Barometer);
    }

    private void OnReadingChanged(object? sender, BarometerChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => { PressureInHectopascals = e.Reading.PressureInHectopascals; });
    }
}
