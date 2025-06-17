namespace Maui_Developer_Sample.Pages.Sensors.BindableSensorManagers;

public class BindableCompass : BindableObject
{

    #region Singleton

    private readonly static Lazy<BindableCompass> LazyInstance = new Lazy<BindableCompass>(() => new BindableCompass());

    public static BindableCompass Instance
    {
        get => LazyInstance.Value;
    }

    #endregion

    public BindableCompass()
    {
        Compass.ReadingChanged += OnCompassReadingChanged;
    }

    #region Start/Stop

    private void StartIfNeeded()
    {
        if (Compass.IsMonitoring)
            return;

        try
        {
            if (!Compass.IsSupported)
                throw new NotSupportedException("Compass is not supported on this device.");

            Compass.Start(SensorSpeed.UI);
            Status = "Compass is on";
            IsMonitoring = true;
        }
        catch (Exception e)
        {
            Status = "Error: " + e.Message;
            Console.WriteLine(e);
        }
    }

    private void StopIfNeeded()
    {
        if (Compass.IsMonitoring)
            Compass.Stop();
        IsMonitoring = false;
        Status = "Compass is off";
    }

    #endregion

    #region IsMonitoring

    private bool _isMonitoring = false;

    public bool IsMonitoring
    {
        get => _isMonitoring;
        set
        {
            if (value == _isMonitoring)
                return;

            _isMonitoring = value;
            OnPropertyChanged();
            if (value)
            {
                StartIfNeeded();
            }
            else
            {
                StopIfNeeded();
            }
        }
    }

    #endregion

    #region Status

    private string _status = "Not Started";

    public string Status
    {
        get => _status;
        protected set
        {
            _status = value;
            OnPropertyChanged();
        }
    }

    #endregion

    private void OnCompassReadingChanged(object? sender, CompassChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => { Heading = e.Reading.HeadingMagneticNorth; });
    }

    private double _heading;

    public double Heading
    {
        get => _heading;
        set
        {
            _heading = value;
            OnPropertyChanged();
        }
    }
}
