namespace Maui_Developer_Sample.Services;

/// <summary>
/// Service for providing haptic feedback (tactile responses) to user interactions.
/// Offers different types of haptic feedback patterns for enhanced user experience.
/// </summary>
/// <remarks>
/// This service wraps the platform-specific haptic feedback APIs and provides
/// a consistent interface for triggering tactile responses on user interactions.
///
/// HAPTIC FEEDBACK TYPES:
/// - Click: Light, quick feedback for button presses and taps
/// - LongPress: Medium intensity feedback for long press interactions
/// - Different intensities provide varying levels of tactile response
///
/// APPLICATIONS:
/// - Button press confirmations
/// - User interface interaction feedback
/// - Accessibility enhancements
/// - Gaming and interactive experiences
/// - Form validation and user guidance
/// </remarks>
public class HapticFeedbackService : BaseBindableAppCapabilityService
{
    /// <summary>
    /// Gets a value indicating whether haptic feedback is supported on this device.
    /// </summary>
    /// <value>
    /// <c>true</c> if haptic feedback hardware is available; otherwise, <c>false</c>.
    /// Most modern mobile devices support haptic feedback functionality.
    /// </value>
    public override bool IsSupported => HapticFeedback.Default.IsSupported;

    /// <summary>
    /// Triggers a light haptic feedback suitable for button clicks and taps.
    /// Provides subtle tactile confirmation for standard UI interactions.
    /// </summary>
    /// <remarks>
    /// This method generates a short, light haptic pulse that's appropriate for
    /// confirming button presses, list item selections, and other standard UI interactions.
    /// The feedback is designed to be subtle and not interfere with normal device usage.
    /// </remarks>
    public void VibrateClick() => HapticFeedback.Perform(HapticFeedbackType.Click);

    /// <summary>
    /// Triggers a medium-intensity haptic feedback suitable for long press interactions.
    /// Provides stronger tactile confirmation for significant user actions.
    /// </summary>
    /// <remarks>
    /// This method generates a longer, more intense haptic pulse that's appropriate for
    /// confirming long press actions, context menu activations, and other significant
    /// user interactions. The feedback is more noticeable than the click feedback.
    /// </remarks>
    public void VibrateLongPress() => HapticFeedback.Perform(HapticFeedbackType.LongPress);
}
