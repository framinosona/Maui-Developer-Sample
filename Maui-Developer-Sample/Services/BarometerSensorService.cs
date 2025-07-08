namespace Maui_Developer_Sample.Services;

/// <summary>
/// Service for accessing device barometer data in real-time.
/// Measures atmospheric pressure in hectopascals (hPa), also known as millibars (mbar).
/// </summary>
/// <remarks>
/// The barometer measures air pressure, which varies with altitude and weather conditions.
/// This sensor is less common than others and may not be available on all devices.
///
/// PRESSURE VALUES:
/// - Sea level standard: ~1013.25 hPa (1 atmosphere)
/// - Typical range: 950-1050 hPa at sea level
/// - Higher altitude = lower pressure (~12 hPa decrease per 100m elevation)
/// - Weather variations: ±30 hPa typical range
///
/// ALTITUDE ESTIMATION:
/// - Can estimate relative altitude changes
/// - Absolute altitude requires reference pressure calibration
/// - Accuracy affected by weather changes
///
/// APPLICATIONS:
/// - Weather monitoring and prediction
/// - Altitude estimation and tracking
/// - Fitness apps (stair climbing, hiking)
/// - Aviation and navigation tools
/// - Environmental monitoring
/// </remarks>
public class BarometerSensorService : BaseSensorService<BarometerData>, IDisposable
{
    /// <summary>
    /// Gets a value indicating whether the barometer is supported on this device.
    /// </summary>
    /// <value>
    /// <c>true</c> if barometer hardware is available; otherwise, <c>false</c>.
    /// Barometers are less common than other sensors and may not be present on all devices.
    /// </value>
    public override bool IsSupported => Barometer.Default.IsSupported;

    /// <summary>
    /// Gets a value indicating whether the barometer is currently active and providing data.
    /// </summary>
    /// <value>
    /// <c>true</c> if the barometer is running and sending updates; otherwise, <c>false</c>.
    /// </value>
    public override bool IsMonitoring => Barometer.Default.IsMonitoring;

    /// <summary>
    /// Initializes a new instance of the BarometerSensorService class.
    /// Automatically subscribes to barometer events and sets sensor speed to normal frequency
    /// (barometer readings don't need high frequency updates like motion sensors).
    /// </summary>
    public BarometerSensorService() : base(SensorSpeed.Default)
    {
        Barometer.Default.ReadingChanged += OnReadingChanged;
    }

    /// <summary>
    /// Releases all resources used by the BarometerSensorService.
    /// Automatically unsubscribes from events and stops the sensor if running.
    /// </summary>
    public void Dispose()
    {
        Barometer.Default.ReadingChanged -= OnReadingChanged;
        StopIfNeeded();
    }

    /// <summary>
    /// Handles barometer reading changes and notifies all registered listeners.
    /// Automatically marshals the data to the main thread for UI updates.
    /// </summary>
    /// <param name="sender">The barometer instance that generated the event.</param>
    /// <param name="e">The event arguments containing the new barometer reading.</param>
    private void OnReadingChanged(object? sender, BarometerChangedEventArgs e)
    {
        NotifyListeners(e.Reading);
    }

    /// <summary>
    /// Starts the barometer if it's not already running.
    /// Uses the configured sensor speed for optimal performance.
    /// </summary>
    protected override void StartIfNeeded()
    {
        if (IsMonitoring)
            return;

        Barometer.Default.Start(SensorSpeed);
    }

    /// <summary>
    /// Stops the barometer if it's currently running.
    /// Safe to call multiple times - no effect if already stopped.
    /// </summary>
    protected override void StopIfNeeded()
    {
        if (!IsMonitoring)
            return;

        Barometer.Default.Stop();
    }

    /// <summary>
    /// Returns a string representation of this sensor service.
    /// </summary>
    /// <returns>The name "Barometer" for identification purposes.</returns>
    public override string ToString()
    {
        return nameof(Barometer);
    }
}
