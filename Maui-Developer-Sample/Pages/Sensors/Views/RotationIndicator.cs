namespace Maui_Developer_Sample.Pages.Sensors.Views;

public class RotationIndicator : BaseIndicator
{
    public RotationIndicator()
    {
        Drawable = new RotationDrawable(this);
    }

    #region Nested type: RotationDrawable

    private class RotationDrawable : IDrawable
    {
        private readonly RotationIndicator _parent;

        public RotationDrawable(RotationIndicator parent)
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

            var arcRect = dirtyRect.Inflate(-10, -10); // Deflate the dirty rectangle to avoid drawing on the border
            canvas.StrokeSize = 1;
            canvas.DrawArc(arcRect,
                           0f,
                           1f,
                           true,
                           true); // Draw the full circle as background

            var value = (float) Math.Round(_parent.Value, 2);
            canvas.StrokeSize = 10;

            if (value > _parent.MaxValue * _parent.Tolerance)
            {
                // Draw positive value
                canvas.StrokeColor = Colors.DarkGreen;
                DrawPositiveValue(Math.Abs(value / _parent.MaxValue), canvas, arcRect);
            }
            else if (value < _parent.MinValue * _parent.Tolerance)
            {
                // Draw negative value
                canvas.StrokeColor = Colors.DarkRed;
                DrawNegativeValue(Math.Abs(value / _parent.MinValue), canvas, arcRect);
            }
            else
            {
                // Perfect 0
                canvas.StrokeColor = Colors.Gray;
                DrawZeroValue(canvas, arcRect);
            }
        }

        #endregion

        public void DrawNegativeValue(float value, ICanvas canvas, RectF dirtyRect)
        {
            canvas.DrawArc(dirtyRect,
                           90, // Start from the top
                           90 + value * 180,
                           false,
                           false);
        }

        public void DrawPositiveValue(float value, ICanvas canvas, RectF dirtyRect)
        {
            canvas.DrawArc(dirtyRect,
                           90, // Start from the top
                           90 - value * 180, // Draw clockwise
                           true,
                           false);
        }

        public void DrawZeroValue(ICanvas canvas, RectF dirtyRect)
        {
            canvas.DrawArc(dirtyRect,
                           90 + 180 * _parent.Tolerance,
                           90 - 180 * _parent.Tolerance,
                           true,
                           false);
        }
    }

    #endregion

}
