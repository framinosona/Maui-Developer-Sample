using System.Windows.Input;
using Maui_Developer_Sample.Helpers;
using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.AppCapability.ViewModels;

/// <summary>
/// ViewModel for the vibration page.
/// Provides data binding and UI logic for device vibration functionality.
/// </summary>
/// <remarks>
/// This ViewModel acts as a bridge between the VibrationService
/// and the UI, providing bindable properties and commands for XAML data binding.
///
/// Vibration provides tactile feedback through device motor vibration,
/// offering configurable duration and timing control.
///
/// VIBRATION PATTERNS:
/// - Short vibrations: Good for notifications and button feedback (100-300ms)
/// - Medium vibrations: Suitable for alerts and confirmations (300-800ms)
/// - Long vibrations: For attention-getting scenarios (800ms+)
///
/// TYPICAL USAGE:
/// - Notification alerts
/// - Button press feedback
/// - Error or warning indicators
/// - Game haptic effects
/// - Accessibility enhancements
/// </remarks>
public class VibrationViewModel : EnhancedBindableObject
{
    private readonly VibrationService _vibrationService;

    /// <summary>
    /// Initializes a new instance of the VibrationViewModel.
    /// </summary>
    /// <param name="vibrationService">The vibration service instance.</param>
    public VibrationViewModel(VibrationService vibrationService)
    {
        _vibrationService = vibrationService ?? throw new ArgumentNullException(nameof(vibrationService));

        // Initialize commands
        VibrateCommand = new Command(Vibrate);
        CancelCommand = new Command(Cancel);

        // Set default duration
        DurationInMs = 500;
    }

    /// <summary>
    /// Gets whether vibration is supported on this device.
    /// </summary>
    public bool IsSupported => _vibrationService.IsSupported;

    /// <summary>
    /// Gets the current status of the vibration capability.
    /// </summary>
    /// <value>
    /// Status messages include:
    /// - "Vibration is supported" - Device supports vibration
    /// - "Vibration is not supported on this device" - Device does not support vibration
    /// </value>
    public string Status => IsSupported
        ? "Vibration is supported"
        : "Vibration is not supported on this device";

    /// <summary>
    /// Gets or sets the duration of the vibration in milliseconds.
    /// </summary>
    /// <value>
    /// Range: Typically 50-5000 milliseconds
    /// Default: 500 milliseconds
    /// Recommended ranges:
    /// - Short feedback: 100-300ms
    /// - Medium alerts: 300-800ms
    /// - Long notifications: 800-2000ms
    /// </value>
    public double DurationInMs
    {
        get => GetValue(500.0);
        set
        {
            if (SetValue(value))
            {
                OnPropertyChanged(nameof(DurationDisplay));
            }
        }
    }

    /// <summary>
    /// Gets the duration formatted for display.
    /// </summary>
    /// <value>A formatted string showing the duration in milliseconds.</value>
    public string DurationDisplay => $"{DurationInMs:F0} ms";

    /// <summary>
    /// Command to trigger a vibration with the specified duration.
    /// </summary>
    public ICommand VibrateCommand { get; }

    /// <summary>
    /// Command to cancel any currently active vibration.
    /// </summary>
    public ICommand CancelCommand { get; }

    /// <summary>
    /// Returns the display name for this capability.
    /// </summary>
    /// <returns>The name "Vibration".</returns>
    public override string ToString()
    {
        return "Vibration";
    }

    /// <summary>
    /// Triggers a vibration for the specified duration.
    /// Uses the current DurationInMs value.
    /// </summary>
    private void Vibrate()
    {
        if (IsSupported)
        {
            _vibrationService.Vibrate((int)DurationInMs);
        }
    }

    /// <summary>
    /// Cancels any currently active vibration.
    /// Safe to call even if no vibration is currently active.
    /// </summary>
    private void Cancel()
    {
        if (IsSupported)
        {
            _vibrationService.Cancel();
        }
    }
}
