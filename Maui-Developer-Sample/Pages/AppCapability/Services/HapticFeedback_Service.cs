namespace Maui_Developer_Sample.Pages.AppCapability.Services;

public class HapticFeedback_Service : BaseBindableAppCapability_Service
{
    public HapticFeedback_Service()
    {
        VibrateClickCommand = new Command(VibrateClick);
        VibrateLongPressCommand = new Command(VibrateLongPress);
    }

    public override bool IsSupported => HapticFeedback.Default.IsSupported;
    
    public void VibrateClick() => HapticFeedback.Default.Perform(HapticFeedbackType.Click);

    public void VibrateLongPress() => HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);

    public Command VibrateClickCommand { get; }

    public Command VibrateLongPressCommand { get; }

}
