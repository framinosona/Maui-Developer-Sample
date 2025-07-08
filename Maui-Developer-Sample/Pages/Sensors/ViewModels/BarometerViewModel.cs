using Maui_Developer_Sample.Helpers;
using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.Sensors.ViewModels;

/// <summary>
/// ViewModel for the barometer sensor page.
/// Provides data binding and UI logic for barometer sensor readings.
/// </summary>
/// <remarks>
/// This ViewModel acts as a bridge between the BarometerSensorService
/// and the UI, providing bindable properties for XAML data binding.
///
/// The barometer measures atmospheric pressure.
/// Values are expressed in hectopascals (hPa).
///
/// PRESSURE VALUES:
/// - Standard atmospheric pressure: 1013.25 hPa (at sea level)
/// - Typical range: 950-1050 hPa
/// - High pressure: >1020 hPa (usually fair weather)
/// - Low pressure: <1000 hPa (usually stormy weather)
/// - Pressure decreases with altitude (~12 hPa per 100m)
///
/// TYPICAL USAGE:
/// - Weather prediction
/// - Altitude estimation
/// - Atmospheric pressure monitoring
/// - Weather station applications
/// </remarks>
public class BarometerViewModel : EnhancedBindableObject
{
    private readonly BarometerSensorService _barometerService;

    /// <summary>
    /// Initializes a new instance of the BarometerViewModel.
    /// </summary>
    /// <param name="barometerService">The barometer sensor service instance.</param>
    public BarometerViewModel(BarometerSensorService barometerService)
    {
        _barometerService = barometerService ?? throw new ArgumentNullException(nameof(barometerService));

        // Initialize status
        UpdateStatus();
    }

    /// <summary>
    /// Gets whether the barometer is supported on this device.
    /// </summary>
    public bool IsSupported => _barometerService.IsSupported;

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
        get => _barometerService.IsMonitoring;
        set
        {
            if (_barometerService.IsMonitoring != value)
            {
                if (value)
                {
                    _barometerService.AddListener(OnBarometerDataReceived);
                    UpdateStatus();
                }
                else
                {
                    _barometerService.RemoveListener(OnBarometerDataReceived);
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
    /// - "Barometer is not started" - Initial state
    /// - "Barometer is on" - Successfully monitoring
    /// - "Barometer is off" - Stopped monitoring
    /// - "Error: {ErrorMessage}" - When an error occurs
    /// </value>
    public string Status
    {
        get => GetValue("Not Started");
        private set => SetValue(value);
    }

    /// <summary>
    /// The atmospheric pressure in hectopascals.
    /// </summary>
    /// <value>
    /// Range: Typically 950-1050 hPa
    /// Standard: 1013.25 hPa (at sea level)
    /// High: >1020 hPa (fair weather)
    /// Low: <1000 hPa (stormy weather)
    /// </value>
    public double PressureInHectopascals
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
    public SensorSpeed SensorSpeed => _barometerService.SensorSpeed;

    /// <summary>
    /// Gets the pressure formatted for display.
    /// </summary>
    /// <value>A formatted string showing the pressure in hPa.</value>
    public string PressureDisplay => $"{PressureInHectopascals:F2} hPa";

    /// <summary>
    /// Gets the pressure in millibars (equivalent to hectopascals).
    /// </summary>
    /// <value>The pressure in millibars (1 hPa = 1 mbar).</value>
    public double PressureInMillibars => PressureInHectopascals;

    /// <summary>
    /// Gets the pressure in inches of mercury.
    /// </summary>
    /// <value>The pressure in inches of mercury (1 hPa = 0.02953 inHg).</value>
    public double PressureInInchesHg => PressureInHectopascals * 0.02953;

    /// <summary>
    /// Gets the pressure condition based on the current reading.
    /// </summary>
    /// <returns>The pressure condition (High, Normal, Low).</returns>
    public string GetPressureCondition()
    {
        return PressureInHectopascals switch
        {
            > 1020 => "High",
            < 1000 => "Low",
            _ => "Normal"
        };
    }

    /// <summary>
    /// Returns the display name for this sensor.
    /// </summary>
    /// <returns>The name "Barometer".</returns>
    public override string ToString()
    {
        return "Barometer";
    }

    /// <summary>
    /// Handles barometer data updates from the service.
    /// </summary>
    /// <param name="data">The barometer data.</param>
    private void OnBarometerDataReceived(BarometerData data)
    {
        PressureInHectopascals = data.PressureInHectopascals;
        OnPropertyChanged(nameof(PressureDisplay));
        OnPropertyChanged(nameof(PressureInMillibars));
        OnPropertyChanged(nameof(PressureInInchesHg));
    }

    /// <summary>
    /// Updates the status based on the current sensor state.
    /// </summary>
    private void UpdateStatus()
    {
        if (!IsSupported)
        {
            Status = "Barometer is not supported on this device";
        }
        else if (IsMonitoring)
        {
            Status = "Barometer is on";
        }
        else
        {
            Status = "Barometer is not started";
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
            _barometerService.RemoveListener(OnBarometerDataReceived);
        }
    }
}
