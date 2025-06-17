namespace Maui_Developer_Sample.Pages.Sensors.BindableSensorManagers;

public class BindableOrientationSensor : BindableObject
{

    #region Singleton

    private readonly static Lazy<BindableOrientationSensor> LazyInstance =
        new Lazy<BindableOrientationSensor>(() => new BindableOrientationSensor());

    public static BindableOrientationSensor Instance
    {
        get => LazyInstance.Value;
    }

    #endregion

    public BindableOrientationSensor()
    {
        OrientationSensor.ReadingChanged += OnOrientationSensorReadingChanged;
    }

    #region Start/Stop

    private void StartIfNeeded()
    {
        if (OrientationSensor.IsMonitoring)
            return;

        try
        {
            if (!OrientationSensor.IsSupported)
                throw new NotSupportedException("OrientationSensor is not supported on this device.");
            OrientationSensor.Start(SensorSpeed.UI);
            Status = "Orientation Sensor is on";
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
        if (OrientationSensor.IsMonitoring)
            OrientationSensor.Stop();
        IsMonitoring = false;
        Status = "Orientation Sensor is off";
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

    private void OnOrientationSensorReadingChanged(object? sender, OrientationSensorChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => {
            W = e.Reading.Orientation.W;
            X = e.Reading.Orientation.X;
            Y = e.Reading.Orientation.Y;
            Z = e.Reading.Orientation.Z;
        });
    }

    private float _w;

    public float W
    {
        get => _w;
        set
        {
            _w = value;
            OnPropertyChanged();
        }
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
