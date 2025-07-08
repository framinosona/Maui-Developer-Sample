namespace Maui_Developer_Sample.Services;

/// <summary>
/// Service for accessing device compass data in real-time.
/// Provides magnetic north heading in degrees, calculated from magnetometer data.
/// </summary>
/// <remarks>
/// The compass combines magnetometer readings to determine the device's orientation
/// relative to magnetic north. This is different from true (geographic) north.
///
/// HEADING VALUES:
/// - 0° = Magnetic North
/// - 90° = East
/// - 180° = South
/// - 270° = West
/// - Values range from 0° to 360°
///
/// ACCURACY CONSIDERATIONS:
/// - Magnetic declination varies by geographic location
/// - Magnetic interference from electronics affects accuracy
/// - Indoor environments may have inconsistent readings
/// - Calibration may be required for optimal accuracy
///
/// APPLICATIONS:
/// - Navigation apps and maps
/// - Augmented reality overlays
/// - Location-based services
/// - Compass widgets and tools
/// - Directional gaming controls
/// </remarks>
public class CompassSensorService : BaseSensorService<CompassData>, IDisposable
{
    /// <summary>
    /// Gets a value indicating whether the compass is supported on this device.
    /// </summary>
    /// <value>
    /// <c>true</c> if compass functionality is available; otherwise, <c>false</c>.
    /// Requires magnetometer hardware and may not be available on all devices.
    /// </value>
    public override bool IsSupported => Compass.Default.IsSupported;

    /// <summary>
    /// Gets a value indicating whether the compass is currently active and providing data.
    /// </summary>
    /// <value>
    /// <c>true</c> if the compass is running and sending updates; otherwise, <c>false</c>.
    /// </value>
    public override bool IsMonitoring => Compass.Default.IsMonitoring;

    /// <summary>
    /// Initializes a new instance of the CompassSensorService class.
    /// Automatically subscribes to compass events and sets sensor speed to UI-optimized frequency.
    /// </summary>
    public CompassSensorService() : base(SensorSpeed.UI)
    {
        Compass.Default.ReadingChanged += OnReadingChanged;
    }

    /// <summary>
    /// Releases all resources used by the CompassSensorService.
    /// Automatically unsubscribes from events and stops the sensor if running.
    /// </summary>
    public void Dispose()
    {
        Compass.Default.ReadingChanged -= OnReadingChanged;
        StopIfNeeded();
    }

    /// <summary>
    /// Handles compass reading changes and notifies all registered listeners.
    /// Automatically marshals the data to the main thread for UI updates.
    /// </summary>
    /// <param name="sender">The compass instance that generated the event.</param>
    /// <param name="e">The event arguments containing the new compass reading.</param>
    private void OnReadingChanged(object? sender, CompassChangedEventArgs e)
    {
        NotifyListeners(e.Reading);
    }

    /// <summary>
    /// Starts the compass if it's not already running.
    /// Uses the configured sensor speed for optimal performance.
    /// </summary>
    protected override void StartIfNeeded()
    {
        if (IsMonitoring)
            return;

        Compass.Default.Start(SensorSpeed);
    }

    /// <summary>
    /// Stops the compass if it's currently running.
    /// Safe to call multiple times - no effect if already stopped.
    /// </summary>
    protected override void StopIfNeeded()
    {
        if (!IsMonitoring)
            return;

        Compass.Default.Stop();
    }

    /// <summary>
    /// Returns a string representation of this sensor service.
    /// </summary>
    /// <returns>The name "Compass" for identification purposes.</returns>
    public override string ToString()
    {
        return nameof(Compass);
    }
}
