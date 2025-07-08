using System.Collections.Concurrent;

namespace Maui_Developer_Sample.Services;

/// <summary>
/// Provides a base implementation for sensor services with automatic lifecycle management.
/// Handles listener registration, sensor starting/stopping, and thread-safe data notification.
/// </summary>
/// <typeparam name="T">The type of sensor data (e.g., AccelerometerData, GyroscopeData).</typeparam>
public abstract class BaseSensorService<T> : BaseBindableAppCapabilityService
{
    /// <summary>
    /// Represents a method that handles sensor data updates.
    /// </summary>
    /// <param name="update">The sensor data update containing the latest readings.</param>
    public delegate void OnUpdateReceived(T update);

    /// <summary>
    /// Initializes a new instance of the BaseSensorService class with the specified sensor speed.
    /// </summary>
    /// <param name="sensorSpeed">The desired sensor reading frequency.</param>
    protected BaseSensorService(SensorSpeed sensorSpeed)
    {
        SensorSpeed = sensorSpeed;
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
    public SensorSpeed SensorSpeed { get; }

    /// <summary>
    /// Gets a value indicating whether the sensor is currently running and providing updates.
    /// </summary>
    public abstract bool IsMonitoring { get; }

    private readonly List<OnUpdateReceived> _listeners = new();

    /// <summary>
    /// Adds a listener to receive sensor data updates.
    /// Automatically starts the sensor if this is the first listener.
    /// </summary>
    /// <param name="listener">The callback method to receive sensor updates.</param>
    /// <exception cref="ArgumentNullException">Thrown when listener is null.</exception>
    /// <exception cref="NotSupportedException">Thrown when sensor is not supported on this device.</exception>
    public void AddListener(OnUpdateReceived listener)
    {
        ArgumentNullException.ThrowIfNull(listener);
        if (IsSupported == false)
            throw new NotSupportedException($"{this} is not supported on this device.");

        if (IsMonitoring == false)
        {
            StartIfNeeded();
        }

        lock (_listeners)
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
    }

    /// <summary>
    /// Removes a listener from receiving sensor data updates.
    /// Automatically stops the sensor if this was the last listener.
    /// </summary>
    /// <param name="listener">The callback method to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown when listener is null.</exception>
    /// <exception cref="NotSupportedException">Thrown when sensor is not supported on this device.</exception>
    public void RemoveListener(OnUpdateReceived listener)
    {
        ArgumentNullException.ThrowIfNull(listener);
        if (IsSupported == false)
            throw new NotSupportedException($"{this} is not supported on this device.");

        bool shouldStop;
        lock (_listeners)
        {
            if (!_listeners.Contains(listener))
                return;

            _listeners.Remove(listener);
            shouldStop = _listeners.Count == 0 && IsMonitoring;
        }
        if (shouldStop)
        {
            StopIfNeeded();
        }
    }

    /// <summary>
    /// Notifies all registered listeners of new sensor data.
    /// Ensures notifications are delivered on the main thread for UI updates.
    /// </summary>
    /// <param name="update">The sensor data to send to listeners.</param>
    /// <exception cref="ArgumentNullException">Thrown when update is null.</exception>
    protected void NotifyListeners(T update)
    {
        ArgumentNullException.ThrowIfNull(update);

        lock (_listeners)
        {
            foreach (var listener in _listeners.ToArray())
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    listener(update);
                });

            }
        }
    }

    /// <summary>
    /// Starts the sensor if it's not already running.
    /// Implementations should check IsMonitoring before starting.
    /// </summary>
    protected abstract void StartIfNeeded();

    /// <summary>
    /// Stops the sensor if it's currently running.
    /// Implementations should check IsMonitoring before stopping.
    /// </summary>
    protected abstract void StopIfNeeded();

    public override string ToString()
    {
        return "Unknown Sensor";
    }
}
