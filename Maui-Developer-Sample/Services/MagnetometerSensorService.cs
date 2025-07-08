namespace Maui_Developer_Sample.Services;

/// <summary>
/// Service for accessing device magnetometer data in real-time.
/// Measures magnetic field strength in microtesla (μT) along each axis.
/// </summary>
/// <remarks>
/// The magnetometer detects magnetic fields, primarily Earth's magnetic field for compass functionality.
/// Values can vary significantly based on environment and magnetic interference.
///
/// COORDINATE SYSTEM:
/// - X-axis: Horizontal magnetic field component (east/west)
/// - Y-axis: Horizontal magnetic field component (north/south)
/// - Z-axis: Vertical magnetic field component (up/down)
///
/// TYPICAL VALUES (Earth's magnetic field):
/// - Natural environment: ±25 to ±65 μT total field strength
/// - X/Y components: ±50 μT (varies by location and orientation)
/// - Z component: ±60 μT (varies significantly by latitude)
///
/// INTERFERENCE SOURCES:
/// - Values >±100 μT often indicate magnetic interference
/// - Electronics, metal objects, magnets can cause interference
/// - Indoor environments typically show higher, irregular values
///
/// APPLICATIONS:
/// - Compass functionality
/// - Metal detection
/// - Magnetic field mapping
/// - Orientation sensing (combined with accelerometer)
/// </remarks>
public class MagnetometerSensorService : BaseSensorService<MagnetometerData>, IDisposable
{
    /// <summary>
    /// Gets a value indicating whether the magnetometer is supported on this device.
    /// </summary>
    /// <value>
    /// <c>true</c> if magnetometer hardware is available; otherwise, <c>false</c>.
    /// Most smartphones include magnetometer for compass functionality.
    /// </value>
    public override bool IsSupported => Magnetometer.Default.IsSupported;

    /// <summary>
    /// Gets a value indicating whether the magnetometer is currently active and providing data.
    /// </summary>
    /// <value>
    /// <c>true</c> if the magnetometer is running and sending updates; otherwise, <c>false</c>.
    /// </value>
    public override bool IsMonitoring => Magnetometer.Default.IsMonitoring;

    /// <summary>
    /// Initializes a new instance of the MagnetometerSensorService class.
    /// Automatically subscribes to magnetometer events and sets sensor speed to UI-optimized frequency.
    /// </summary>
    public MagnetometerSensorService() : base(SensorSpeed.UI)
    {
        Magnetometer.Default.ReadingChanged += OnReadingChanged;
    }

    /// <summary>
    /// Releases all resources used by the MagnetometerSensorService.
    /// Automatically unsubscribes from events and stops the sensor if running.
    /// </summary>
    public void Dispose()
    {
        Magnetometer.Default.ReadingChanged -= OnReadingChanged;
        StopIfNeeded();
    }

    /// <summary>
    /// Handles magnetometer reading changes and notifies all registered listeners.
    /// Automatically marshals the data to the main thread for UI updates.
    /// </summary>
    /// <param name="sender">The magnetometer instance that generated the event.</param>
    /// <param name="e">The event arguments containing the new magnetometer reading.</param>
    private void OnReadingChanged(object? sender, MagnetometerChangedEventArgs e)
    {
        NotifyListeners(e.Reading);
    }

    /// <summary>
    /// Starts the magnetometer if it's not already running.
    /// Uses the configured sensor speed for optimal performance.
    /// </summary>
    protected override void StartIfNeeded()
    {
        if (IsMonitoring)
            return;

        Magnetometer.Default.Start(SensorSpeed);
    }

    /// <summary>
    /// Stops the magnetometer if it's currently running.
    /// Safe to call multiple times - no effect if already stopped.
    /// </summary>
    protected override void StopIfNeeded()
    {
        if (!IsMonitoring)
            return;

        Magnetometer.Default.Stop();
    }

    /// <summary>
    /// Returns a string representation of this sensor service.
    /// </summary>
    /// <returns>The name "Magnetometer" for identification purposes.</returns>
    public override string ToString()
    {
        return nameof(Magnetometer);
    }
}
