using Maui_Developer_Sample.Helpers;
using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.Sensors.ViewModels;

/// <summary>
/// ViewModel for the compass sensor page.
/// Provides data binding and UI logic for compass sensor readings.
/// </summary>
/// <remarks>
/// This ViewModel acts as a bridge between the CompassSensorService
/// and the UI, providing bindable properties for XAML data binding.
///
/// The compass provides heading information relative to magnetic north.
/// Values are expressed in degrees.
///
/// HEADING VALUES:
/// - 0° = North
/// - 90° = East
/// - 180° = South
/// - 270° = West
///
/// TYPICAL USAGE:
/// - Navigation applications
/// - Augmented reality orientation
/// - Map rotation and alignment
/// - Directional indicators
/// </remarks>
public class CompassViewModel : EnhancedBindableObject
{
    private readonly CompassSensorService _compassService;

    /// <summary>
    /// Initializes a new instance of the CompassViewModel.
    /// </summary>
    /// <param name="compassService">The compass sensor service instance.</param>
    public CompassViewModel(CompassSensorService compassService)
    {
        _compassService = compassService ?? throw new ArgumentNullException(nameof(compassService));

        // Initialize status
        UpdateStatus();
    }

    /// <summary>
    /// Gets whether the compass is supported on this device.
    /// </summary>
    public bool IsSupported => _compassService.IsSupported;

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
        get => _compassService.IsMonitoring;
        set
        {
            if (_compassService.IsMonitoring != value)
            {
                if (value)
                {
                    _compassService.AddListener(OnCompassDataReceived);
                    UpdateStatus();
                }
                else
                {
                    _compassService.RemoveListener(OnCompassDataReceived);
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
    /// - "Compass is not started" - Initial state
    /// - "Compass is on" - Successfully monitoring
    /// - "Compass is off" - Stopped monitoring
    /// - "Error: {ErrorMessage}" - When an error occurs
    /// </value>
    public string Status
    {
        get => GetValue("Not Started");
        private set => SetValue(value);
    }

    /// <summary>
    /// The heading in degrees from magnetic north.
    /// </summary>
    /// <value>
    /// Range: 0.0 to 360.0 degrees
    /// 0° = North
    /// 90° = East
    /// 180° = South
    /// 270° = West
    /// </value>
    public double HeadingMagneticNorth
    {
        get => GetValue(0.0);
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
    public SensorSpeed SensorSpeed => _compassService.SensorSpeed;

    /// <summary>
    /// Gets the compass heading as a formatted string.
    /// </summary>
    /// <value>A formatted string showing the heading in degrees with direction.</value>
    public string HeadingDisplay => $"{HeadingMagneticNorth:F1}° {GetCardinalDirection()}";

    /// <summary>
    /// Gets the cardinal direction based on the current heading.
    /// </summary>
    /// <returns>The cardinal direction (N, NE, E, SE, S, SW, W, NW).</returns>
    public string GetCardinalDirection()
    {
        return HeadingMagneticNorth switch
        {
            >= 337.5 or < 22.5 => "N",
            >= 22.5 and < 67.5 => "NE",
            >= 67.5 and < 112.5 => "E",
            >= 112.5 and < 157.5 => "SE",
            >= 157.5 and < 202.5 => "S",
            >= 202.5 and < 247.5 => "SW",
            >= 247.5 and < 292.5 => "W",
            >= 292.5 and < 337.5 => "NW",
            _ => "N"
        };
    }

    /// <summary>
    /// Returns the display name for this sensor.
    /// </summary>
    /// <returns>The name "Compass".</returns>
    public override string ToString()
    {
        return "Compass";
    }

    /// <summary>
    /// Handles compass data updates from the service.
    /// </summary>
    /// <param name="data">The compass data.</param>
    private void OnCompassDataReceived(CompassData data)
    {
        HeadingMagneticNorth = data.HeadingMagneticNorth;
        OnPropertyChanged(nameof(HeadingDisplay));
    }

    /// <summary>
    /// Updates the status based on the current sensor state.
    /// </summary>
    private void UpdateStatus()
    {
        if (!IsSupported)
        {
            Status = "Compass is not supported on this device";
        }
        else if (IsMonitoring)
        {
            Status = "Compass is on";
        }
        else
        {
            Status = "Compass is not started";
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
            _compassService.RemoveListener(OnCompassDataReceived);
        }
    }
}
