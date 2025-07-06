using Maui_Developer_Sample.Pages.AppCapability.Services;

namespace Maui_Developer_Sample.Pages.Sensors.Services;

/// <summary>
/// Abstract base class for all sensor services in the application.
/// Provides common functionality for sensor lifecycle management, monitoring state, and error handling.
/// </summary>
/// <remarks>
/// This class handles:
/// - Sensor start/stop operations with proper state management
/// - Automatic cleanup of event subscriptions
/// - Error handling with user-friendly status messages
/// - Sensor speed configuration with automatic restart when changed
/// </remarks>
public abstract class BaseBindableSensor_Service : BaseBindableAppCapability_Service
{
    /// <summary>
    /// Initializes a new instance of the sensor service.
    /// Sets the initial state to not monitoring and provides default status message.
    /// </summary>
    protected BaseBindableSensor_Service()
    {
        // Initialize the sensor state
        IsMonitoring = false;
        Status = $"{this} is not started";
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
    public SensorSpeed SensorSpeed
    {
        get => GetValue(SensorSpeed.UI);
        set
        {
            if (SetValue(value))
            {
                if (IsMonitoring)
                {
                    StopIfNeeded();
                    StartIfNeeded();
                }
            }
        }
    }

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
        get => GetValue(false);
        set
        {
            if (!SetValue(value))
                return;

            if (value)
                StartIfNeeded();
            else
                StopIfNeeded();
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

    private void StartIfNeeded()
    {
        if (IsSensorMonitoring())
            return;

        try
        {
            if (!IsSupported)
                throw new NotSupportedException($"{this} is not supported on this device.");

            UnsubscribeFromSensorEvents(); // Ensure no previous subscriptions are active
            SubscribeToSensorEvents();
            StartSensor();
            Status = $"{this} is on";
            IsMonitoring = true;
        }
        catch (Exception ex)
        {
            IsMonitoring = false;
            Status = $"Error: {ex.Message}";
            Console.WriteLine(ex);
        }
    }

    private void StopIfNeeded()
    {
        if (IsSensorMonitoring())
        {
            StopSensor();
            UnsubscribeFromSensorEvents();
        }
        Status = $"{this} is off";
        IsMonitoring = false;
    }

    protected abstract void SubscribeToSensorEvents();

    protected abstract void UnsubscribeFromSensorEvents();

    protected abstract bool IsSensorMonitoring();

    protected abstract void StartSensor();

    protected abstract void StopSensor();

    public override string ToString()
    {
        return "Unknown Sensor";
    }
}
