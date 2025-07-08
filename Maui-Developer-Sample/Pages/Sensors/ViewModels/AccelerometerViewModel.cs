using System.Numerics;
using Maui_Developer_Sample.Helpers;
using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.Sensors.ViewModels;

/// <summary>
/// ViewModel for the accelerometer sensor page.
/// Provides data binding and UI logic for accelerometer sensor readings.
/// </summary>
/// <remarks>
/// This ViewModel acts as a bridge between the AccelerometerSensorService
/// and the UI, providing bindable properties for XAML data binding.
///
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
/// </remarks>
public class AccelerometerViewModel : EnhancedBindableObject
{
    private readonly AccelerometerSensorService _accelerometerService;

    /// <summary>
    /// Initializes a new instance of the AccelerometerViewModel.
    /// </summary>
    /// <param name="accelerometerService">The accelerometer sensor service instance.</param>
    public AccelerometerViewModel(AccelerometerSensorService accelerometerService)
    {
        _accelerometerService = accelerometerService ?? throw new ArgumentNullException(nameof(accelerometerService));

        // Initialize status
        UpdateStatus();
    }

    /// <summary>
    /// Gets whether the accelerometer is supported on this device.
    /// </summary>
    public bool IsSupported => _accelerometerService.IsSupported;

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
        get => _accelerometerService.IsMonitoring;
        set
        {
            if (_accelerometerService.IsMonitoring != value)
            {
                if (value)
                {
                    _accelerometerService.AddListener(OnAccelerometerDataReceived);
                    UpdateStatus();
                }
                else
                {
                    _accelerometerService.RemoveListener(OnAccelerometerDataReceived);
                    UpdateStatus();
                }
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Handles accelerometer data updates from the service.
    /// </summary>
    /// <param name="data">The accelerometer data.</param>
    private void OnAccelerometerDataReceived(AccelerometerData data)
    {
        XinG = data.Acceleration.X;
        YinG = data.Acceleration.Y;
        ZinG = data.Acceleration.Z;
        OnPropertyChanged(nameof(AccelerationVector));
    }

    /// <summary>
    /// Updates the status based on the current sensor state.
    /// </summary>
    private void UpdateStatus()
    {
        if (!IsSupported)
        {
            Status = "Accelerometer is not supported on this device";
        }
        else if (IsMonitoring)
        {
            Status = "Accelerometer is on";
        }
        else
        {
            Status = "Accelerometer is not started";
        }
    }

    /// <summary>
    /// Gets the current status of the sensor operation.
    /// Provides human-readable information about the sensor state, including error messages.
    /// </summary>
    /// <value>
    /// Status messages include:
    /// - "{SensorName} is not started" - Initial state
    /// - "{SensorName} is on" - Successfully monitoring
    /// - "{SensorName} is off" - Stopped monitoring
    /// - "Error: {ErrorMessage}" - When an error occurs
    /// </value>
    public string Status
    {
        get => GetValue("Not Started");
        protected set => SetValue(value);
    }

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
    public SensorSpeed SensorSpeed => _accelerometerService.SensorSpeed;

    /// <summary>
    /// Gets the current acceleration vector.
    /// </summary>
    /// <value>A Vector3 containing the X, Y, and Z acceleration values in G-forces.</value>
    public Vector3 AccelerationVector => new(XinG, YinG, ZinG);

    /// <summary>
    /// Returns the display name for this sensor.
    /// </summary>
    /// <returns>The name "Accelerometer".</returns>
    public override string ToString()
    {
        return "Accelerometer";
    }

    /// <summary>
    /// Stops monitoring when the ViewModel is disposed.
    /// </summary>
    /// <param name="disposing">True if disposing managed resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing && IsMonitoring)
        {
            _accelerometerService.RemoveListener(OnAccelerometerDataReceived);
        }
    }
}
