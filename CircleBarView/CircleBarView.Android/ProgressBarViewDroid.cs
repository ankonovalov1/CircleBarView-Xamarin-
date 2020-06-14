using System;
using System.Timers;
using Android.Content;
using Android.Graphics;
using Android.Renderscripts;
using Android.Runtime;
using Android.Util;
using Android.Views;
using SkiaSharp;

namespace CircleBarView.Droid
{
    [Register("circlebarview.android.views.ProgressBarView")]
    public class ProgressBarViewDroid : View
    {
        public ProgressBarViewDroid(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();            
        }

        public ProgressBarViewDroid(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();            
        }

        private void Initialize()
        {
            displayDensity = Context.Resources.DisplayMetrics.Density;
            _timeLeftTextColor = Color.Gray;
            timeColor = Color.ParseColor("#f69326");
            timeLeft = CircleBarStaticResources.TIME_LEFT;
            color112 = new int[] {
                Color.ParseColor("#f69326"), // orange
                Color.ParseColor("#fb3a57"), // rose
                Color.ParseColor("#fb0d17"),  // dark rose
                Color.ParseColor("#facb94"), // light orange                 
                Color.ParseColor("#f69326"), // orange
            };
            positions = new float[]
            {
                0.000f, 0.25f, 0.50f,
                0.75f, 0.999f
            };            
        }
        private int ringAreaSize;
        private string timeLeft;
        private float _progress, internalDiameter, strokeWidth;
        private Color _backColor, _frontColor, _timeLeftTextColor, timeColor;
        private Paint _paint;
        private RectF _ringInternalArea;
        private float displayDensity;
        int[] color112;
        float[] positions;

        public Color BackColor { get => _backColor; set => _backColor = value; }
        public Color FrontColor
        {
            get
            {
                return _frontColor;
            }
            set
            {
                _frontColor = value;
                timeColor = _frontColor;
            }
        }

        public bool IsActive { get; set; }
        public float Time { get; set; }    
        public float StrokeWidth
        {
            get { return strokeWidth; }
            set
            {
                strokeWidth = value;
            }
        }
        public bool TimerIsRunning { get; set; }
        public float Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                if (_progress != 1.0f)
                {
                    _progress = value;
                    if (_progress >= 0.9)
                    {
                        FrontColor = Color.ParseColor("#c60e3b");
                    }
                    if (TimerIsRunning)
                    {
                        Invalidate();
                    }
                }
            }
        }
        public string TimeLeft
        {
            get
            {
                return timeLeft;
            }
            set
            {
                timeLeft = value;
                if (timeLeft == "EXPIRED")
                {
                    _timeLeftTextColor = Color.ParseColor("#c60e3b");
                    Invalidate();
                }
            }
        }        

        protected override void OnDraw(Canvas canvas)
        {            
            ringAreaSize = Math.Min(canvas.ClipBounds.Width(), canvas.ClipBounds.Height());            

            if (_ringInternalArea == null)
            {
                internalDiameter = ringAreaSize - strokeWidth * displayDensity;                

                var internalLeft = canvas.ClipBounds.CenterX() - internalDiameter / 2;
                var internalTop = canvas.ClipBounds.CenterY() - internalDiameter / 2;

                _ringInternalArea = new RectF(internalLeft, internalTop, internalLeft + internalDiameter, internalTop + internalDiameter);
            }                           
            
            if(_paint == null)
            {
                _paint = new Paint();
                _paint.StrokeWidth = strokeWidth * displayDensity;
                _paint.Flags = PaintFlags.AntiAlias;
            }            

            DrawBackgroundCircle(canvas);
            DrawProgressRing(canvas, _progress, _backColor, _frontColor);
            DrawTimer(canvas);
        }
        private void DrawBackgroundCircle(Canvas canvas)
        {
            _paint.SetShader(null);            
            _paint.SetShadowLayer(ringAreaSize / 14, 0, 0, Color.ParseColor("#fb3a57"));
            _paint.SetStyle(Paint.Style.Fill);
            _paint.Color = Color.ParseColor("#1b1d23"); 
            canvas.DrawCircle(_ringInternalArea.CenterX(), _ringInternalArea.CenterY(), internalDiameter / 2, _paint);
        }
        private void DrawProgressRing(Canvas canvas, float progress,
                                      Color backColor,
                                      Color frontColor)
        {            
            _paint.SetShader(null);
            _paint.ClearShadowLayer();            
            _paint.Color = backColor;
            _paint.SetStyle(Paint.Style.Stroke);
            
            canvas.DrawArc(_ringInternalArea, 270, 360, false, _paint);

            if (progress >= 0.9)
            {
                _paint.SetShader(null);
            }
            else
            {
                SweepGradient sweepShader = new SweepGradient(_ringInternalArea.CenterX(), _ringInternalArea.CenterY(), color112, positions);
                _paint.SetShader(sweepShader);
            }
            _paint.SetShadowLayer(10, 0, 0, Color.ParseColor("#fb3a57"));
            _paint.StrokeCap = Paint.Cap.Round;
            _paint.Color = frontColor;
            canvas.DrawArc(_ringInternalArea, 270, 360 * progress, false, _paint);
        }

        private void DrawTimer(Canvas canvas)
        {
            _paint.SetShader(null);
            _paint.ClearShadowLayer();

            string timeInterval = TimeToString();
            _paint.SetStyle(Paint.Style.Fill);
            _paint.Color = timeColor;
            _paint.TextSize = ringAreaSize / 10  * displayDensity;

            var timeIntervalYbias = ringAreaSize / 10 * displayDensity / 2;
            _paint.TextAlign = Paint.Align.Center;
            canvas.DrawText(timeInterval, canvas.ClipBounds.CenterX() , canvas.ClipBounds.CenterY() + timeIntervalYbias, _paint);

            _paint.Color = _timeLeftTextColor;
            _paint.TextSize = (ringAreaSize / 3) / 10 * displayDensity;
            var timeLeftYbias = 2.0f * timeIntervalYbias;
            
            if (timeLeft == "EXPIRED")
            {
                _paint.SetTypeface(Typeface.DefaultBold);                
            }
            canvas.DrawText(timeLeft, canvas.ClipBounds.CenterX(), canvas.ClipBounds.CenterY() + timeLeftYbias, _paint);
        }

        private string TimeToString()
        {
            TimeSpan interval = TimeSpan.FromSeconds(Time);
            return interval.ToString("mm\\:ss");
        }

    }
}