using System.Windows.Input;
using Maui_Developer_Sample.Helpers;
using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.UI.ViewModels;

/// <summary>
/// ViewModel for the application theme page.
/// Provides data binding and UI logic for theme management functionality.
/// </summary>
/// <remarks>
/// This ViewModel acts as a bridge between the AppThemeService and the UI,
/// providing bindable properties and commands for XAML data binding.
///
/// The theme system supports three states:
/// - Light Theme: Bright backgrounds with dark text
/// - Dark Theme: Dark backgrounds with light text
/// - System Theme: Automatically follows device/OS preference
///
/// THEME PROPERTIES:
/// - IsLightModeEnabled: Controls and reflects current theme mode
/// - IsOverriden: Indicates if user has overridden system theme
/// - CurrentThemeDisplay: Human-readable description of current theme
///
/// TYPICAL USAGE:
/// - Theme toggle switches in settings
/// - Theme selection lists
/// - System theme reset functionality
/// - Theme status indicators
/// </remarks>
public class AppThemeViewModel : EnhancedBindableObject
{
    private readonly AppThemeService _appThemeService;

    /// <summary>
    /// Initializes a new instance of the AppThemeViewModel.
    /// </summary>
    /// <param name="appThemeService">The app theme service instance.</param>
    public AppThemeViewModel(AppThemeService appThemeService)
    {
        _appThemeService = appThemeService ?? throw new ArgumentNullException(nameof(appThemeService));

        // Initialize commands
        ResetToSystemThemeCommand = new Command(ResetToSystemTheme);
        SetLightThemeCommand = new Command(() => SetTheme(true));
        SetDarkThemeCommand = new Command(() => SetTheme(false));
    }

    /// <summary>
    /// Gets or sets whether light mode is currently enabled.
    /// Setting this property will immediately change the application theme.
    /// </summary>
    /// <value>
    /// <c>true</c> if light theme is active; <c>false</c> if dark theme is active.
    /// When set, this will override any system theme preference.
    /// </value>
    /// <remarks>
    /// This property is typically bound to toggle switches or radio buttons
    /// in theme selection UI. Changing this value will immediately update
    /// the application's visual appearance.
    /// </remarks>
    public bool IsLightModeEnabled
    {
        get => _appThemeService.GetIsLightModeEnabled();
        set
        {
            if (_appThemeService.GetIsLightModeEnabled() != value)
            {
                SetTheme(value);
            }
        }
    }

    /// <summary>
    /// Gets whether the current theme has been manually overridden by the user.
    /// This is read-only and reflects the current override state.
    /// </summary>
    /// <value>
    /// <c>true</c> if user has set a custom theme that differs from system theme;
    /// <c>false</c> if app is following system theme preference.
    /// </value>
    /// <remarks>
    /// This property is useful for conditional UI elements, such as:
    /// - Showing/hiding "Reset to System" buttons
    /// - Displaying theme override indicators
    /// - Enabling/disabling theme-related controls
    /// </remarks>
    public bool IsOverriden
    {
        get => _appThemeService.GetIsOverriden();
    }

    /// <summary>
    /// Gets a human-readable description of the current theme state.
    /// </summary>
    /// <value>
    /// Returns one of:
    /// - "Light Theme (Custom)" - User-selected light theme
    /// - "Dark Theme (Custom)" - User-selected dark theme
    /// - "System Theme" - Following device preference
    /// </value>
    /// <remarks>
    /// This property is useful for displaying the current theme status
    /// in settings pages or theme selection interfaces.
    /// </remarks>
    public string CurrentThemeDisplay
    {
        get
        {
            if (IsOverriden)
            {
                return IsLightModeEnabled ? "Light Theme (Custom)" : "Dark Theme (Custom)";
            }
            return "System Theme";
        }
    }

    /// <summary>
    /// Gets the current theme mode as a string.
    /// </summary>
    /// <value>Returns "Light" or "Dark" based on current theme.</value>
    public string CurrentThemeMode => IsLightModeEnabled ? "Light" : "Dark";

    /// <summary>
    /// Command to reset the application theme to follow system preference.
    /// This removes any user-defined theme override.
    /// </summary>
    public ICommand ResetToSystemThemeCommand { get; }

    /// <summary>
    /// Command to explicitly set the application to light theme.
    /// This overrides system theme preference.
    /// </summary>
    public ICommand SetLightThemeCommand { get; }

    /// <summary>
    /// Command to explicitly set the application to dark theme.
    /// This overrides system theme preference.
    /// </summary>
    public ICommand SetDarkThemeCommand { get; }

    /// <summary>
    /// Returns the display name for this view model.
    /// </summary>
    /// <returns>The name "App Theme".</returns>
    public override string ToString()
    {
        return "App Theme";
    }

    /// <summary>
    /// Sets the application theme to the specified mode.
    /// </summary>
    /// <param name="isLightMode">
    /// <c>true</c> to set light theme; <c>false</c> to set dark theme.
    /// </param>
    /// <remarks>
    /// This method updates the theme through the service and refreshes
    /// all related properties to ensure UI synchronization.
    /// </remarks>
    private void SetTheme(bool isLightMode)
    {
        _appThemeService.SetAppTheme(isLightMode);
        RefreshThemeProperties();
    }

    /// <summary>
    /// Resets the application theme to follow system preference.
    /// </summary>
    /// <remarks>
    /// After calling this method, the application will automatically
    /// switch themes when the user changes their device theme settings.
    /// </remarks>
    private void ResetToSystemTheme()
    {
        _appThemeService.ResetAppTheme();
        RefreshThemeProperties();
    }

    /// <summary>
    /// Refreshes all theme-related properties to ensure UI synchronization.
    /// </summary>
    /// <remarks>
    /// This method should be called after any theme changes to ensure
    /// that all bound UI elements reflect the current theme state.
    /// </remarks>
    private void RefreshThemeProperties()
    {
        OnPropertyChanged(nameof(IsLightModeEnabled));
        OnPropertyChanged(nameof(IsOverriden));
        OnPropertyChanged(nameof(CurrentThemeDisplay));
        OnPropertyChanged(nameof(CurrentThemeMode));
    }
}
