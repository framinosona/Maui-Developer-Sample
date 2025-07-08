namespace Maui_Developer_Sample.Services;

public class HapticFeedbackService : BaseBindableAppCapabilityService
{
    public override bool IsSupported => HapticFeedback.Default.IsSupported;

    public void VibrateClick() => HapticFeedback.Perform(HapticFeedbackType.Click);

    public void VibrateLongPress() => HapticFeedback.Perform(HapticFeedbackType.LongPress);
}
