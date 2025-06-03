namespace Framinosona.SensorsManager;

public class BindableBarometer : BindableObject
{

    #region Singleton

    private readonly static Lazy<BindableBarometer> LazyInstance =
        new Lazy<BindableBarometer>(() => new BindableBarometer());

    public static BindableBarometer Instance
    {
        get => LazyInstance.Value;
    }

    #endregion

    public BindableBarometer()
    {
        Barometer.ReadingChanged += OnBarometerReadingChanged;
    }

    #region Start/Stop

    private void StartIfNeeded()
    {
        if (Barometer.IsMonitoring)
            return;

        try
        {
            if (!Barometer.IsSupported)
                throw new NotSupportedException("Barometer is not supported on this device.");

            Barometer.Start(SensorSpeed.UI);
            Status = "Barometer is on";
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
        if (Barometer.IsMonitoring)
            Barometer.Stop();
        IsMonitoring = false;
        Status = "Barometer is off";
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

    private void OnBarometerReadingChanged(object? sender, BarometerChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => { Pressure = e.Reading.PressureInHectopascals; });
    }

    private double _pressure;

    public double Pressure
    {
        get => _pressure;
        set
        {
            _pressure = value;
            OnPropertyChanged();
        }
    }
}
