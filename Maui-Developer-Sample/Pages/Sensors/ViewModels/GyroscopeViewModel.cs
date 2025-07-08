using System.Numerics;
using Maui_Developer_Sample.Helpers;
using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.Sensors.ViewModels;

/// <summary>
/// ViewModel for the gyroscope sensor page.
/// Provides data binding and UI logic for gyroscope sensor readings.
/// </summary>
/// <remarks>
/// This ViewModel acts as a bridge between the GyroscopeSensorService
/// and the UI, providing bindable properties for XAML data binding.
///
/// The gyroscope measures angular velocity (rotational speed) around three axes.
/// Values are expressed in radians per second (rad/s).
///
/// COORDINATE SYSTEM:
/// - X-axis: Roll - rotation around the horizontal axis (pitch up/down)
/// - Y-axis: Pitch - rotation around the vertical axis (yaw left/right)
/// - Z-axis: Yaw - rotation around the depth axis (roll left/right)
///
/// TYPICAL VALUES:
/// - At rest: All values near 0.0 rad/s
/// - Slow rotation: ±0.1 to ±1.0 rad/s
/// - Fast rotation: ±1.0 to ±5.0 rad/s
/// - Very fast rotation: Can exceed ±5.0 rad/s
/// </remarks>
public class GyroscopeViewModel : EnhancedBindableObject
{
    private readonly GyroscopeSensorService _gyroscopeService;

    /// <summary>
    /// Initializes a new instance of the GyroscopeViewModel.
    /// </summary>
    /// <param name="gyroscopeService">The gyroscope sensor service instance.</param>
    public GyroscopeViewModel(GyroscopeSensorService gyroscopeService)
    {
        _gyroscopeService = gyroscopeService ?? throw new ArgumentNullException(nameof(gyroscopeService));

        // Initialize status
        UpdateStatus();
    }

    /// <summary>
    /// Gets whether the gyroscope is supported on this device.
    /// </summary>
    public bool IsSupported => _gyroscopeService.IsSupported;

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
        get => _gyroscopeService.IsMonitoring;
        set
        {
            if (_gyroscopeService.IsMonitoring != value)
            {
                if (value)
                {
                    _gyroscopeService.AddListener(OnGyroscopeDataReceived);
                    UpdateStatus();
                }
                else
                {
                    _gyroscopeService.RemoveListener(OnGyroscopeDataReceived);
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
    /// - "Gyroscope is not started" - Initial state
    /// - "Gyroscope is on" - Successfully monitoring
    /// - "Gyroscope is off" - Stopped monitoring
    /// - "Error: {ErrorMessage}" - When an error occurs
    /// </value>
    public string Status
    {
        get => GetValue("Not Started");
        private set => SetValue(value);
    }

    /// <summary>
    /// Angular velocity around the X-axis in radians per second.
    /// </summary>
    /// <value>
    /// Range: Typically -5.0 to +5.0 rad/s (can exceed during very fast rotation)
    /// Positive: Pitching up (device top tilting away from user)
    /// Negative: Pitching down (device top tilting toward user)
    /// Zero: No rotation around X-axis
    /// </value>
    public float XinRadPerSec
    {
        get => GetValue(0.0f);
        private set => SetValue(value);
    }

    /// <summary>
    /// Angular velocity around the X-axis in degrees per second.
    /// </summary>
    /// <value>
    /// Range: Typically -300 to +300 degrees/s (can exceed during very fast rotation)
    /// Positive: Pitching up (device top tilting away from user)
    /// Negative: Pitching down (device top tilting toward user)
    /// Zero: No rotation around X-axis
    /// </value>
    public float XinDegPerSec
    {
        get => GetValue(0.0f);
        private set => SetValue(value);
    }

    /// <summary>
    /// Angular velocity around the Y-axis in radians per second.
    /// </summary>
    /// <value>
    /// Range: Typically -5.0 to +5.0 rad/s (can exceed during very fast rotation)
    /// Positive: Yawing left (device turning counterclockwise when viewed from above)
    /// Negative: Yawing right (device turning clockwise when viewed from above)
    /// Zero: No rotation around Y-axis
    /// </value>
    public float YinRadPerSec
    {
        get => GetValue(0.0f);
        private set => SetValue(value);
    }

    /// <summary>
    /// Angular velocity around the Y-axis in degrees per second.
    /// </summary>
    /// <value>
    /// Range: Typically -300 to +300 degrees/s (can exceed during very fast rotation)
    /// Positive: Yawing left (device turning counterclockwise when viewed from above)
    /// Negative: Yawing right (device turning clockwise when viewed from above)
    /// Zero: No rotation around Y-axis
    /// </value>
    public float YinDegPerSec
    {
        get => GetValue(0.0f);
        private set => SetValue(value);
    }

    /// <summary>
    /// Angular velocity around the Z-axis in radians per second.
    /// </summary>
    /// <value>
    /// Range: Typically -5.0 to +5.0 rad/s (can exceed during very fast rotation)
    /// Positive: Rolling left (device left side tilting up)
    /// Negative: Rolling right (device right side tilting up)
    /// Zero: No rotation around Z-axis
    /// </value>
    public float ZinRadPerSec
    {
        get => GetValue(0.0f);
        private set => SetValue(value);
    }

    /// <summary>
    /// Angular velocity around the Z-axis in degrees per second.
    /// </summary>
    /// <value>
    /// Range: Typically -300 to +300 degrees/s (can exceed during very fast rotation)
    /// Positive: Rolling left (device left side tilting up)
    /// Negative: Rolling right (device right side tilting up)
    /// Zero: No rotation around Z-axis
    /// </value>
    public float ZinDegPerSec
    {
        get => GetValue(0.0f);
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
    public SensorSpeed SensorSpeed => _gyroscopeService.SensorSpeed;

    /// <summary>
    /// Gets the current angular velocity vector.
    /// </summary>
    /// <value>A Vector3 containing the X, Y, and Z angular velocity values in radians per second.</value>
    public Vector3 AngularVelocityVector => new(XinRadPerSec, YinRadPerSec, ZinRadPerSec);

    /// <summary>
    /// Returns the display name for this sensor.
    /// </summary>
    /// <returns>The name "Gyroscope".</returns>
    public override string ToString()
    {
        return "Gyroscope";
    }

    /// <summary>
    /// Handles gyroscope data updates from the service.
    /// </summary>
    /// <param name="data">The gyroscope data.</param>
    private void OnGyroscopeDataReceived(GyroscopeData data)
    {
        XinRadPerSec = data.AngularVelocity.X;
        YinRadPerSec = data.AngularVelocity.Y;
        ZinRadPerSec = data.AngularVelocity.Z;
        XinDegPerSec = MathHelper.ToDegrees(XinRadPerSec);
        YinDegPerSec = MathHelper.ToDegrees(YinRadPerSec);
        ZinDegPerSec = MathHelper.ToDegrees(ZinRadPerSec);
        OnPropertyChanged(nameof(AngularVelocityVector));
    }

    /// <summary>
    /// Updates the status based on the current sensor state.
    /// </summary>
    private void UpdateStatus()
    {
        if (!IsSupported)
        {
            Status = "Gyroscope is not supported on this device";
        }
        else if (IsMonitoring)
        {
            Status = "Gyroscope is on";
        }
        else
        {
            Status = "Gyroscope is not started";
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
            _gyroscopeService.RemoveListener(OnGyroscopeDataReceived);
        }
    }
}
