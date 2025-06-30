using Maui_Developer_Sample.Helpers;

namespace Maui_Developer_Sample.Pages.AppCapability.Services;

public abstract class BaseBindableAppCapability_Service : EnhancedBindableObject
{
    protected abstract bool IsSupported();
}
