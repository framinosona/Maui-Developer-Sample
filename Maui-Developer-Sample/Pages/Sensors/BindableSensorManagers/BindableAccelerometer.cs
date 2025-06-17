namespace Maui_Developer_Sample.Pages.Sensors.BindableSensorManagers;

public class BindableAccelerometer : BindableObject
{

    #region Singleton

    private readonly static Lazy<BindableAccelerometer> LazyInstance =
        new Lazy<BindableAccelerometer>(() => new BindableAccelerometer());

    public static BindableAccelerometer Instance
    {
        get => LazyInstance.Value;
    }

    #endregion

    public BindableAccelerometer()
    {
        Accelerometer.ReadingChanged += OnAccelerometerReadingChanged;
    }

    #region Start/Stop

    private void StartIfNeeded()
    {
        if (Accelerometer.IsMonitoring)
            return;

        try
        {
            if (!Accelerometer.IsSupported)
                throw new NotSupportedException("Accelerometer is not supported on this device.");

            Accelerometer.Start(SensorSpeed.UI);
            Status = "Accelerometer is on";
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
        if (Accelerometer.IsMonitoring)
            Accelerometer.Stop();
        IsMonitoring = false;
        Status = "Accelerometer is off";
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

    private void OnAccelerometerReadingChanged(object? sender, AccelerometerChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => {
            X = e.Reading.Acceleration.X;
            Y = e.Reading.Acceleration.Y;
            Z = e.Reading.Acceleration.Z;
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
