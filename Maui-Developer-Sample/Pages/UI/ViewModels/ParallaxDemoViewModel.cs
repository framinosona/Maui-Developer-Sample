using Maui_Developer_Sample.Helpers;
using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.UI.ViewModels;

/// <summary>
/// ViewModel for the parallax demo page.
/// Provides data binding and UI logic for parallax effect demonstrations using gyroscope sensor data.
/// </summary>
/// <remarks>
/// This ViewModel demonstrates advanced UI effects by utilizing gyroscope sensor data
/// to create parallax scrolling and rotation effects in the user interface.
///
/// The parallax effect creates a sense of depth and immersion by moving background
/// elements at different speeds than foreground elements based on device orientation.
///
/// GYROSCOPE INTEGRATION:
/// - Uses gyroscope angular velocity data for smooth rotation effects
/// - Converts radians/second to degrees for UI-friendly values
/// - Provides real-time orientation feedback for parallax calculations
///
/// TYPICAL USE CASES:
/// - Interactive backgrounds that respond to device movement
/// - 3D-like effects in 2D interfaces
/// - Enhanced user experience through motion-based interactions
/// - Gaming or entertainment applications
///
/// PERFORMANCE CONSIDERATIONS:
/// - Optimized for smooth 60fps animations
/// - Uses efficient data binding patterns
/// - Minimal memory allocation during sensor updates
/// </remarks>
public class ParallaxDemoViewModel : EnhancedBindableObject
{
    private readonly GyroscopeSensorService _gyroscopeService;

    /// <summary>
    /// Initializes a new instance of the ParallaxDemoViewModel.
    /// </summary>
    /// <param name="gyroscopeService">The gyroscope sensor service instance.</param>
    public ParallaxDemoViewModel(GyroscopeSensorService gyroscopeService)
    {
        _gyroscopeService = gyroscopeService ?? throw new ArgumentNullException(nameof(gyroscopeService));

        // Initialize status
        UpdateStatus();
    }

    /// <summary>
    /// Gets whether the gyroscope sensor is supported on this device.
    /// </summary>
    /// <value>
    /// true if the device has a gyroscope sensor available;
    /// false if the sensor is not available or not supported.
    /// </value>
    public bool IsSupported => _gyroscopeService.IsSupported;

    /// <summary>
    /// Gets or sets whether the gyroscope sensor is currently monitoring/active.
    /// Setting to true starts the sensor for parallax effects, setting to false stops it.
    /// </summary>
    /// <value>
    /// true if the sensor is actively monitoring and generating readings for parallax effects;
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
    /// Gets the current status of the gyroscope sensor operation.
    /// Provides human-readable information about the sensor state for UI display.
    /// </summary>
    /// <value>
    /// Status messages include:
    /// - "Gyroscope is not started" - Initial state
    /// - "Gyroscope is on" - Successfully monitoring for parallax effects
    /// - "Gyroscope is off" - Stopped monitoring
    /// - "Gyroscope is not supported on this device" - Hardware not available
    /// </value>
    public string Status
    {
        get => GetValue("Not Started");
        protected set => SetValue(value);
    }

    /// <summary>
    /// Gets the angular velocity around the X-axis (roll) in radians per second.
    /// Used for horizontal parallax effects.
    /// </summary>
    /// <value>
    /// Range: Typically -π to +π radians/second
    /// Positive: Device rotating clockwise around X-axis
    /// Negative: Device rotating counter-clockwise around X-axis
    /// Zero: No rotation around X-axis
    /// </value>
    public float XRadians
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the angular velocity around the Y-axis (pitch) in radians per second.
    /// Used for vertical parallax effects.
    /// </summary>
    /// <value>
    /// Range: Typically -π to +π radians/second
    /// Positive: Device rotating clockwise around Y-axis
    /// Negative: Device rotating counter-clockwise around Y-axis
    /// Zero: No rotation around Y-axis
    /// </value>
    public float YRadians
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the angular velocity around the Z-axis (yaw) in radians per second.
    /// Used for rotational parallax effects.
    /// </summary>
    /// <value>
    /// Range: Typically -π to +π radians/second
    /// Positive: Device rotating clockwise around Z-axis
    /// Negative: Device rotating counter-clockwise around Z-axis
    /// Zero: No rotation around Z-axis
    /// </value>
    public float ZRadians
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the angular velocity around the X-axis (roll) in degrees per second.
    /// UI-friendly version of XRadians, commonly used for visual effects.
    /// </summary>
    /// <value>
    /// Range: Typically -180 to +180 degrees/second
    /// Positive: Device rotating clockwise around X-axis
    /// Negative: Device rotating counter-clockwise around X-axis
    /// Zero: No rotation around X-axis
    /// </value>
    public float XDegrees
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the angular velocity around the Y-axis (pitch) in degrees per second.
    /// UI-friendly version of YRadians, commonly used for visual effects.
    /// </summary>
    /// <value>
    /// Range: Typically -180 to +180 degrees/second
    /// Positive: Device rotating clockwise around Y-axis
    /// Negative: Device rotating counter-clockwise around Y-axis
    /// Zero: No rotation around Y-axis
    /// </value>
    public float YDegrees
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the angular velocity around the Z-axis (yaw) in degrees per second.
    /// UI-friendly version of ZRadians, commonly used for visual effects.
    /// </summary>
    /// <value>
    /// Range: Typically -180 to +180 degrees/second
    /// Positive: Device rotating clockwise around Z-axis
    /// Negative: Device rotating counter-clockwise around Z-axis
    /// Zero: No rotation around Z-axis
    /// </value>
    public float ZDegrees
    {
        get => GetValue(0.0f);
        set => SetValue(value);
    }

    /// <summary>
    /// Gets the sensor reading frequency/speed setting.
    /// Affects the smoothness and responsiveness of parallax effects.
    /// </summary>
    /// <value>
    /// SensorSpeed.UI - Updates optimized for user interface (default, ~60Hz)
    /// SensorSpeed.Game - High frequency updates for games (~100Hz)
    /// SensorSpeed.Normal - Standard frequency (~20Hz)
    /// SensorSpeed.Fastest - Maximum frequency available from hardware
    /// </value>
    public SensorSpeed SensorSpeed => _gyroscopeService.SensorSpeed;

    /// <summary>
    /// Handles gyroscope data updates from the service.
    /// Converts raw sensor data into UI-friendly values for parallax effects.
    /// </summary>
    /// <param name="data">The gyroscope sensor data containing angular velocity measurements.</param>
    private void OnGyroscopeDataReceived(GyroscopeData data)
    {
        // Update radian values
        XRadians = data.AngularVelocity.X;
        YRadians = data.AngularVelocity.Y;
        ZRadians = data.AngularVelocity.Z;

        // Convert to degrees for UI (radians * 180 / π)
        XDegrees = XRadians * 57.2958f; // 180/π ≈ 57.2958
        YDegrees = YRadians * 57.2958f;
        ZDegrees = ZRadians * 57.2958f;
    }

    /// <summary>
    /// Updates the status message based on the current sensor state.
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
    /// Returns the display name for this sensor demo.
    /// </summary>
    /// <returns>The name "Parallax Demo".</returns>
    public override string ToString()
    {
        return "Parallax Demo";
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
