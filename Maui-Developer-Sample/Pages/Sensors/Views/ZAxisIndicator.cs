namespace Maui_Developer_Sample.Pages.Sensors.Views;

public class ZAxisIndicator : BaseIndicator
{
    public ZAxisIndicator()
    {
        Drawable = new ZAxisDrawable(this);
    }

    #region Nested type: ZAxisDrawable

    private class ZAxisDrawable : IDrawable
    {
        private readonly ZAxisIndicator _parent;

        public ZAxisDrawable(ZAxisIndicator parent)
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

            dirtyRect = dirtyRect.Inflate(-2, -2); // Deflate the dirty rectangle to avoid drawing on the border

            var value = _parent.Value;

            if (value > _parent.MaxValue * _parent.Tolerance)
            {
                // Draw positive value
                canvas.FillColor = Colors.DarkGreen;
                DrawValue(Math.Abs(value / _parent.MaxValue), canvas, dirtyRect);
            }
            else if (value < _parent.MinValue * _parent.Tolerance)
            {
                // Draw negative value
                canvas.FillColor = Colors.DarkRed;
                DrawValue(Math.Abs(value / _parent.MinValue), canvas, dirtyRect);
            }
            else
            {
                // Perfect 0
                canvas.FillColor = Colors.Gray;
                DrawZeroValue(canvas, dirtyRect);
            }
        }

        #endregion

        public void DrawValue(float value, ICanvas canvas, RectF dirtyRect)
        {
            var x = dirtyRect.X + dirtyRect.Width / 2 * (1 - value);
            var y = dirtyRect.Y + dirtyRect.Height / 2 * (1 - value);
            var width = dirtyRect.Width * value;
            var height = dirtyRect.Height * value;
            canvas.FillRectangle(x, y, width, height);
        }

        public void DrawZeroValue(ICanvas canvas, RectF dirtyRect)
        {
            var x = dirtyRect.X + dirtyRect.Width / 2 * (1 - _parent.Tolerance);
            var y = dirtyRect.Y + dirtyRect.Height / 2 * (1 - _parent.Tolerance);
            var width = dirtyRect.Width * _parent.Tolerance;
            var height = dirtyRect.Height * _parent.Tolerance;
            canvas.FillRectangle(x, y, width, height);
        }
    }

    #endregion

}
