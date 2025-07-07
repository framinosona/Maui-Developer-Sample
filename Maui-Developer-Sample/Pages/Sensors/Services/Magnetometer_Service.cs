using System.Diagnostics;

namespace Maui_Developer_Sample.Pages.Sensors.Services;

/// <summary>
/// Service for accessing device magnetometer data in real-time.
/// Measures magnetic field strength in three dimensions, primarily Earth's magnetic field.
/// </summary>
/// <remarks>
/// The magnetometer detects magnetic fields in microtesla (μT) units.
/// Primarily used for compass functionality and metal detection.
///
/// EARTH'S MAGNETIC FIELD:
/// - Total field strength: ~25-65 μT depending on location
/// - Horizontal component: ~15-40 μT (used for compass)
/// - Vertical component: ~15-60 μT (varies by latitude)
///
/// COORDINATE SYSTEM:
/// - X-axis: Horizontal, pointing right when device upright
/// - Y-axis: Horizontal, pointing up when device upright
/// - Z-axis: Vertical, pointing out of screen when device upright
///
/// INTERFERENCE SOURCES:
/// - Permanent magnets: Strong local distortion
/// - Electronics: Speakers, motors, transformers
/// - Metal structures: Buildings, vehicles, appliances
/// - Electromagnetic fields: Power lines, wireless chargers
///
/// TYPICAL VALUES:
/// - In natural environment: X,Y,Z components typically ±50 μT
/// - Near magnetic interference: Can exceed ±200 μT
/// - Metal detection: Sudden changes >10 μT from baseline
/// </remarks>
public class Magnetometer_Service : BaseBindableSensor_Service
{
    public override bool IsSupported => Magnetometer.IsSupported;

    /// <summary>
    /// Magnetic field strength along the X-axis in microtesla (μT).
    /// </summary>
    /// <value>
    /// Range: Typically -50 to +50 μT in natural environment
    ///
    /// Earth's field component pointing horizontally right when device upright
    /// Positive: Magnetic field pointing right
    /// Negative: Magnetic field pointing left
    /// Zero: No horizontal X-component of magnetic field
    ///
    /// Values >±100 μT: Likely magnetic interference present
    /// Sudden changes >10 μT: Possible metal objects nearby
    /// </value>
    public float XInMicroTesla
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Magnetic field strength along the Y-axis in microtesla (μT).
    /// </summary>
    /// <value>
    /// Range: Typically -50 to +50 μT in natural environment
    ///
    /// Earth's field component pointing horizontally up when device upright
    /// Positive: Magnetic field pointing up/north (in northern hemisphere)
    /// Negative: Magnetic field pointing down/south
    /// Zero: No horizontal Y-component of magnetic field
    ///
    /// Values >±100 μT: Likely magnetic interference present
    /// Used with X-component for compass calculations
    /// </value>
    public float YInMicroTesla
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Magnetic field strength along the Z-axis in microtesla (μT).
    /// </summary>
    /// <value>
    /// Range: Typically -60 to +60 μT depending on latitude
    ///
    /// Earth's field component pointing vertically (out of/into device screen)
    /// Positive: Magnetic field pointing out of screen (toward user)
    /// Negative: Magnetic field pointing into screen (away from user)
    ///
    /// Varies significantly by geographic latitude:
    /// - Equator: Near zero (horizontal field)
    /// - Poles: Maximum values ±60 μT (vertical field)
    /// - Mid-latitudes: Moderate values ±30 μT
    ///
    /// Values >±100 μT: Likely magnetic interference present
    /// </value>
    public float ZInMicroTesla
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    protected override bool IsSensorMonitoring()
    {
        return Magnetometer.IsMonitoring;
    }

    protected override void SubscribeToSensorEvents()
    {
        Magnetometer.ReadingChanged += OnReadingChanged;
    }

    protected override void UnsubscribeFromSensorEvents()
    {
        Magnetometer.ReadingChanged -= OnReadingChanged;
    }

    protected override void StartSensor(SensorSpeed sensorSpeed)
    {
        Magnetometer.Start(sensorSpeed);
        Debug.WriteLine($"{this} started monitoring. Speed: {sensorSpeed}");
    }

    protected override void StopSensor()
    {
        Magnetometer.Stop();
    }

    public override string ToString()
    {
        return nameof(Magnetometer);
    }

    private void OnReadingChanged(object? sender, MagnetometerChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            XInMicroTesla = e.Reading.MagneticField.X;
            YInMicroTesla = e.Reading.MagneticField.Y;
            ZInMicroTesla = e.Reading.MagneticField.Z;
        });
        Debug.WriteLine($"Magnetometer reading: X={XInMicroTesla} μT, Y={YInMicroTesla} μT, Z={ZInMicroTesla} μT");
    }
}
