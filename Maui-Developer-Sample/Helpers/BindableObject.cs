using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maui_Developer_Sample.Helpers;

public class EnhancedBindableObject : INotifyPropertyChanged
{
    private readonly ConcurrentDictionary<string, object?> _values = new ConcurrentDictionary<string, object?>();

    #region ILoggableObject Members

    public override string ToString()
    {
        return GetType().Name;
    }

    #endregion

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    protected bool HasValue([CallerMemberName] string? propertyName = null)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
            throw new ArgumentException("Invalid property name", propertyName);

        return _values.ContainsKey(propertyName);
    }

    protected T GetValue<T>(T defaultValue, [CallerMemberName] string? propertyName = null)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
            throw new ArgumentException("Invalid property name", propertyName);

        if (_values.TryGetValue(propertyName, out var value) && value is T tValue)
            return tValue;

        _values.TryAdd(propertyName, defaultValue);
        return defaultValue;
    }

    protected bool SetValue<T>(T value, [CallerMemberName] string? propertyName = null)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
            throw new ArgumentException("Invalid property name", propertyName);

        if (_values.TryGetValue(propertyName, out var existingValue) && Equals(existingValue, value))
            return false; // No change

        _values.AddOrUpdate(propertyName, value, (key, oldValue) => value);
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
