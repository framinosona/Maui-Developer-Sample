namespace Framinosona.SensorsManager;

public class BindableGyroscope : BindableObject
{

    #region Singleton

    private readonly static Lazy<BindableGyroscope> LazyInstance =
        new Lazy<BindableGyroscope>(() => new BindableGyroscope());

    public static BindableGyroscope Instance
    {
        get => LazyInstance.Value;
    }

    #endregion

    public BindableGyroscope()
    {
        Gyroscope.ReadingChanged += OnGyroscopeReadingChanged;
    }

    #region Start/Stop

    private void StartIfNeeded()
    {
        if (Gyroscope.IsMonitoring)
            return;
        
        try
        {
            if (!Gyroscope.IsSupported)
                throw new NotSupportedException("Compass is not supported on this device.");
            Gyroscope.Start(SensorSpeed.UI);
            Status = "Gyroscope is on";
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
        if (Gyroscope.IsMonitoring)
            Gyroscope.Stop();
        IsMonitoring = false;
        Status = "Gyroscope is off";
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

    private void OnGyroscopeReadingChanged(object? sender, GyroscopeChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => {
            X = e.Reading.AngularVelocity.X;
            Y = e.Reading.AngularVelocity.Y;
            Z = e.Reading.AngularVelocity.Z;
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
