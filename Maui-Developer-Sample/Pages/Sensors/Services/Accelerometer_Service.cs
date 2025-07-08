using System.Numerics;

namespace Maui_Developer_Sample.Pages.Sensors.Services;

/// <summary>
/// Service for accessing device accelerometer data in real-time.
/// Measures the acceleration forces acting on the device in 3D space.
/// </summary>
/// <remarks>
/// The accelerometer measures acceleration in G-forces (1G ≈ 9.8 m/s²).
/// Values include both device movement acceleration and gravity.
///
/// COORDINATE SYSTEM:
/// - X-axis: Horizontal, positive values when device tilts right
/// - Y-axis: Vertical, positive values when device tilts up
/// - Z-axis: Depth, positive values when device face tilts toward user
///
/// TYPICAL VALUES:
/// - At rest flat on table: X≈0, Y≈0, Z≈1 (gravity pulling down)
/// - Held upright: X≈0, Y≈1, Z≈0 (gravity pulling toward bottom)
/// - Values typically range from -3G to +3G during normal use
/// - Can exceed ±3G during vigorous movement or impacts
/// </remarks>
public class Accelerometer_Service : BaseBindableSensor_Service
{
    public override bool IsSupported => Accelerometer.Default.IsSupported;

    /// <summary>
    /// Acceleration along the X-axis in G-forces.
    /// </summary>
    /// <value>
    /// Range: Typically -3.0 to +3.0 G (can exceed during impacts)
    /// Positive: Device tilting right or accelerating rightward
    /// Negative: Device tilting left or accelerating leftward
    /// Zero: No horizontal acceleration or tilt
    /// </value>
    public float XinG
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Acceleration along the Y-axis in G-forces.
    /// </summary>
    /// <value>
    /// Range: Typically -3.0 to +3.0 G (can exceed during impacts)
    /// Positive: Device tilting up or accelerating upward
    /// Negative: Device tilting down or accelerating downward
    /// Zero: No vertical acceleration or tilt
    /// </value>
    public float YinG
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Acceleration along the Z-axis in G-forces.
    /// </summary>
    /// <value>
    /// Range: Typically -3.0 to +3.0 G (can exceed during impacts)
    /// Positive: Device face tilting toward user or accelerating forward
    /// Negative: Device face tilting away from user or accelerating backward
    /// ~1.0: Device lying flat (gravity effect)
    /// Zero: Device held perpendicular to ground
    /// </value>
    public float ZinG
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    public Vector3 AccelerationVector
    {
        get => GetValue(Vector3.Zero);
        set
        {
            if (SetValue(value))
            {
                XinG = value.X;
                YinG = value.Y;
                ZinG = value.Z;
            }
        }
    }

    protected override bool IsSensorMonitoring()
    {
        return Accelerometer.Default.IsMonitoring;
    }

    protected override void SubscribeToSensorEvents()
    {
        Accelerometer.Default.ReadingChanged += OnReadingChanged;
    }

    protected override void UnsubscribeFromSensorEvents()
    {
        Accelerometer.Default.ReadingChanged -= OnReadingChanged;
    }

    protected override void StartSensor(SensorSpeed sensorSpeed)
    {
        Accelerometer.Default.Start(sensorSpeed);
    }

    protected override void StopSensor()
    {
        Accelerometer.Default.Stop();
    }

    public override string ToString()
    {
        return nameof(Accelerometer);
    }

    private void OnReadingChanged(object? sender, AccelerometerChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            XinG = e.Reading.Acceleration.X;
            YinG = e.Reading.Acceleration.Y;
            ZinG = e.Reading.Acceleration.Z;
        });
    }
}
