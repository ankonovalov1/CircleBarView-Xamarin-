using System;
using System.Drawing;

using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Essentials;

namespace CircleBarView.iOS
{
    [Register("ProgressViewBar")]
    public class ProgressViewBarIOS : UIView
    {
        public ProgressViewBarIOS()
        {
            Initialize();
        }

        public ProgressViewBarIOS(RectangleF bounds) : base(bounds)
        {
            Initialize();
        }

        void Initialize()
        {
            _timeLeftTextColor = UIColor.Gray;
            color112 = new UIColor[] {
                UIColor.FromName("#f69326"), // orange
                UIColor.FromName("#fb3a57"), // rose
                UIColor.FromName("#fb0d17"),  // dark rose
                UIColor.FromName("#facb94"), // light orange                
                UIColor.FromName("#f69326"), // orange
            };
            positions = new float[]
            {
                0.000f, 0.25f, 0.50f,
                0.75f, 0.999f
            };
        }

        //private string timeLeft = CircleBarStaticResources.TIME_LEFT;
        //private float _progress, ringDiameter;
        private UIColor _backColor, _frontColor, _timeLeftTextColor;
        //private RectangleF _ringDrawArea;
        //private bool _sizeChanged = false;
        //private bool _timerIsRunning = false;
        //private float _time;
        UIColor[] color112;
        float[] positions;

        //public float Progress
        //{
        //    get
        //    {
        //        return _progress;
        //    }
        //    set
        //    {
        //        if (_progress != 1.0f)
        //        {
        //            _progress = value;
        //            if (_progress >= 0.9)
        //            {
        //                FrontColor = UIColor.FromName("#fb3572");
        //            }
        //            if (_timerIsRunning)
        //            {
        //                SetNeedsDisplay();
        //            }
        //        }
        //    }
        //}
        //public Color BackColor { get => _backColor.ToSystemColor(); set => _backColor = value.ToPlatformColor(); }
        //public Color FrontColor { get => _frontColor.ToSystemColor(); set => _frontColor = value.ToPlatformColor(); }
        //public float Time { get => _time; set => _time = value; }
        //public bool TimerIsRunning { get => _timerIsRunning; set => _timerIsRunning = value; }
        //public string TimeLeft
        //{
        //    get
        //    {
        //        return timeLeft;
        //    }
        //    set
        //    {
        //        timeLeft = value;
        //        if (timeLeft == "EXPIRED")
        //        {
        //            _timeLeftTextColor = UIColor.FromName("#fb3572");
        //            SetNeedsDisplay();
        //        }
        //    }
        //}
        //public override void Draw(CGRect rect)
        //{
        //    base.Draw(rect);

        //    using (CGContext g = UIGraphics.GetCurrentContext())
        //    {
        //        float strokeWidth = 0.0f;

        //        var displayDensity = UIScreen.MainScreen.Scale;
        //        strokeWidth = (float)Math.Ceiling(30 * displayDensity);


        //        if (_ringDrawArea == null || _sizeChanged)
        //        {
        //            _sizeChanged = false;

        //            var ringAreaSize = Math.Min(Bounds.Width, Bounds.Height);

        //            ringDiameter = (float)ringAreaSize - strokeWidth;

        //            var left = Bounds.Left;
        //            var top = Bounds.Top;

        //            _ringDrawArea = new RectF(left, top, left + ringDiameter, top + ringDiameter);
        //        }


        //        DrawBackgroundCircle(rect);
        //        DrawProgressRing(rect, _progress, _backColor, _frontColor);
        //        DrawTimer(rect, _ringDrawArea.Left + 50, _ringDrawArea.CenterY(), _paint);
        //    };
        //}

        //private void DrawBackgroundCircle(CGRect rect)
        //{
        //    _paint.SetShader(null);
        //    _paint.SetStyle(Paint.Style.Fill);
        //    _paint.Color = Color.DarkGray;
        //    canvas.DrawCircle(_ringDrawArea.CenterX(), _ringDrawArea.CenterY(), ringDiameter / 2, _paint);
        //}
        //private void DrawProgressRing(CGRect rect, float progress,
        //                              UIColor backColor,
        //                              UIColor frontColor)
        //{
        //    _paint.SetShader(null);
        //    _paint.Color = backColor;
        //    _paint.SetStyle(Paint.Style.Stroke);
        //    canvas.DrawArc(_ringDrawArea, 270, 360, false, _paint);

        //    if (progress >= 0.9)
        //    {
        //        _paint.SetShader(null);
        //    }
        //    else
        //    {
        //        SweepGradient sweepShader = new SweepGradient(_ringDrawArea.CenterX(), _ringDrawArea.CenterY(), color112, positions);
        //        _paint.SetShader(sweepShader);
        //    }
        //    _paint.Color = frontColor;
        //    canvas.DrawArc(_ringDrawArea, 270, 360 * progress, false, _paint);
        //}

        //private void DrawTimer(CGRect rect, float X, float Y, Paint paint)
        //{
        //    _paint.SetShader(null);
        //    string timeInterval = TimeToString();
        //    _paint.SetStyle(Paint.Style.Fill);
        //    paint.Color = Color.Orange;
        //    paint.TextSize = 100;
        //    canvas.DrawText(timeInterval, X + 50, Y, paint);
        //    paint.Color = _timeLeftTextColor;
        //    paint.TextSize = 40;
        //    canvas.DrawText(timeLeft, X + 100, Y + 70, paint);
        //}

        //private string TimeToString()
        //{
        //    TimeSpan interval = TimeSpan.FromSeconds(_time);
        //    return interval.ToString("mm\\:ss");
        //}

    }
}