namespace Maui_Developer_Sample.Pages.Sensors.Views;

public partial class BidirectionalIndicator : BaseIndicator
{
    public readonly static BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation),
        typeof(StackOrientation),
        typeof(BidirectionalIndicator),
        propertyChanged: (bindable, oldValue, newValue) => {
            var control = (BidirectionalIndicator) bindable;
            control.Invalidate();
        });
    
    public BidirectionalIndicator()
    {
        Drawable = new BidirectionalDrawable(this);
    }

    public StackOrientation Orientation
    {
        get => (StackOrientation) GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }
    

    private class BidirectionalDrawable : IDrawable
    {
        private readonly BidirectionalIndicator _parent;

        public BidirectionalDrawable(BidirectionalIndicator parent)
        {
            _parent = parent;
        }

        public void DrawHorizontalPositiveValue(float value, ICanvas canvas, RectF dirtyRect)
        {
            var halfWidth = 0.5f * dirtyRect.Width;
            
            var x = dirtyRect.X + halfWidth; // Starts in the middle
            var y = dirtyRect.Y; // Starts at the top
            var width = halfWidth * value; // Variable width
            var height = dirtyRect.Height; // Full height

            canvas.FillRectangle(x, y, width, height);
        }

        public void DrawHorizontalNegativeValue(float value, ICanvas canvas, RectF dirtyRect)
        {
            var halfWidth = 0.5f * dirtyRect.Width;
            
            var x = dirtyRect.X + halfWidth - halfWidth * value; // Starts variable
            var y = dirtyRect.Y; // Starts at the top
            var width = halfWidth * value; // Variable width
            var height = dirtyRect.Height; // Full height

            canvas.FillRectangle(x, y, width, height);
        }
        
        public void DrawHorizontalZeroValue(ICanvas canvas, RectF dirtyRect)
        {
            var x = dirtyRect.X + ((0.5f - _parent.Tolerance / 2.0f) * dirtyRect.Width); // Starts center - tolerance
            var y = dirtyRect.Y; // Starts at the top
            var width = (_parent.Tolerance * dirtyRect.Width); // tolerance
            var height = dirtyRect.Height; // Full height

            canvas.FillRectangle(x, y, width, height);
        }

        public void DrawVerticalPositiveValue(float value, ICanvas canvas, RectF dirtyRect)
        {
            var halfHeight = 0.5f * dirtyRect.Height;
            
            var x = dirtyRect.X; // Starts at the left
            var y = dirtyRect.Y + halfHeight - halfHeight * value; // Starts variable
            var height = halfHeight * value; // Variable height
            var width = dirtyRect.Width; // Full width

            canvas.FillRectangle(x, y, width, height);
        }

        public void DrawVerticalNegativeValue(float value, ICanvas canvas, RectF dirtyRect)
        {
            var halfHeight = 0.5f * dirtyRect.Height;
            
            var x = dirtyRect.X; // Starts at the left
            var y = dirtyRect.Y + halfHeight; // Starts at the middle
            var height = halfHeight * value; // Variable height
            var width = dirtyRect.Width; // Full width

            canvas.FillRectangle(x, y, width, height);
        }
        
        public void DrawVerticalZeroValue(ICanvas canvas, RectF dirtyRect)
        {
            var x = dirtyRect.X; // Starts at the left
            var y = dirtyRect.Y + ((0.5f - _parent.Tolerance / 2.0f) * dirtyRect.Height); // Starts center - tolerance
            var height = (_parent.Tolerance * dirtyRect.Height); // tolerance
            var width = dirtyRect.Width; // Full width
            
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

            if (_parent.Orientation == StackOrientation.Horizontal) // Horizontal
            {
                if (value > _parent.MaxValue * _parent.Tolerance)
                {
                    // Draw positive value
                    canvas.FillColor = Colors.DarkGreen;
                    DrawHorizontalPositiveValue(Math.Min(1, Math.Abs(value/_parent.MaxValue)), canvas, dirtyRect);
                }
                else if (value < _parent.MinValue * _parent.Tolerance)
                {
                    // Draw negative value
                    canvas.FillColor = Colors.DarkRed;
                    DrawHorizontalNegativeValue(Math.Min(1, Math.Abs(value/_parent.MinValue)), canvas, dirtyRect);
                }
                else
                {
                    // Perfect 0
                    canvas.FillColor = Colors.Gray;
                    DrawHorizontalZeroValue(canvas, dirtyRect);
                }
            }
            else // Vertical
            {
                if (value > _parent.MaxValue * _parent.Tolerance)
                {
                    // Draw positive value
                    canvas.FillColor = Colors.DarkGreen;
                    DrawVerticalPositiveValue(Math.Min(1, Math.Abs(value/_parent.MaxValue)), canvas, dirtyRect);
                }
                else if (value < _parent.MinValue * _parent.Tolerance)
                {
                    // Draw negative value
                    canvas.FillColor = Colors.DarkRed;
                    DrawVerticalNegativeValue(Math.Min(1, Math.Abs(value/_parent.MinValue)), canvas, dirtyRect);
                }
                else
                {
                    // Perfect 0
                    canvas.FillColor = Colors.Gray;
                    DrawVerticalZeroValue(canvas, dirtyRect);
                }
            }
        }
    }
}
