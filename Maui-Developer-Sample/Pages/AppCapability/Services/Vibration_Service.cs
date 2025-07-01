namespace Maui_Developer_Sample.Pages.AppCapability.Services;

public class Vibration_Service : BaseBindableAppCapability_Service
{
    protected override bool IsSupported() => Vibration.Default.IsSupported;

    public Vibration_Service()
    {
        VibrateCommand = new Command(Vibrate);
        CancelCommand = new Command(Cancel);
    }

    private void Vibrate() => Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(DurationInMs));

    private void Cancel() => Vibration.Default.Cancel();

    public double DurationInMs
    {
        get => GetValue(500d);
        set => SetValue(value);
    }

    public Command VibrateCommand { get; }

    public Command CancelCommand { get; }
}
