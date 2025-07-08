namespace Maui_Developer_Sample.Services;

/// <summary>
/// Service for managing application theme settings and user preferences.
/// Provides functionality to switch between light and dark themes, reset to system theme,
/// and check current theme states.
/// </summary>
/// <remarks>
/// This service wraps the MAUI theme management APIs and provides a consistent interface
/// for controlling application appearance. It handles both user-defined themes and
/// system-defined theme preferences.
///
/// THEME MANAGEMENT:
/// - Light Theme: Bright backgrounds with dark text
/// - Dark Theme: Dark backgrounds with light text
/// - System Theme: Follows device/OS theme preference
/// - User Override: Manually set theme that overrides system preference
///
/// TYPICAL USAGE:
/// - Theme switching in settings pages
/// - Automatic theme detection
/// - User preference persistence
/// - Accessibility compliance
/// </remarks>
public class AppThemeService
{
    /// <summary>
    /// Sets the application theme to light or dark mode.
    /// This overrides the system theme preference.
    /// </summary>
    /// <param name="isLightModeEnabled">
    /// <c>true</c> to set light theme; <c>false</c> to set dark theme.
    /// </param>
    /// <remarks>
    /// When called, this method will override the system theme preference.
    /// The theme change takes effect immediately across the entire application.
    /// The user's choice is maintained until explicitly changed or reset.
    /// </remarks>
    public void SetAppTheme(bool isLightModeEnabled)
    {
        Application.Current!.UserAppTheme = isLightModeEnabled ? AppTheme.Light : AppTheme.Dark;
    }

    /// <summary>
    /// Resets the application theme to follow the system/platform theme preference.
    /// This removes any user-defined theme override.
    /// </summary>
    /// <remarks>
    /// After calling this method, the application will automatically switch themes
    /// when the user changes their system-wide theme preference (e.g., in device settings).
    /// This is the default behavior when no explicit theme has been set.
    /// </remarks>
    public void ResetAppTheme()
    {
        Application.Current!.UserAppTheme = Application.Current!.PlatformAppTheme;
    }

    /// <summary>
    /// Determines whether the current theme has been manually overridden by the user.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the user has manually set a theme that differs from the system theme;
    /// <c>false</c> if the app is following the system theme preference.
    /// </returns>
    /// <remarks>
    /// This method is useful for UI elements that need to show whether the app is using
    /// a custom theme or following system preferences. For example, a "Reset to System"
    /// button might only be enabled when this returns <c>true</c>.
    /// </remarks>
    public bool GetIsOverriden()
    {
        return Application.Current!.UserAppTheme != Application.Current!.PlatformAppTheme;
    }

    /// <summary>
    /// Determines whether the application is currently using light mode.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the current theme is light mode;
    /// <c>false</c> if the current theme is dark mode or unspecified.
    /// </returns>
    /// <remarks>
    /// This method checks the active user theme setting. If no user theme has been set,
    /// it will reflect the system theme preference. This is useful for UI elements
    /// like theme toggle switches that need to show the current theme state.
    /// </remarks>
    public bool GetIsLightModeEnabled()
    {
        return Application.Current!.UserAppTheme == AppTheme.Light;
    }
}
