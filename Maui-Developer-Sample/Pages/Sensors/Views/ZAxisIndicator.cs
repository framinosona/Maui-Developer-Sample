using System.Diagnostics;

namespace Maui_Developer_Sample.Pages.Sensors.Views;
public class ZAxisIndicator : BaseIndicator
{
    public ZAxisIndicator()
    {
        Drawable = new ZAxisDrawable(this);
    }

    private class ZAxisDrawable : IDrawable
    {
        private readonly ZAxisIndicator _parent;

        public ZAxisDrawable(ZAxisIndicator parent)
        {
            _parent = parent;
        }
        
        public void DrawValue(float value, ICanvas canvas, RectF dirtyRect)
        {
            float x = dirtyRect.X + (dirtyRect.Width / 2) * (1 - value);
            float y = dirtyRect.Y + (dirtyRect.Height / 2) * (1 - value);
            float width = dirtyRect.Width * value;
            float height = dirtyRect.Height * value;
            canvas.FillRectangle(x, y, width, height);
        }
        
        public void DrawZeroValue(ICanvas canvas, RectF dirtyRect)
        {
            float x = dirtyRect.X + (dirtyRect.Width / 2) * (1 - _parent.Tolerance);
            float y = dirtyRect.Y + (dirtyRect.Height / 2) * (1 - _parent.Tolerance);
            float width = dirtyRect.Width * _parent.Tolerance;
            float height = dirtyRect.Height * _parent.Tolerance;
            canvas.FillRectangle(x, y, width, height);
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // Draw border
            canvas.StrokeColor = Colors.Gray;
            canvas.StrokeSize = 2;
            canvas.DrawRectangle(dirtyRect);
            
            dirtyRect = dirtyRect.Inflate(-2, -2); // Inflate the dirty rectangle to avoid drawing on the border

            float value = _parent.Value;
            
            if (value > _parent.MaxValue * _parent.Tolerance)
            {
                // Draw positive value
                canvas.FillColor = Colors.DarkGreen;
                DrawValue(Math.Abs(value/_parent.MaxValue), canvas, dirtyRect);
            }
            else if (value < _parent.MinValue * _parent.Tolerance)
            {
                // Draw negative value
                canvas.FillColor = Colors.DarkRed;
                DrawValue(Math.Abs(value/_parent.MinValue), canvas, dirtyRect);
            }
            else
            {
                // Perfect 0
                canvas.FillColor = Colors.Gray;
                DrawZeroValue(canvas, dirtyRect);
            }
        }
    }
}