namespace Maui_Developer_Sample.Services;

/// <summary>
/// Service for accessing device accelerometer data in real-time.
/// Measures acceleration forces acting on the device in 3D space, including gravity.
/// </summary>
/// <remarks>
/// The accelerometer measures acceleration in G-forces (1G ≈ 9.8 m/s²).
/// Values include both device movement acceleration and gravity.
///
/// COORDINATE SYSTEM:
/// - X-axis: Horizontal, positive when device tilts right
/// - Y-axis: Vertical, positive when device tilts up
/// - Z-axis: Depth, positive when device face tilts toward user
///
/// TYPICAL VALUES:
/// - At rest flat on table: X≈0, Y≈0, Z≈1 (gravity pulling down)
/// - Held upright: X≈0, Y≈1, Z≈0 (gravity pulling toward bottom)
/// - Values typically range from -3G to +3G during normal use
/// - Can exceed ±3G during vigorous movement or impacts
/// </remarks>
public class AccelerometerSensorService : BaseSensorService<AccelerometerData>, IDisposable
{
    /// <summary>
    /// Gets a value indicating whether the accelerometer is supported on this device.
    /// </summary>
    /// <value>
    /// <c>true</c> if accelerometer hardware is available; otherwise, <c>false</c>.
    /// Most modern mobile devices support accelerometer functionality.
    /// </value>
    public override bool IsSupported => Accelerometer.Default.IsSupported;

    /// <summary>
    /// Gets a value indicating whether the accelerometer is currently active and providing data.
    /// </summary>
    /// <value>
    /// <c>true</c> if the accelerometer is running and sending updates; otherwise, <c>false</c>.
    /// </value>
    public override bool IsMonitoring => Accelerometer.Default.IsMonitoring;

    /// <summary>
    /// Initializes a new instance of the AccelerometerSensorService class.
    /// Automatically subscribes to accelerometer events and sets sensor speed to UI-optimized frequency.
    /// </summary>
    public AccelerometerSensorService() : base(SensorSpeed.UI)
    {
        Accelerometer.Default.ReadingChanged += OnReadingChanged;
    }

    /// <summary>
    /// Releases all resources used by the AccelerometerSensorService.
    /// Automatically unsubscribes from events and stops the sensor if running.
    /// </summary>
    public void Dispose()
    {
        Accelerometer.Default.ReadingChanged -= OnReadingChanged;
        StopIfNeeded();
    }

    /// <summary>
    /// Handles accelerometer reading changes and notifies all registered listeners.
    /// Automatically marshals the data to the main thread for UI updates.
    /// </summary>
    /// <param name="sender">The accelerometer instance that generated the event.</param>
    /// <param name="e">The event arguments containing the new accelerometer reading.</param>
    private void OnReadingChanged(object? sender, AccelerometerChangedEventArgs e)
    {
        NotifyListeners(e.Reading);
    }

    /// <summary>
    /// Starts the accelerometer if it's not already running.
    /// Uses the configured sensor speed for optimal performance.
    /// </summary>
    protected override void StartIfNeeded()
    {
        if (IsMonitoring)
            return;

        Accelerometer.Default.Start(SensorSpeed);
    }

    /// <summary>
    /// Stops the accelerometer if it's currently running.
    /// Safe to call multiple times - no effect if already stopped.
    /// </summary>
    protected override void StopIfNeeded()
    {
        if (!IsMonitoring)
            return;

        Accelerometer.Default.Stop();
    }

    /// <summary>
    /// Returns a string representation of this sensor service.
    /// </summary>
    /// <returns>The name "Accelerometer" for identification purposes.</returns>
    public override string ToString()
    {
        return nameof(Accelerometer);
    }
}
