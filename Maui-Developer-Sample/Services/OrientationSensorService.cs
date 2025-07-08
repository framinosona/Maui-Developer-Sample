namespace Maui_Developer_Sample.Services;

/// <summary>
/// Service for accessing device orientation sensor data in real-time.
/// Provides quaternion representation of device orientation in 3D space.
/// </summary>
/// <remarks>
/// The orientation sensor combines data from accelerometer, gyroscope, and magnetometer
/// to provide a complete 3D orientation using quaternions (W, X, Y, Z components).
///
/// QUATERNION REPRESENTATION:
/// - W: Scalar component (rotation amount)
/// - X: Vector component (rotation around X-axis)
/// - Y: Vector component (rotation around Y-axis)
/// - Z: Vector component (rotation around Z-axis)
/// - All values range from -1.0 to +1.0
/// - Magnitude (W² + X² + Y² + Z²) should equal 1.0
///
/// COORDINATE SYSTEM:
/// - X-axis: Pitch rotation (forward/backward tilt)
/// - Y-axis: Yaw rotation (left/right rotation)
/// - Z-axis: Roll rotation (left/right tilt)
///
/// CONVERSION TO EULER ANGLES:
/// - Quaternions can be converted to pitch, yaw, roll angles
/// - Avoids gimbal lock issues present in Euler angle representations
/// - More computationally efficient for 3D rotations
///
/// APPLICATIONS:
/// - 3D gaming and VR/AR applications
/// - Camera stabilization and smooth rotation
/// - 3D model manipulation and visualization
/// - Advanced motion tracking and gesture recognition
/// - Robotics and drone orientation control
/// </remarks>
public class OrientationSensorService : BaseSensorService<OrientationSensorData>, IDisposable
{
    /// <summary>
    /// Gets a value indicating whether the orientation sensor is supported on this device.
    /// </summary>
    /// <value>
    /// <c>true</c> if orientation sensor functionality is available; otherwise, <c>false</c>.
    /// Requires accelerometer, gyroscope, and optionally magnetometer hardware.
    /// May not be available on older or budget devices.
    /// </value>
    public override bool IsSupported => OrientationSensor.Default.IsSupported;

    /// <summary>
    /// Gets a value indicating whether the orientation sensor is currently active and providing data.
    /// </summary>
    /// <value>
    /// <c>true</c> if the orientation sensor is running and sending updates; otherwise, <c>false</c>.
    /// </value>
    public override bool IsMonitoring => OrientationSensor.Default.IsMonitoring;

    /// <summary>
    /// Initializes a new instance of the OrientationSensorService class.
    /// Automatically subscribes to orientation sensor events and sets sensor speed to UI-optimized frequency.
    /// </summary>
    public OrientationSensorService() : base(SensorSpeed.UI)
    {
        OrientationSensor.Default.ReadingChanged += OnReadingChanged;
    }

    /// <summary>
    /// Releases all resources used by the OrientationSensorService.
    /// Automatically unsubscribes from events and stops the sensor if running.
    /// </summary>
    public void Dispose()
    {
        OrientationSensor.Default.ReadingChanged -= OnReadingChanged;
        StopIfNeeded();
    }

    /// <summary>
    /// Handles orientation sensor reading changes and notifies all registered listeners.
    /// Automatically marshals the data to the main thread for UI updates.
    /// </summary>
    /// <param name="sender">The orientation sensor instance that generated the event.</param>
    /// <param name="e">The event arguments containing the new orientation reading.</param>
    private void OnReadingChanged(object? sender, OrientationSensorChangedEventArgs e)
    {
        NotifyListeners(e.Reading);
    }

    /// <summary>
    /// Starts the orientation sensor if it's not already running.
    /// Uses the configured sensor speed for optimal performance.
    /// </summary>
    protected override void StartIfNeeded()
    {
        if (IsMonitoring)
            return;

        OrientationSensor.Default.Start(SensorSpeed);
    }

    /// <summary>
    /// Stops the orientation sensor if it's currently running.
    /// Safe to call multiple times - no effect if already stopped.
    /// </summary>
    protected override void StopIfNeeded()
    {
        if (!IsMonitoring)
            return;

        OrientationSensor.Default.Stop();
    }

    /// <summary>
    /// Returns a string representation of this sensor service.
    /// </summary>
    /// <returns>The name "OrientationSensor" for identification purposes.</returns>
    public override string ToString()
    {
        return nameof(OrientationSensor);
    }
}
