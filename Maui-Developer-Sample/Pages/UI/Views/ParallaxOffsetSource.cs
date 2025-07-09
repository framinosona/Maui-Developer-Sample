using Maui_Developer_Sample.Helpers;
using Maui_Developer_Sample.Services;

namespace Maui_Developer_Sample.Pages.UI.Views;

/// <summary>
/// Abstract base class for parallax offset sources that provide X and Y values from -1 to 1.
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

    // Useful for cumulative calculation
    public double CurrentOffsetX { get; private set; }
    public double CurrentOffsetY { get; private set; }

    /// <summary>
    /// Notifies all listeners about the new parallax offset.
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

        CurrentOffsetX = x;
        CurrentOffsetY = y;
        OnPropertyChanged(nameof(CurrentOffsetX));
        OnPropertyChanged(nameof(CurrentOffsetY));
    }
}
