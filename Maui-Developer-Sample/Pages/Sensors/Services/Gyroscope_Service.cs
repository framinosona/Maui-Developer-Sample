namespace Maui_Developer_Sample.Pages.Sensors.Services;

/// <summary>
/// Service for accessing device gyroscope data in real-time.
/// Measures angular velocity (rotation rate) around three axes.
/// </summary>
/// <remarks>
/// The gyroscope measures how fast the device is rotating around each axis.
/// Unlike accelerometer, it measures rotation, not linear movement.
///
/// COORDINATE SYSTEM:
/// - X-axis: Rotation around horizontal axis (pitch - nodding up/down)
/// - Y-axis: Rotation around vertical axis (yaw - turning left/right)
/// - Z-axis: Rotation around depth axis (roll - tilting left/right)
///
/// ANGULAR VELOCITY RANGES:
/// - Typical values: -10 to +10 rad/s during normal use
/// - Can exceed ±20 rad/s during rapid movements
/// - Zero: No rotation detected
/// - Positive/Negative: Direction depends on right-hand rule
///
/// APPLICATIONS:
/// - Game controls (steering, looking around)
/// - Stabilization detection
/// - Gesture recognition
/// - Camera shake detection
/// - Virtual reality orientation tracking
/// </remarks>
public class Gyroscope_Service : BaseBindableSensor_Service
{
    public override bool IsSupported => Gyroscope.Default.IsSupported;

    /// <summary>
    /// Angular velocity around the X-axis in radians per second.
    /// Represents pitch rotation (nodding up/down motion).
    /// </summary>
    /// <value>
    /// Range: Typically -10.0 to +10.0 rad/s (can exceed during rapid movement)
    ///
    /// Positive: Rotating forward (top of device moving toward user)
    /// Negative: Rotating backward (top of device moving away from user)
    /// Zero: No rotation around X-axis
    ///
    /// 1 rad/s ≈ 57.3 degrees/second
    /// </value>
    public float XRadians
    {
        get => GetValue(0.0f);
        protected set => SetValue(value);
    }

    /// <summary>
    /// Angular velocity around the Y-axis in radians per second.
    /// Represents yaw rotation (turning left/right motion).
    /// </summary>
    /// <value>
    /// Range: Typically -10.0 to +10.0 rad/s (can exceed during rapid movement)
    ///
    /// Positive: Rotating left (counter-clockwise when viewed from above)
    /// Negative: Rotating right (clockwise when viewed from above)
    /// Zero: No rotation around Y-axis
    ///
    /// 1 rad/s ≈ 57.3 degrees/second
    /// </value>
    public float YRadians
    {
        get => GetValue(0.0f);
        protected set => SetValue(value);
    }

    /// <summary>
    /// Angular velocity around the Z-axis in radians per second.
    /// Represents roll rotation (tilting left/right motion).
    /// </summary>
    /// <value>
    /// Range: Typically -10.0 to +10.0 rad/s (can exceed during rapid movement)
    ///
    /// Positive: Rotating counter-clockwise (left side lifting up)
    /// Negative: Rotating clockwise (right side lifting up)
    /// Zero: No rotation around Z-axis
    ///
    /// 1 rad/s ≈ 57.3 degrees/second
    /// </value>
    public float ZRadians
    {
        get => GetValue(0.0f);
        protected set => SetValue(value);
    }

    /// <summary>
    /// Angular velocity around the X-axis in degrees per second.
    /// Human-readable version of XRadians for easier interpretation.
    /// </summary>
    /// <value>
    /// Range: Typically -573 to +573 degrees/s (can exceed during rapid movement)
    ///
    /// Positive: Pitching forward (top moving toward user)
    /// Negative: Pitching backward (top moving away from user)
    /// Zero: No pitch rotation
    ///
    /// Reference: 90°/s = quarter turn per second
    /// </value>
    public double XDegrees
    {
        get => GetValue(0.0f);
        protected set => SetValue(value);
    }

    /// <summary>
    /// Angular velocity around the Y-axis in degrees per second.
    /// Human-readable version of YRadians for easier interpretation.
    /// </summary>
    /// <value>
    /// Range: Typically -573 to +573 degrees/s (can exceed during rapid movement)
    ///
    /// Positive: Yawing left (counter-clockwise from above)
    /// Negative: Yawing right (clockwise from above)
    /// Zero: No yaw rotation
    ///
    /// Reference: 90°/s = quarter turn per second
    /// </value>
    public double YDegrees
    {
        get => GetValue(0.0f);
        protected set => SetValue(value);
    }

    /// <summary>
    /// Angular velocity around the Z-axis in degrees per second.
    /// Human-readable version of ZRadians for easier interpretation.
    /// </summary>
    /// <value>
    /// Range: Typically -573 to +573 degrees/s (can exceed during rapid movement)
    ///
    /// Positive: Rolling left (counter-clockwise when viewed from behind)
    /// Negative: Rolling right (clockwise when viewed from behind)
    /// Zero: No roll rotation
    ///
    /// Reference: 90°/s = quarter turn per second
    /// </value>
    public double ZDegrees
    {
        get => GetValue(0.0f);
        protected set => SetValue(value);
    }

    protected override bool IsSensorMonitoring()
    {
        return Gyroscope.Default.IsMonitoring;
    }

    protected override void SubscribeToSensorEvents()
    {
        Gyroscope.Default.ReadingChanged += OnReadingChanged;
    }

    protected override void UnsubscribeFromSensorEvents()
    {
        Gyroscope.Default.ReadingChanged -= OnReadingChanged;
    }

    protected override void StartSensor()
    {
        Gyroscope.Default.Start(SensorSpeed);
    }

    protected override void StopSensor()
    {
        Gyroscope.Default.Stop();
    }

    public override string ToString()
    {
        return nameof(Gyroscope);
    }

    private void OnReadingChanged(object? sender, GyroscopeChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            XRadians = e.Reading.AngularVelocity.X;
            XDegrees = RadianToDegree(e.Reading.AngularVelocity.X);
            YRadians = e.Reading.AngularVelocity.Y;
            YDegrees = RadianToDegree(e.Reading.AngularVelocity.Y);
            ZRadians = e.Reading.AngularVelocity.Z;
            ZDegrees = RadianToDegree(e.Reading.AngularVelocity.Z);
        });
    }

    /// <summary>
    /// Converts angular velocity from radians per second to degrees per second.
    /// </summary>
    /// <param name="radians">Angular velocity in radians per second</param>
    /// <returns>Angular velocity in degrees per second</returns>
    /// <remarks>
    /// Conversion formula: degrees = radians × (180 / π)
    /// Where π ≈ 3.14159
    /// </remarks>
    private double RadianToDegree(float radians)
    {
        return radians * (180.0 / Math.PI);
    }
}
