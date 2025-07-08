using System.Numerics;
using Maui_Developer_Sample.Helpers;
using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.Sensors.ViewModels;

/// <summary>
/// ViewModel for the orientation sensor page.
/// Provides data binding and UI logic for device orientation sensor readings.
/// </summary>
/// <remarks>
/// This ViewModel acts as a bridge between the OrientationSensorService
/// and the UI, providing bindable properties for XAML data binding.
///
/// The orientation sensor provides device rotation information as a quaternion.
/// Quaternions represent 3D rotations and are more stable than Euler angles.
///
/// QUATERNION VALUES:
/// - X, Y, Z: Vector part of the quaternion
/// - W: Scalar part of the quaternion
/// - Range: Each component is typically -1.0 to +1.0
/// - Normalized: X² + Y² + Z² + W² = 1.0
///
/// TYPICAL USAGE:
/// - 3D graphics and games
/// - Augmented reality applications
/// - Motion tracking
/// - Stabilization systems
/// </remarks>
public class OrientationSensorViewModel : EnhancedBindableObject
{
    private readonly OrientationSensorService _orientationService;

    /// <summary>
    /// Initializes a new instance of the OrientationSensorViewModel.
    /// </summary>
    /// <param name="orientationService">The orientation sensor service instance.</param>
    public OrientationSensorViewModel(OrientationSensorService orientationService)
    {
        _orientationService = orientationService ?? throw new ArgumentNullException(nameof(orientationService));

        // Initialize status
        UpdateStatus();
    }

    /// <summary>
    /// Gets whether the orientation sensor is supported on this device.
    /// </summary>
    public bool IsSupported => _orientationService.IsSupported;

