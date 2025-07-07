namespace Maui_Developer_Sample.Pages.AppCapability.Services;

public class Vibration_Service : BaseBindableAppCapability_Service
{

    public Vibration_Service()
    {
        VibrateCommand = new Command(Vibrate);
        CancelCommand = new Command(Cancel);
    }

    public override bool IsSupported => Vibration.Default.IsSupported;

    private void Vibrate() => Vibration.Vibrate(TimeSpan.FromMilliseconds(DurationInMs));

    private void Cancel() => Vibration.Cancel();

    public double DurationInMs
    {
        get => GetValue(500d);
        set => SetValue(value);
    }

    // Commands
    public Command VibrateCommand { get; }

    public Command CancelCommand { get; }
}
