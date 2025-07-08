namespace Maui_Developer_Sample.Services;

/// <summary>
/// Base class for bindable app capability services.
/// Provides a common interface for checking if the capability is supported.
/// </summary>
public abstract class BaseBindableAppCapabilityService
{
    /// <summary>
    /// Gets a value indicating whether the app capability is supported on the current device.
    /// </summary>
    public abstract bool IsSupported { get; }
}
