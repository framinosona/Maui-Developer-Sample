using System.Numerics;

namespace Maui_Developer_Sample.Services;

/// <summary>
/// Service for accessing device gyroscope data in real-time.
/// Measures angular velocity (rotation rate) around each axis in radians per second.
/// </summary>
/// <remarks>
/// The gyroscope measures rotation speed, not absolute orientation.
/// Values represent how fast the device is rotating around each axis.
///
/// COORDINATE SYSTEM:
/// - X-axis: Pitch rotation (forward/backward tilt)
/// - Y-axis: Yaw rotation (left/right turn)
/// - Z-axis: Roll rotation (left/right tilt)
///
/// TYPICAL VALUES:
/// - At rest: All values near 0 rad/s
/// - Slow rotation: ±0.1 to ±1.0 rad/s
/// - Fast rotation: ±1.0 to ±10.0 rad/s
/// - Maximum values: Can exceed ±20 rad/s during rapid movement
///
/// APPLICATIONS:
/// - Camera stabilization
/// - VR/AR orientation tracking
/// - Game controls (steering, aiming)
/// - Motion gesture detection
/// </remarks>
public class GyroscopeSensorService : BaseSensorService<GyroscopeData>, IDisposable
{
    /// <summary>
    /// Gets a value indicating whether the gyroscope is supported on this device.
    /// </summary>
    /// <value>
    /// <c>true</c> if gyroscope hardware is available; otherwise, <c>false</c>.
    /// Most modern smartphones have gyroscopes, but some budget devices may not.
    /// </value>
    public override bool IsSupported => Gyroscope.Default.IsSupported;

    /// <summary>
    /// Gets a value indicating whether the gyroscope is currently active and providing data.
    /// </summary>
    /// <value>
    /// <c>true</c> if the gyroscope is running and sending updates; otherwise, <c>false</c>.
    /// </value>
    public override bool IsMonitoring => Gyroscope.Default.IsMonitoring;

    /// <summary>
    /// Initializes a new instance of the GyroscopeSensorService class.
    /// Automatically subscribes to gyroscope events and sets sensor speed to UI-optimized frequency.
    /// </summary>
    public GyroscopeSensorService() : base(SensorSpeed.UI)
    {
        Gyroscope.Default.ReadingChanged += OnReadingChanged;
    }

    /// <summary>
    /// Releases all resources used by the GyroscopeSensorService.
    /// Automatically unsubscribes from events and stops the sensor if running.
    /// </summary>
    public void Dispose()
    {
        Gyroscope.Default.ReadingChanged -= OnReadingChanged;
        StopIfNeeded();
    }

    /// <summary>
    /// Handles gyroscope reading changes and notifies all registered listeners.
    /// Automatically marshals the data to the main thread for UI updates.
    /// </summary>
    /// <param name="sender">The gyroscope instance that generated the event.</param>
    /// <param name="e">The event arguments containing the new gyroscope reading.</param>
    private void OnReadingChanged(object? sender, GyroscopeChangedEventArgs e)
    {
        NotifyListeners(e.Reading);
    }


    /// <summary>
    /// Starts the gyroscope if it's not already running.
    /// Uses the configured sensor speed for optimal performance.
    /// </summary>
    protected override void StartIfNeeded()
    {
        if (IsMonitoring)
            return;

        Gyroscope.Default.Start(SensorSpeed);
    }

    /// <summary>
    /// Stops the gyroscope if it's currently running.
    /// Safe to call multiple times - no effect if already stopped.
    /// </summary>
    protected override void StopIfNeeded()
    {
        if (!IsMonitoring)
            return;

        Gyroscope.Default.Stop();
    }

    /// <summary>
    /// Returns a string representation of this sensor service.
    /// </summary>
    /// <returns>The name "Gyroscope" for identification purposes.</returns>
    public override string ToString()
    {
        return nameof(Gyroscope);
    }
}
