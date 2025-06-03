namespace Maui_Developer_Sample.Pages.Vibrations.BindableHapticFeedback;

public class BindableHapticFeedbackSimple : BindableObject
{

    #region Singleton

    private readonly static Lazy<BindableHapticFeedbackSimple> LazyInstance = new Lazy<BindableHapticFeedbackSimple>(() => new BindableHapticFeedbackSimple());

    public static BindableHapticFeedbackSimple Instance => LazyInstance.Value;

    #endregion

    private BindableHapticFeedbackSimple()
    {
        VibrateClickCommand = new Command(VibrateClick);
        VibrateLongPressCommand = new Command(VibrateLongPress);
    }

    private void VibrateClick() => HapticFeedback.Default.Perform(HapticFeedbackType.Click);

    private void VibrateLongPress() => HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);

    public Command VibrateClickCommand { get; }

    public Command VibrateLongPressCommand { get; }
}