    /// <summary>
    /// Gets or sets whether the sensor is currently monitoring/active.
    /// Setting to true starts the sensor, setting to false stops it.
    /// </summary>
    /// <value>
    /// true if the sensor is actively monitoring and generating readings;
    /// false if the sensor is stopped or inactive.
    /// </value>
    public bool IsMonitoring
    {
        get => _orientationService.IsMonitoring;
        set
        {
            if (_orientationService.IsMonitoring != value)
            {
                if (value)
                {
                    _orientationService.AddListener(OnOrientationDataReceived);
                    UpdateStatus();
                }
                else
                {
                    _orientationService.RemoveListener(OnOrientationDataReceived);
                    UpdateStatus();
                }
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets the current status of the sensor operation.
    /// Provides human-readable information about the sensor state, including error messages.
    /// </summary>
    /// <value>
    /// Status messages include:
    /// - "Orientation Sensor is not started" - Initial state
    /// - "Orientation Sensor is on" - Successfully monitoring
    /// - "Orientation Sensor is off" - Stopped monitoring
    /// - "Error: {ErrorMessage}" - When an error occurs
    /// </value>
    public string Status
    {
        get => GetValue("Not Started");
        private set => SetValue(value);
    }

    /// <summary>
    /// The X component of the orientation quaternion.
    /// </summary>
    /// <value>
    /// Range: -1.0 to +1.0
    /// Represents rotation around the X-axis component
    /// </value>
    public float X
    {
        get => GetValue(0.0f);
        private set => SetValue(value);
    }

    /// <summary>
    /// The Y component of the orientation quaternion.
    /// </summary>
    /// <value>
    /// Range: -1.0 to +1.0
    /// Represents rotation around the Y-axis component
    /// </value>
    public float Y
    {
        get => GetValue(0.0f);
        private set => SetValue(value);
    }

    /// <summary>
    /// The Z component of the orientation quaternion.
    /// </summary>
    /// <value>
    /// Range: -1.0 to +1.0
    /// Represents rotation around the Z-axis component
    /// </value>
    public float Z
    {
        get => GetValue(0.0f);
        private set => SetValue(value);
    }

    /// <summary>
    /// The W component of the orientation quaternion.
    /// </summary>
    /// <value>
    /// Range: -1.0 to +1.0
    /// Represents the scalar part of the quaternion
    /// </value>
    public float W
    {
        get => GetValue(1.0f);
        private set => SetValue(value);
    }

    /// <summary>
    /// Gets or sets the sensor reading frequency/speed.
    /// When changed while monitoring, the sensor is automatically restarted with the new speed.
    /// </summary>
    /// <value>
    /// SensorSpeed.UI - Updates optimized for user interface (default, ~60Hz)
    /// SensorSpeed.Game - High frequency updates for games (~100Hz)
    /// SensorSpeed.Normal - Standard frequency (~20Hz)
    /// SensorSpeed.Fastest - Maximum frequency available from hardware
    /// </value>
    public SensorSpeed SensorSpeed => _orientationService.SensorSpeed;

    /// <summary>
    /// Gets the current orientation quaternion.
    /// </summary>
    /// <value>A Quaternion representing the current device orientation.</value>
    public Quaternion OrientationQuaternion => new(X, Y, Z, W);

    /// <summary>
    /// Gets the magnitude of the quaternion (should be ~1.0 for normalized quaternions).
    /// </summary>
    /// <value>The magnitude of the quaternion.</value>
    public float QuaternionMagnitude => OrientationQuaternion.Length();

    /// <summary>
    /// Gets a display string for the quaternion.
    /// </summary>
    /// <value>A formatted string showing the quaternion components.</value>
    public string QuaternionDisplay => $"X:{X:F3} Y:{Y:F3} Z:{Z:F3} W:{W:F3}";

    /// <summary>
    /// Gets the roll angle in degrees (rotation around Z-axis).
    /// </summary>
    public float RollInDegrees
    {
        get
        {
            var quaternion = OrientationQuaternion;
            var roll = MathF.Atan2(2.0f * (quaternion.W * quaternion.Z + quaternion.X * quaternion.Y),
                                  1.0f - 2.0f * (quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z));
            return roll * 180.0f / MathF.PI;
        }
    }

    /// <summary>
    /// Gets the pitch angle in degrees (rotation around X-axis).
    /// </summary>
    public float PitchInDegrees
    {
        get
        {
            var quaternion = OrientationQuaternion;
            var sinp = 2.0f * (quaternion.W * quaternion.X - quaternion.Z * quaternion.Y);
            var pitch = MathF.Abs(sinp) >= 1 ? MathF.CopySign(MathF.PI / 2, sinp) : MathF.Asin(sinp);
            return pitch * 180.0f / MathF.PI;
        }
    }

    /// <summary>
    /// Gets the yaw angle in degrees (rotation around Y-axis).
    /// </summary>
    public float YawInDegrees
    {
        get
        {
            var quaternion = OrientationQuaternion;
            var yaw = MathF.Atan2(2.0f * (quaternion.W * quaternion.Y + quaternion.X * quaternion.Z),
                                 1.0f - 2.0f * (quaternion.X * quaternion.X + quaternion.Y * quaternion.Y));
            return yaw * 180.0f / MathF.PI;
        }
    }

    /// <summary>
    /// Returns the display name for this sensor.
    /// </summary>
    /// <returns>The name "Orientation Sensor".</returns>
    public override string ToString()
    {
        return "Orientation Sensor";
    }

    /// <summary>
    /// Handles orientation data updates from the service.
    /// </summary>
    /// <param name="data">The orientation data.</param>
    private void OnOrientationDataReceived(OrientationSensorData data)
    {
        X = data.Orientation.X;
        Y = data.Orientation.Y;
        Z = data.Orientation.Z;
        W = data.Orientation.W;
        OnPropertyChanged(nameof(OrientationQuaternion));
        OnPropertyChanged(nameof(QuaternionMagnitude));
        OnPropertyChanged(nameof(QuaternionDisplay));
        OnPropertyChanged(nameof(RollInDegrees));
        OnPropertyChanged(nameof(PitchInDegrees));
        OnPropertyChanged(nameof(YawInDegrees));
    }

    /// <summary>
    /// Updates the status based on the current sensor state.
    /// </summary>
    private void UpdateStatus()
    {
        if (!IsSupported)
        {
            Status = "Orientation Sensor is not supported on this device";
        }
        else if (IsMonitoring)
        {
            Status = "Orientation Sensor is on";
        }
        else
        {
            Status = "Orientation Sensor is not started";
        }
    }

    /// <summary>
    /// Stops monitoring when the ViewModel is disposed.
    /// </summary>
    /// <param name="disposing">True if disposing managed resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing && IsMonitoring)
        {
            _orientationService.RemoveListener(OnOrientationDataReceived);
        }
    }
}
