namespace Maui_Developer_Sample.Pages.Sensors.Views;

public class MonodirectionalIndicator : BaseIndicator
{
    public readonly static BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation),
                                                                                          typeof(StackOrientation),
                                                                                          typeof(MonodirectionalIndicator),
                                                                                          propertyChanged: (bindable, oldValue, newValue) => {
                                                                                              var control = (MonodirectionalIndicator) bindable;
                                                                                              control.Invalidate();
                                                                                          });

    public MonodirectionalIndicator()
    {
        Drawable = new MonodirectionalDrawable(this);
    }

    public StackOrientation Orientation
    {
        get => (StackOrientation) GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    #region Nested type: MonodirectionalDrawable

    private class MonodirectionalDrawable : IDrawable
    {
        private readonly MonodirectionalIndicator _parent;

        public MonodirectionalDrawable(MonodirectionalIndicator parent)
        {
            _parent = parent;
        }

        #region IDrawable Members

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // Draw border
            canvas.StrokeColor = Colors.Gray;
            canvas.StrokeSize = 2;
            canvas.DrawRectangle(dirtyRect);

            dirtyRect = dirtyRect.Inflate(-2, -2); // Inflate the dirty rectangle to avoid drawing on the border

            var value = _parent.Value;

            if (_parent.Orientation == StackOrientation.Horizontal) // Horizontal
            {
                if (value > _parent.MinValue + _parent.Tolerance * (_parent.MaxValue - _parent.MinValue))
                {
                    canvas.FillColor = Colors.DodgerBlue;
                    DrawHorizontalValue((value - _parent.MinValue) / (_parent.MaxValue - _parent.MinValue), canvas, dirtyRect);
                }
                else
                {
                    canvas.FillColor = Colors.Gray;
                    DrawHorizontalZeroValue(canvas, dirtyRect);
                }
            }
            else // Vertical
            {
                if (value > _parent.MinValue + _parent.Tolerance * (_parent.MaxValue - _parent.MinValue))
                {
                    canvas.FillColor = Colors.DodgerBlue;
                    DrawVerticalValue((value - _parent.MinValue) / (_parent.MaxValue - _parent.MinValue), canvas, dirtyRect);
                }
                else
                {
                    canvas.FillColor = Colors.Gray;
                    DrawVerticalZeroValue(canvas, dirtyRect);
                }
            }
        }

        #endregion

        public void DrawHorizontalValue(float value, ICanvas canvas, RectF dirtyRect)
        {
            var x = dirtyRect.X; // Starts at the left
            var y = dirtyRect.Y; // Starts at the top
            var width = dirtyRect.Width * value; // Variable width
            var height = dirtyRect.Height; // Full height

            canvas.FillRectangle(x, y, width, height);
        }

        public void DrawVerticalValue(float value, ICanvas canvas, RectF dirtyRect)
        {
            var x = dirtyRect.X; // Starts at the left
            var y = dirtyRect.Y + dirtyRect.Height * (1 - value); // Starts variable
            var height = dirtyRect.Height * value; // Variable height
            var width = dirtyRect.Width; // Full width

            canvas.FillRectangle(x, y, width, height);
        }

        public void DrawHorizontalZeroValue(ICanvas canvas, RectF dirtyRect)
        {
            var x = dirtyRect.X; // Starts at the left
            var y = dirtyRect.Y; // Starts at the top
            var width = dirtyRect.Width * _parent.Tolerance; // tolerance
            var height = dirtyRect.Height; // Full height

            canvas.FillRectangle(x, y, width, height);
        }

        public void DrawVerticalZeroValue(ICanvas canvas, RectF dirtyRect)
        {
            var toleranceHeight = dirtyRect.Height * _parent.Tolerance;

            var x = dirtyRect.X; // Starts at the left
            var y = dirtyRect.Y + dirtyRect.Height - toleranceHeight; // Starts variable
            var height = toleranceHeight; // tolerance
            var width = dirtyRect.Width; // Full width

            canvas.FillRectangle(x, y, width, height);
        }
    }

    #endregion

}
