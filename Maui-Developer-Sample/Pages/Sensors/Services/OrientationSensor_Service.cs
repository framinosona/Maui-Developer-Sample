using System.Diagnostics;

namespace Maui_Developer_Sample.Pages.Sensors.Services;

/// <summary>
/// Orientation sensor service that provides device orientation data using quaternions.
///
/// QUATERNION EXPLANATION:
/// - W: Scalar component (rotation amount) - represents how much rotation
/// - X: Vector X component - rotation around X-axis (pitch/tilt forward-backward)
/// - Y: Vector Y component - rotation around Y-axis (yaw/turn left-right)
/// - Z: Vector Z component - rotation around Z-axis (roll/tilt left-right)
///
/// COORDINATE SYSTEM:
/// - X-axis: Points right when device is upright
/// - Y-axis: Points up when device is upright
/// - Z-axis: Points out of the screen when device is upright
/// </summary>
public class OrientationSensor_Service : BaseBindableSensor_Service
{
    public override bool IsSupported => OrientationSensor.IsSupported;

    #region Quaternion Properties (Raw Data)

    /// <summary>
    /// W component of quaternion - scalar part representing rotation amount
    /// Range: -1.0 to +1.0
    /// </summary>
    public float W
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// X component of quaternion - rotation around X-axis (pitch)
    /// Range: -1.0 to +1.0
    /// Positive: device tilted forward, Negative: device tilted backward
    /// </summary>
    public float X
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Y component of quaternion - rotation around Y-axis (yaw)
    /// Range: -1.0 to +1.0
    /// Positive: device turned left, Negative: device turned right
    /// </summary>
    public float Y
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Z component of quaternion - rotation around Z-axis (roll)
    /// Range: -1.0 to +1.0
    /// Positive: device tilted left, Negative: device tilted right
    /// </summary>
    public float Z
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    #endregion

    #region Human-Readable Euler Angles (Degrees)

    /// <summary>
    /// Pitch angle in degrees - forward/backward tilt
    /// Range: -90° to +90°
    /// Positive: device tilted forward (top edge down)
    /// Negative: device tilted backward (top edge up)
    /// </summary>
    public double PitchInDegrees
    {
        get => GetValue(0.0);
        private set => SetValue(value);
    }

    /// <summary>
    /// Yaw angle in degrees - left/right rotation (compass heading)
    /// Range: -180° to +180° (or 0° to 360°)
    /// 0°: North, 90°: East, 180°: South, 270°: West
    /// </summary>
    public double YawInDegrees
    {
        get => GetValue(0.0);
        private set => SetValue(value);
    }

    /// <summary>
    /// Roll angle in degrees - left/right tilt
    /// Range: -180° to +180°
    /// Positive: device tilted to the left
    /// Negative: device tilted to the right
    /// </summary>
    public double RollInDegrees
    {
        get => GetValue(0.0);
        private set => SetValue(value);
    }

    #endregion

    protected override bool IsSensorMonitoring()
    {
        return OrientationSensor.IsMonitoring;
    }

    protected override void SubscribeToSensorEvents()
    {
        OrientationSensor.ReadingChanged += OnReadingChanged;
    }

    protected override void UnsubscribeFromSensorEvents()
    {
        OrientationSensor.ReadingChanged -= OnReadingChanged;
    }

    protected override void StartSensor(SensorSpeed sensorSpeed)
    {
        OrientationSensor.Start(sensorSpeed);
        Debug.WriteLine($"{this} started monitoring. Speed: {sensorSpeed}");
    }

    protected override void StopSensor()
    {
        OrientationSensor.Stop();
    }

    public override string ToString()
    {
        return nameof(OrientationSensor);
    }

    private void OnReadingChanged(object? sender, OrientationSensorChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            // Update raw quaternion values
            W = e.Reading.Orientation.W;
            X = e.Reading.Orientation.X;
            Y = e.Reading.Orientation.Y;
            Z = e.Reading.Orientation.Z;

            // Convert to human-readable values
            UpdateEulerAngles();
        });
        Debug.WriteLine($"OrientationSensor reading: W={W}, X={X}, Y={Y}, Z={Z}, " +
                        $"Pitch={PitchInDegrees}°, Yaw={YawInDegrees}°, Roll={RollInDegrees}°");
    }

    #region Quaternion to Euler Conversion Methods

    /// <summary>
    /// Converts quaternion (W,X,Y,Z) to Euler angles (Pitch, Yaw, Roll) in degrees
    /// </summary>
    private void UpdateEulerAngles()
    {
        // Convert quaternion to Euler angles using standard formulas
        var (pitch, yaw, roll) = QuaternionToEulerAngles(W, X, Y, Z);

        PitchInDegrees = RadiansToDegrees(pitch);
        YawInDegrees = RadiansToDegrees(yaw);
        RollInDegrees = RadiansToDegrees(roll);
    }

    /// <summary>
    /// Converts quaternion to Euler angles (in radians)
    /// Returns: (pitch, yaw, roll) in radians
    /// </summary>
    private static (double pitch, double yaw, double roll) QuaternionToEulerAngles(float w, float x, float y, float z)
    {
        // Pitch (X-axis rotation) - forward/backward tilt
        var sinPitch = 2.0 * (w * x + y * z);
        var cosPitch = 1.0 - 2.0 * (x * x + y * y);
        var pitch = Math.Atan2(sinPitch, cosPitch);

        // Yaw (Y-axis rotation) - left/right turn
        var sinYaw = 2.0 * (w * y - z * x);
        var yaw = Math.Abs(sinYaw) >= 1
            ? Math.CopySign(Math.PI / 2, sinYaw) // Use 90 degrees if out of range
            : Math.Asin(sinYaw);

        // Roll (Z-axis rotation) - left/right tilt
        var sinRoll = 2.0 * (w * z + x * y);
        var cosRoll = 1.0 - 2.0 * (y * y + z * z);
        var roll = Math.Atan2(sinRoll, cosRoll);

        return (pitch, yaw, roll);
    }

    /// <summary>
    /// Converts radians to degrees
    /// </summary>
    private static double RadiansToDegrees(double radians)
    {
        return radians * (180.0 / Math.PI);
    }

    #endregion
}
