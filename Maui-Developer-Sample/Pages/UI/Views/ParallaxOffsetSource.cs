using Maui_Developer_Sample.Helpers;
using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.UI.Views;

/// <summary>
/// This class represents a source of parallax offset.
/// It gives an X and Y value, moving from -1 to 1.
/// The offset is used to calculate the position of the parallax layer.
/// </summary>
public abstract class ParallaxOffsetSource : BindableObject
{
    private List<IParallaxOffsetListener> Listeners { get; } = new List<IParallaxOffsetListener>();

    public virtual void AddListener(IParallaxOffsetListener listener)
    {
        if (listener == null)
            throw new ArgumentNullException(nameof(listener));

        lock (Listeners)
        {
            // Ensure the listener is not already added
            if (Listeners.Contains(listener))
                return;

            // Add the listener to the list
            Listeners.Add(listener);
        }
    }

    public virtual void RemoveListener(IParallaxOffsetListener listener)
    {
        if (listener == null)
            throw new ArgumentNullException(nameof(listener));

        lock (Listeners)
        {
            Listeners.Remove(listener);
        }
    }
    
    public double OffsetX { get; private set; }
    public double OffsetY { get; private set; }

    /// <summary>
    /// Notifies all listeners about the new parallax offset.
    /// The X and Y values will be clamped between -1 and 1.
    /// </summary>
    protected void NotifyListeners(double x, double y)
    {
        x = Math.Clamp(x, -1, 1);
        y = Math.Clamp(y, -1, 1);
        
        lock (Listeners)
        {
            foreach (var listener in Listeners)
            {
                listener.OnParallaxOffsetChanged(x, y);
            }
        }
        
        OffsetX = x;
        OffsetY = y;
        OnPropertyChanged(nameof(OffsetX));
        OnPropertyChanged(nameof(OffsetY));
    }
}
