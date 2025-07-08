namespace Maui_Developer_Sample.Services;

/// <summary>
/// Service for controlling device vibration functionality.
/// Provides methods to trigger vibration patterns and cancel ongoing vibrations.
/// </summary>
/// <remarks>
/// This service wraps the platform-specific vibration APIs and provides
/// a consistent interface for triggering haptic feedback through vibration.
///
/// VIBRATION PATTERNS:
/// - Short vibrations: Good for notifications and button feedback
/// - Long vibrations: Suitable for alerts and attention-getting scenarios
/// - Custom durations: Configurable timing for specific use cases
/// </remarks>
public class VibrationService : BaseBindableAppCapabilityService
{
    /// <summary>
    /// Gets a value indicating whether vibration is supported on this device.
    /// </summary>
    /// <value>
    /// <c>true</c> if vibration hardware is available; otherwise, <c>false</c>.
    /// Most mobile devices support vibration functionality.
    /// </value>
    public override bool IsSupported => Vibration.Default.IsSupported;

    /// <summary>
    /// Triggers a vibration for the specified duration.
    /// </summary>
    /// <param name="durationInMs">The duration of the vibration in milliseconds. Default is 500ms.</param>
    public void Vibrate(int durationInMs = 500) => Vibration.Vibrate(TimeSpan.FromMilliseconds(durationInMs));

    /// <summary>
    /// Cancels any currently active vibration.
    /// Safe to call even if no vibration is currently active.
    /// </summary>
    public void Cancel() => Vibration.Cancel();
}
