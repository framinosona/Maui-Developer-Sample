namespace Maui_Developer_Sample.Pages.Sensors.Services;

public class Gyroscope_Service : BaseBindableSensor_Service
{
    protected override bool IsSupported() => Gyroscope.IsSupported;

    protected override bool IsSensorMonitoring() => Gyroscope.IsMonitoring;

    protected override void SubscribeToSensorEvents() => Gyroscope.ReadingChanged += OnReadingChanged;

    protected override void UnsubscribeFromSensorEvents() => Gyroscope.ReadingChanged -= OnReadingChanged;

    protected override void StartSensor() => Gyroscope.Start(SensorSpeed);

    protected override void StopSensor() => Gyroscope.Stop();

    public override string ToString() => nameof(Gyroscope);

    private void OnReadingChanged(object? sender, GyroscopeChangedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => {
            XRadians = e.Reading.AngularVelocity.X;
            YRadians = e.Reading.AngularVelocity.Y;
            ZRadians = e.Reading.AngularVelocity.Z;
        });
    }

    public float XRadians
    {
        get => GetValue(0.0f);
        protected set
        {
            if (SetValue(value))
            {
                OnPropertyChanged(nameof(XDegrees));
                OnPropertyChanged(nameof(XPercentage));
            }
        }
    }

    public float YRadians
    {
        get => GetValue(0.0f);
        protected set
        {
            if (SetValue(value))
            {
                OnPropertyChanged(nameof(YDegrees));
                OnPropertyChanged(nameof(YPercentage));
            }
        }
    }

    public float ZRadians
    {
        get => GetValue(0.0f);
        protected set
        {
            if (SetValue(value))
            {
                OnPropertyChanged(nameof(ZDegrees));
                OnPropertyChanged(nameof(ZPercentage));
            }
        }
    }

    private double RadianToDegree(float radians)
    {
        return radians * (180.0 / Math.PI);
    }

    public double XDegrees => RadianToDegree(XRadians);

    public double YDegrees => RadianToDegree(YRadians);

    public double ZDegrees => RadianToDegree(ZRadians);

    private double DegreeToPercentage(double degrees)
    {
        return degrees / 360.0;
    }

    public double XPercentage => DegreeToPercentage(XDegrees);

    public double YPercentage => DegreeToPercentage(YDegrees);

    public double ZPercentage => DegreeToPercentage(ZDegrees);
}
