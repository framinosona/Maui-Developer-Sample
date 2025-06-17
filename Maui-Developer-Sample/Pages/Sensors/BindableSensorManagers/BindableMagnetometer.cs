namespace Maui_Developer_Sample.Pages.Sensors.BindableSensorManagers;

public class BindableMagnetometer : BindableObject
{

    #region Singleton

    private readonly static Lazy<BindableMagnetometer> LazyInstance =
        new Lazy<BindableMagnetometer>(() => new BindableMagnetometer());

    public static BindableMagnetometer Instance
    {
        get => LazyInstance.Value;
    }

    #endregion

    public BindableMagnetometer()
    {
        Magnetometer.ReadingChanged += OnMagnetometerReadingChanged;
    }

    #region Start/Stop

    private void StartIfNeeded()
    {
        if (Magnetometer.IsMonitoring)
            return;

        try
        {
            if (!Magnetometer.IsSupported)
                throw new NotSupportedException("Magnetometer is not supported on this device.");
            Magnetometer.Start(SensorSpeed.UI);
            Status = "Magnetometer is on";
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
        if (Magnetometer.IsMonitoring)
            Magnetometer.Stop();
        IsMonitoring = false;
        Status = "Magnetometer is off";
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

    #region Values

    private void OnMagnetometerReadingChanged(object? sender, MagnetometerChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => {
            X = e.Reading.MagneticField.X;
            Y = e.Reading.MagneticField.Y;
            Z = e.Reading.MagneticField.Z;
        });
    }

    private float _x;

    public float X
    {
        get => _x;
        set
        {
            _x = value;
            OnPropertyChanged();
        }
    }

    private float _y;

    public float Y
    {
        get => _y;
        set
        {
            _y = value;
            OnPropertyChanged();
        }
    }

    private float _z;

    public float Z
    {
        get => _z;
        set
        {
            _z = value;
            OnPropertyChanged();
        }
    }

    #endregion

}
