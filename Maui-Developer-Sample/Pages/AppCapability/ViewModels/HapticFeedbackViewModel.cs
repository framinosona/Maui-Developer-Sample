using System.Windows.Input;
using Maui_Developer_Sample.Helpers;
using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.AppCapability.ViewModels;

/// <summary>
/// ViewModel for the haptic feedback page.
/// Provides data binding and UI logic for haptic feedback functionality.
/// </summary>
/// <remarks>
/// This ViewModel acts as a bridge between the HapticFeedbackService
/// and the UI, providing bindable properties and commands for XAML data binding.
///
/// Haptic feedback provides tactile responses to user interactions,
/// enhancing the user experience with physical feedback.
///
/// FEEDBACK TYPES:
/// - Click: Short, light vibration for button presses
/// - Long Press: Longer, stronger vibration for extended interactions
///
/// TYPICAL USAGE:
/// - Button press confirmation
/// - UI interaction feedback
/// - Error or success notifications
/// - Enhanced accessibility
/// </remarks>
public class HapticFeedbackViewModel : EnhancedBindableObject
{
    private readonly HapticFeedbackService _hapticFeedbackService;

    /// <summary>
    /// Initializes a new instance of the HapticFeedbackViewModel.
    /// </summary>
    /// <param name="hapticFeedbackService">The haptic feedback service instance.</param>
    public HapticFeedbackViewModel(HapticFeedbackService hapticFeedbackService)
    {
        _hapticFeedbackService = hapticFeedbackService ?? throw new ArgumentNullException(nameof(hapticFeedbackService));

        // Initialize commands
        VibrateClickCommand = new Command(VibrateClick);
        VibrateLongPressCommand = new Command(VibrateLongPress);
    }

    /// <summary>
    /// Gets whether haptic feedback is supported on this device.
    /// </summary>
    public bool IsSupported => _hapticFeedbackService.IsSupported;

    /// <summary>
    /// Gets the current status of the haptic feedback capability.
    /// </summary>
    /// <value>
    /// Status messages include:
    /// - "Haptic Feedback is supported" - Device supports haptic feedback
    /// - "Haptic Feedback is not supported on this device" - Device does not support haptic feedback
    /// </value>
    public string Status => IsSupported
        ? "Haptic Feedback is supported"
        : "Haptic Feedback is not supported on this device";

    /// <summary>
    /// Command to perform a click haptic feedback.
    /// Provides short, light tactile feedback for button presses.
    /// </summary>
    public ICommand VibrateClickCommand { get; }

    /// <summary>
    /// Command to perform a long press haptic feedback.
    /// Provides longer, stronger tactile feedback for extended interactions.
    /// </summary>
    public ICommand VibrateLongPressCommand { get; }

    /// <summary>
    /// Returns the display name for this capability.
    /// </summary>
    /// <returns>The name "Haptic Feedback".</returns>
    public override string ToString()
    {
        return "Haptic Feedback";
    }

    /// <summary>
    /// Performs a click haptic feedback.
    /// Triggers a short, light vibration suitable for button presses.
    /// </summary>
    private void VibrateClick()
    {
        if (IsSupported)
        {
            _hapticFeedbackService.VibrateClick();
        }
    }

    /// <summary>
    /// Performs a long press haptic feedback.
    /// Triggers a longer, stronger vibration suitable for extended interactions.
    /// </summary>
    private void VibrateLongPress()
    {
        if (IsSupported)
        {
            _hapticFeedbackService.VibrateLongPress();
        }
    }
}
