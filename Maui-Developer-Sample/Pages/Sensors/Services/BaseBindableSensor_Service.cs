using Maui_Developer_Sample.Pages.AppCapability.Services;

namespace Maui_Developer_Sample.Pages.Sensors.Services;

public abstract class BaseBindableSensor_Service : BaseBindableAppCapability_Service
{
    protected BaseBindableSensor_Service()
    {
        // Initialize the sensor state
        IsMonitoring = false;
        Status = $"{this} is not started";
    }
    
    public SensorSpeed SensorSpeed
    {
        get => GetValue(SensorSpeed.UI);
        set
        {
            if(SetValue(value))
            {
                if (IsMonitoring)
                {
                    StopIfNeeded();
                    StartIfNeeded();
                }
            }
        }
    }

    public bool IsMonitoring
    {
        get => GetValue(false);
        set
        {
            if (!SetValue(value))
                return;

            if (value)
                StartIfNeeded();
            else
                StopIfNeeded();
        }
    }

    public string Status
    {
        get => GetValue("Not Started");
        protected set => SetValue(value);
    }

    private void StartIfNeeded()
    {
        if (IsSensorMonitoring())
            return;

        try
        {
            if (!IsSupported())
                throw new NotSupportedException($"{this} is not supported on this device.");
            
            UnsubscribeFromSensorEvents(); // Ensure no previous subscriptions are active
            SubscribeToSensorEvents();
            StartSensor();
            Status = $"{this} is on";
            IsMonitoring = true;
        }
        catch (Exception ex)
        {
            IsMonitoring = false;
            Status = $"Error: {ex.Message}";
            Console.WriteLine(ex);
        }
    }

    private void StopIfNeeded()
    {
        if (IsSensorMonitoring())
        {
            StopSensor();
            UnsubscribeFromSensorEvents();
        }
        Status = $"{this} is off";
        IsMonitoring = false;
    }


    protected abstract void SubscribeToSensorEvents();
    protected abstract void UnsubscribeFromSensorEvents();
    protected abstract bool IsSensorMonitoring();
    protected abstract void StartSensor();
    protected abstract void StopSensor();

    public override string ToString()
    {
        return "Unknown Sensor";
    }
}
