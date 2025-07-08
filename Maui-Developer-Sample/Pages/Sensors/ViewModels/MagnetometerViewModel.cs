using System.Numerics;
using Maui_Developer_Sample.Helpers;
using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.Sensors.ViewModels;

/// <summary>
/// ViewModel for the magnetometer sensor page.
/// Provides data binding and UI logic for magnetometer sensor readings.
/// </summary>
/// <remarks>
/// This ViewModel acts as a bridge between the MagnetometerSensorService
/// and the UI, providing bindable properties for XAML data binding.
///
/// The magnetometer measures magnetic field strength around three axes.
/// Values are expressed in microteslas (μT).
///
/// COORDINATE SYSTEM:
/// - X-axis: Horizontal magnetic field strength (left/right)
/// - Y-axis: Vertical magnetic field strength (up/down)
/// - Z-axis: Depth magnetic field strength (forward/backward)
///
/// TYPICAL VALUES:
/// - Earth's magnetic field: ~25-65 μT (varies by location)
/// - Near metal objects: Can be significantly higher
/// - Magnetic north direction: Combination of X, Y, Z values
/// - Values can range from -100 to +100 μT in normal conditions
/// </remarks>
public class MagnetometerViewModel : EnhancedBindableObject
{
    private readonly MagnetometerSensorService _magnetometerService;

    /// <summary>
    /// Initializes a new instance of the MagnetometerViewModel.
    /// </summary>
    /// <param name="magnetometerService">The magnetometer sensor service instance.</param>
    public MagnetometerViewModel(MagnetometerSensorService magnetometerService)
    {
        _magnetometerService = magnetometerService ?? throw new ArgumentNullException(nameof(magnetometerService));

        // Initialize status
        UpdateStatus();
    }

    /// <summary>
    /// Gets whether the magnetometer is supported on this device.
    /// </summary>
    public bool IsSupported => _magnetometerService.IsSupported;

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
        get => _magnetometerService.IsMonitoring;
        set
        {
            if (_magnetometerService.IsMonitoring != value)
            {
                if (value)
                {
                    _magnetometerService.AddListener(OnMagnetometerDataReceived);
                    UpdateStatus();
                }
                else
                {
                    _magnetometerService.RemoveListener(OnMagnetometerDataReceived);
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
    /// - "Magnetometer is not started" - Initial state
    /// - "Magnetometer is on" - Successfully monitoring
    /// - "Magnetometer is off" - Stopped monitoring
    /// - "Error: {ErrorMessage}" - When an error occurs
    /// </value>
    public string Status
    {
        get => GetValue("Not Started");
        private set => SetValue(value);
    }

    /// <summary>
    /// Magnetic field strength along the X-axis in microteslas.
    /// </summary>
    /// <value>
    /// Range: Typically -100 to +100 μT (can exceed near strong magnetic sources)
    /// Positive: Magnetic field pointing right
    /// Negative: Magnetic field pointing left
    /// Zero: No magnetic field in horizontal direction
    /// </value>
    public float XinMicroTeslas
    {
        get => GetValue(0.0f);
        private set => SetValue(value);
    }

    /// <summary>
    /// Magnetic field strength along the Y-axis in microteslas.
    /// </summary>
    /// <value>
    /// Range: Typically -100 to +100 μT (can exceed near strong magnetic sources)
    /// Positive: Magnetic field pointing up
    /// Negative: Magnetic field pointing down
    /// Zero: No magnetic field in vertical direction
    /// </value>
    public float YinMicroTeslas
    {
        get => GetValue(0.0f);
        private set => SetValue(value);
    }

    /// <summary>
    /// Magnetic field strength along the Z-axis in microteslas.
    /// </summary>
    /// <value>
    /// Range: Typically -100 to +100 μT (can exceed near strong magnetic sources)
    /// Positive: Magnetic field pointing toward user
    /// Negative: Magnetic field pointing away from user
    /// Zero: No magnetic field in depth direction
    /// </value>
    public float ZinMicroTeslas
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
    public SensorSpeed SensorSpeed => _magnetometerService.SensorSpeed;

    /// <summary>
    /// Gets the current magnetic field vector.
    /// </summary>
    /// <value>A Vector3 containing the X, Y, and Z magnetic field values in microteslas.</value>
    public Vector3 MagneticFieldVector => new(XinMicroTeslas, YinMicroTeslas, ZinMicroTeslas);

    /// <summary>
    /// Gets the total magnetic field strength (magnitude of the vector).
    /// </summary>
    /// <value>The magnitude of the magnetic field vector in microteslas.</value>
    public float MagneticFieldMagnitude => MagneticFieldVector.Length();

    /// <summary>
    /// Returns the display name for this sensor.
    /// </summary>
    /// <returns>The name "Magnetometer".</returns>
    public override string ToString()
    {
        return "Magnetometer";
    }

    /// <summary>
    /// Handles magnetometer data updates from the service.
    /// </summary>
    /// <param name="data">The magnetometer data.</param>
    private void OnMagnetometerDataReceived(MagnetometerData data)
    {
        XinMicroTeslas = data.MagneticField.X;
        YinMicroTeslas = data.MagneticField.Y;
        ZinMicroTeslas = data.MagneticField.Z;
        OnPropertyChanged(nameof(MagneticFieldVector));
        OnPropertyChanged(nameof(MagneticFieldMagnitude));
    }

    /// <summary>
    /// Updates the status based on the current sensor state.
    /// </summary>
    private void UpdateStatus()
    {
        if (!IsSupported)
        {
            Status = "Magnetometer is not supported on this device";
        }
        else if (IsMonitoring)
        {
            Status = "Magnetometer is on";
        }
        else
        {
            Status = "Magnetometer is not started";
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
            _magnetometerService.RemoveListener(OnMagnetometerDataReceived);
        }
    }
}
