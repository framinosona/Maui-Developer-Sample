using System.Diagnostics;

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
/// - High pressure: higher than 1020 hPa (often clear weather)
/// - Low pressure: lower than 1000 hPa (often stormy weather)
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
    public override bool IsSupported => Barometer.IsSupported;

    protected override SensorSpeed DefaultSensorSpeed => SensorSpeed.Default;

    /// <summary>
    /// Current atmospheric pressure reading in hectopascals (hPa).
    /// </summary>
    /// <value>
    /// Range: Typically 950-1050 hPa on Earth's surface
    ///
    /// Reference values:
    /// - 1013.25 hPa: Standard sea level pressure
    ///
    /// - lower than 950 hPa: Very low pressure (severe weather systems)
    /// - lower than 1000 hPa: Low pressure (often stormy weather)
    /// - 1000-1020 hPa: Normal pressure range
    /// - higher than 1020 hPa: High pressure (often clear weather)
    /// - higher than 1050 hPa: Very high pressure (strong high-pressure systems)
    ///
    /// Note: 1 hPa = 1 mbar = 100 Pa
    /// </value>
    public double PressureInHectopascals
    {
        get => GetValue(0d);
        set => SetValue(value);
    }

    protected override bool IsSensorMonitoring()
    {
        return Barometer.IsMonitoring;
    }

    protected override void SubscribeToSensorEvents()
    {
        Barometer.ReadingChanged += OnReadingChanged;
    }

    protected override void UnsubscribeFromSensorEvents()
    {
        Barometer.ReadingChanged -= OnReadingChanged;
    }

    protected override void StartSensor(SensorSpeed sensorSpeed)
    {
        Barometer.Start(sensorSpeed);
        Debug.WriteLine($"{this} started monitoring. Speed: {sensorSpeed}");
    }

    protected override void StopSensor()
    {
        Barometer.Stop();
    }

    public override string ToString()
    {
        return nameof(Barometer);
    }

    private void OnReadingChanged(object? sender, BarometerChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => { PressureInHectopascals = e.Reading.PressureInHectopascals; });
        Debug.WriteLine($"Barometer reading: Pressure={PressureInHectopascals} hPa");
    }
}
