using System;
using Android.Animation;
using Android.Content;
using Android.Graphics;
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

        private string timeLeft;
        private float _progress, ringDiameter, strokeWidth;
        private Color _backColor, _frontColor, _timeLeftTextColor, timeColor;
        private Paint _paint;
        private RectF _ringDrawArea;
        private bool _sizeChanged = false;
        private bool _timerIsRunning = false;
        private float _time;
        int[] color112;
        float[] positions;
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
                    if (_timerIsRunning)
                    {
                        Invalidate();
                    }
                }
            }
        }
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
       
        public float Time { get => _time; set => _time = value; }
        public bool TimerIsRunning { get => _timerIsRunning; set => _timerIsRunning = value; }
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
            if (_paint == null)
            {
                var displayDensity = Context.Resources.DisplayMetrics.Density;
                strokeWidth = (float)Math.Ceiling(12 * displayDensity);

                _paint = new Paint();
                _paint.StrokeWidth = strokeWidth;
                _paint.SetStyle(Paint.Style.Stroke);
                _paint.Flags = PaintFlags.AntiAlias;

            }

            if (_ringDrawArea == null || _sizeChanged)
            {
                _sizeChanged = false;

                var ringAreaSize = Math.Min(canvas.ClipBounds.Width(), canvas.ClipBounds.Height());
                
                ringDiameter = ringAreaSize - _paint.StrokeWidth;
                

                var left = canvas.ClipBounds.CenterX() - ringDiameter / 2;
                var top = canvas.ClipBounds.CenterY() - ringDiameter / 2;

                _ringDrawArea = new RectF(left, top, left + ringDiameter, top + ringDiameter);
                
            }
            
            DrawBackgroundCircle(canvas);
            DrawProgressRing(canvas, _progress, _backColor, _frontColor);
            DrawTimer(canvas, _ringDrawArea.Left, _ringDrawArea.CenterY(), _paint);
        }
        private void DrawBackgroundCircle(Canvas canvas)
        {
            _paint.SetShader(null);
            _paint.SetShadowLayer(ringDiameter / 6, 0, 0, Color.ParseColor("#fb3a57"));
            _paint.SetStyle(Paint.Style.Fill);
            _paint.Color = Color.ParseColor("#1b1d23");
            canvas.DrawCircle(_ringDrawArea.CenterX(), _ringDrawArea.CenterY(), ringDiameter / 2, _paint);
        }
        private void DrawProgressRing(Canvas canvas, float progress,
                                      Color backColor,
                                      Color frontColor)
        {
            _paint.SetShader(null);
            _paint.ClearShadowLayer();
            _paint.StrokeCap = Paint.Cap.Round;
            _paint.Color = backColor;            
            _paint.SetStyle(Paint.Style.Stroke);
            canvas.DrawArc(_ringDrawArea, 270, 360, false, _paint);

            if (progress >= 0.9)
            {
                _paint.SetShader(null);
            }
            else
            {                
                SweepGradient sweepShader = new SweepGradient(_ringDrawArea.CenterX(), _ringDrawArea.CenterY(), color112, positions);
                _paint.SetShader(sweepShader);
            }
            _paint.SetShadowLayer(strokeWidth / 2, 0, 0, Color.ParseColor("#fb3a57"));
            _paint.Color = frontColor;
            canvas.DrawArc(_ringDrawArea, 270, 360 * progress, false, _paint);
        }

        private void DrawTimer(Canvas canvas, float X, float Y, Paint paint)
        {
            _paint.SetShader(null);
            _paint.ClearShadowLayer();
            string timeInterval = TimeToString();
            _paint.SetStyle(Paint.Style.Fill);
            paint.Color = timeColor;
            paint.TextSize = 80;
            canvas.DrawText(timeInterval, X + 60, Y + 25, paint);
            paint.Color = _timeLeftTextColor;
            if (timeLeft == "EXPIRED")
            {
                paint.SetTypeface(Typeface.DefaultBold);
                X += 20;
            }
            paint.TextSize = 28;
            canvas.DrawText(timeLeft, X + 90, Y + 75, paint);
        }

        private string TimeToString()
        {
            TimeSpan interval = TimeSpan.FromSeconds(_time);
            return interval.ToString("mm\\:ss");
        }

        
    }
}