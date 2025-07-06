namespace Maui_Developer_Sample.Pages.UI.Views;

public class DrawArcGraphicsView : GraphicsView
{
    public DrawArcGraphicsView()
    {
        Drawable = new DrawArcGraphicsDrawable(this);
    }
    public readonly static BindableProperty OffsetAngleProperty = BindableProperty.Create(nameof(OffsetAngle),
                                                                                         typeof(float),
                                                                                         typeof(DrawArcGraphicsView),
                                                                                         defaultValue: 90.0f,
                                                                                         propertyChanged: (bindable, oldValue, newValue) => {
                                                                                             var control = (DrawArcGraphicsView) bindable;
                                                                                             control.Invalidate();
                                                                                         });

    public float OffsetAngle
    {
        get => (float) GetValue(OffsetAngleProperty);
        set => SetValue(OffsetAngleProperty, value);
    }

    public readonly static BindableProperty StartAngleProperty = BindableProperty.Create(nameof(StartAngle),
                                                                                         typeof(float),
                                                                                         typeof(DrawArcGraphicsView),
                                                                                         defaultValue: 0.0f,
                                                                                         propertyChanged: (bindable, oldValue, newValue) => {
                                                                                             var control = (DrawArcGraphicsView) bindable;
                                                                                             control.Invalidate();
                                                                                         });

    public float StartAngle
    {
        get => (float) GetValue(StartAngleProperty);
        set => SetValue(StartAngleProperty, value);
    }

    public readonly static BindableProperty EndAngleProperty = BindableProperty.Create(nameof(EndAngle),
                                                                                       typeof(float),
                                                                                       typeof(DrawArcGraphicsView),
                                                                                       defaultValue: 0.0f,
                                                                                       propertyChanged: (bindable, oldValue, newValue) => {
                                                                                           var control = (DrawArcGraphicsView) bindable;
                                                                                           control.Invalidate();
                                                                                       });

    public float EndAngle
    {
        get => (float) GetValue(EndAngleProperty);
        set => SetValue(EndAngleProperty, value);
    }

    public readonly static BindableProperty ClockwiseProperty = BindableProperty.Create(nameof(Clockwise),
                                                                                        typeof(bool),
                                                                                        typeof(DrawArcGraphicsView),
                                                                                        defaultValue: true,
                                                                                        propertyChanged: (bindable, oldValue, newValue) => {
                                                                                            var control = (DrawArcGraphicsView) bindable;
                                                                                            control.Invalidate();
                                                                                        });

    public bool Clockwise
    {
        get => (bool) GetValue(ClockwiseProperty);
        set => SetValue(ClockwiseProperty, value);
    }

    private class DrawArcGraphicsDrawable : IDrawable
    {
        private readonly DrawArcGraphicsView _parent;

        public DrawArcGraphicsDrawable(DrawArcGraphicsView parent)
        {
            _parent = parent;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // Draw border
            canvas.StrokeColor = Colors.Gray;
            canvas.StrokeSize = 2;
            canvas.DrawRectangle(dirtyRect);

            dirtyRect = dirtyRect.Inflate(-10, -10); // Deflate the dirty rectangle to avoid drawing on the border
            canvas.DrawArc(dirtyRect,0,
                           1,
                           true,
                           true);

            canvas.StrokeSize = 10;
            canvas.StrokeColor = Colors.Gray;
            canvas.DrawArc(dirtyRect,
                           _parent.StartAngle + _parent.OffsetAngle,
                           _parent.EndAngle + _parent.OffsetAngle,
                           _parent.Clockwise,
                           false);
            
            canvas.StrokeSize = 16;
            canvas.StrokeColor = Colors.Green;
            canvas.DrawArc(dirtyRect,
                           _parent.StartAngle + _parent.OffsetAngle -1,
                           _parent.StartAngle + _parent.OffsetAngle +1,
                           false,
                           false);
            canvas.StrokeColor = Colors.Red;
            canvas.DrawArc(dirtyRect,
                           _parent.EndAngle + _parent.OffsetAngle -1,
                           _parent.EndAngle + _parent.OffsetAngle +1,
                           false,
                           false);
            canvas.StrokeSize = 12;
            canvas.StrokeColor = Colors.Blue;
            canvas.DrawArc(dirtyRect,
                           _parent.OffsetAngle - 1,
                           _parent.OffsetAngle + 1,
                           false,
                           false);
        }
    }
}
