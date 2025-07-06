using Maui_Developer_Sample.Helpers;

namespace Maui_Developer_Sample.Pages.UI.Services;

public class AppTheme_Service : EnhancedBindableObject
{
    public AppTheme_Service()
    {
        IsLightModeEnabled = Application.Current?.PlatformAppTheme == AppTheme.Light;
    }
    
    private void RefreshTheme()
    {
        if (IsOverriden)
        {
            Application.Current!.UserAppTheme = IsLightModeEnabled ? AppTheme.Light : AppTheme.Dark;
        }
        else
        {
            Application.Current!.UserAppTheme = Application.Current!.PlatformAppTheme;
        }
    }
    
    public bool IsLightModeEnabled
    {
        get => GetValue(false);
        set
        {
            if(SetValue(value))
            {
                RefreshTheme();
            };
        }
    }
    
    public bool IsOverriden
    {
        get => GetValue(false);
        set
        {
            if(SetValue(value))
            {
                RefreshTheme();
            };
        }
    }
}
