using System.Diagnostics;

namespace Maui_Developer_Sample.Pages.Sensors.Services;

/// <summary>
/// Service for accessing device compass (digital magnetometer-based) data.
/// Provides magnetic north heading and heading change information.
/// </summary>
/// <remarks>
/// The compass uses the device's magnetometer to determine magnetic north direction.
///
/// HEADING SYSTEM:
/// - 0° = Magnetic North
/// - 90° = East
/// - 180° = South
/// - 270° = West
///
/// IMPORTANT NOTES:
/// - Reports MAGNETIC North, not TRUE North
/// - Magnetic declination varies by location (typically 0-20° difference)
/// - Accuracy affected by magnetic interference from metal objects, electronics
/// - Requires device to be reasonably level for accurate readings
/// - Indoor accuracy may be reduced due to building materials
///
/// CALIBRATION:
/// - Most devices auto-calibrate by detecting figure-8 movements
/// - Manual calibration may be needed in high-interference environments
/// </remarks>
public class Compass_Service : BaseBindableSensor_Service
{
    public override bool IsSupported => Compass.IsSupported;

    /// <summary>
    /// Current heading relative to magnetic north in degrees.
    /// </summary>
    /// <value>
    /// Range: 0.0° to 359.9°
    ///
    /// Direction mapping:
    /// - 0°: Magnetic North
    /// - 45°: Northeast
    /// - 90°: East
    /// - 135°: Southeast
    /// - 180°: South
    /// - 225°: Southwest
    /// - 270°: West
    /// - 315°: Northwest
    ///
    /// Accuracy: Typically ±5° in ideal conditions, ±15° in poor conditions
    /// </value>
    public float HeadingInDegrees
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Change in heading since the last reading in degrees.
    /// Useful for detecting rotation direction and speed.
    /// </summary>
    /// <value>
    /// Range: -180° to +180°
    ///
    /// Positive values: Clockwise rotation (turning right)
    /// Negative values: Counter-clockwise rotation (turning left)
    /// Zero: No rotation detected
    ///
    /// Example interpretations:
    /// - +45°: Turned 45° clockwise (right)
    /// - -90°: Turned 90° counter-clockwise (left)
    /// - +180°/-180°: Complete opposite direction
    ///
    /// Note: Normalized to shortest rotation path
    /// </value>
    public float HeadingInDegreesDelta
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    protected override bool IsSensorMonitoring()
    {
        return Compass.IsMonitoring;
    }

    protected override void SubscribeToSensorEvents()
    {
        Compass.ReadingChanged += OnReadingChanged;
    }

    protected override void UnsubscribeFromSensorEvents()
    {
        Compass.ReadingChanged -= OnReadingChanged;
    }

    protected override void StartSensor(SensorSpeed sensorSpeed)
    {
        Compass.Start(sensorSpeed);
        Debug.WriteLine($"{this} started monitoring. Speed: {sensorSpeed}");
    }

    protected override void StopSensor()
    {
        Compass.Stop();
    }

    public override string ToString()
    {
        return nameof(Compass);
    }

    private void OnReadingChanged(object? sender, CompassChangedEventArgs e)
    {
        var newHeading = (float)e.Reading.HeadingMagneticNorth;
        var delta = newHeading - HeadingInDegrees;

        // Normalize delta to [-180, 180]
        if (delta > 180)
            delta -= 360;
        else if (delta < -180)
            delta += 360;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            HeadingInDegrees = newHeading;
            HeadingInDegreesDelta = delta;
        });
        Debug.WriteLine($"Compass reading: Heading={HeadingInDegrees}° Delta={HeadingInDegreesDelta}°");
    }
}
