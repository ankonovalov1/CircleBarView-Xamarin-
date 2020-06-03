using System;
using System.ComponentModel;
using System.Drawing;
using CoreGraphics;
using CoreText;
using Foundation;
using UIKit;
using Xamarin.Essentials;

namespace CircleBarView.iOS
{
    [Register("ProgressViewBar"), DesignTimeVisible(true)]
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
        public ProgressViewBarIOS(IntPtr p) : base(p)
        {
            Initialize();
        }
        void Initialize()
        {
            path = new CGPath();
            _timeLeftTextColor = UIColor.Gray;
            color112 = new UIColor[] {
                UIColor.FromRGB(246, 147, 38), // orange
                UIColor.FromRGB(251, 58, 87), // rose
                UIColor.FromRGB(251, 13, 23),  // dark rose
                UIColor.FromRGB(250, 203, 148), // light orange                
                UIColor.FromRGB(246, 147, 38), // orange
            };
            positions = new float[]
            {
                0.000f, 0.25f, 0.50f,
                0.75f, 0.999f
            };
        }

        CGPath path;
        private string timeLeft;
        private float _progress, radius, strokeWidth;
        private UIColor _backColor, _frontColor, _timeLeftTextColor, timeColor;
        private CGRect _ringDrawArea;
        private bool _timerIsRunning = false;
        private float _time;
        UIColor[] color112;
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
                        FrontColor = UIColor.FromRGB(251, 53, 114);
                    }
                    if (_timerIsRunning)
                    {
                        SetNeedsDisplay();
                    }
                }
            }
        }
        public UIColor BackColor { get => _backColor; set => _backColor = value; }
        public UIColor FrontColor { get => _frontColor; set => _frontColor = value; }
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
                    _timeLeftTextColor = UIColor.FromName("#fb3572");
                    SetNeedsDisplay();
                }
            }
        }
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            using (CGContext context = UIGraphics.GetCurrentContext())
            {

                float strokeWidth = 0.0f;

                var displayDensity = UIScreen.MainScreen.Scale;
                strokeWidth = (float)Math.Ceiling(12 * displayDensity);


                var ringAreaSize = Math.Min(Bounds.Width, Bounds.Height);

                radius = (float)(ringAreaSize / 2f) - (strokeWidth / 2f);


                _ringDrawArea = new CGRect(Bounds.GetMidX(), Bounds.GetMidY(), Bounds.Width, Bounds.Height);




                DrawBackgroundCircle(context, Bounds.GetMidX(), Bounds.GetMidY(), radius);
                DrawProgressRing(context, Bounds.GetMidX(), Bounds.GetMidY(), radius, _progress, UIColor.Red, UIColor.Blue);
                DrawTimer(context, _ringDrawArea.Left + 50, _ringDrawArea.Top + 50);
            }


        }

        private void DrawBackgroundCircle(CGContext context, nfloat x, nfloat y, nfloat ringDiameter)
        {
            //_paint.SetShader(null);
            //_paint.SetStyle(Paint.Style.Fill);
            //_paint.Color = Color.DarkGray;
            context.SetFillColor(UIColor.DarkGray.CGColor);
            path.AddArc(x, y, ringDiameter, 270, 360, false);
            context.AddPath(path);
            context.DrawPath(CGPathDrawingMode.Fill);


        }
        private void DrawProgressRing(CGContext context, nfloat x, nfloat y, nfloat ringDiameter, float progress,
                                      UIColor backColor,
                                      UIColor frontColor)
        {
            context.SetStrokeColor(backColor.CGColor);
            path.AddArc(x, y, ringDiameter, 270, 360, false);
            context.AddPath(path);
            context.DrawPath(CGPathDrawingMode.Stroke);

            if (progress >= 0.9)
            {
                var x1 = 0;
                //_paint.SetShader(null);
            }
            else
            {
                //SweepGradient sweepShader = new SweepGradient(_ringDrawArea.CenterX(), _ringDrawArea.CenterY(), color112, positions);
                //_paint.SetShader(sweepShader);
            }

            context.SetStrokeColor(frontColor.CGColor);
            path.AddArc(x, y, ringDiameter, 270, 270 + (360 * progress), false);
            context.AddPath(path);
            context.DrawPath(CGPathDrawingMode.Stroke);
        }

        private void DrawTimer(CGContext context, nfloat X, nfloat Y)
        {
            //_paint.SetShader(null);
            //_paint.ClearShadowLayer();
            string timeInterval = TimeToString();
            context.SetFillColor(UIColor.Brown.CGColor); // timeColor


            DrawText(context, timeInterval, 20, X + 60, Y + 25, 80);
            //paint.Color = _timeLeftTextColor;
            //if (timeLeft == "EXPIRED")
            //{
            //    paint.SetTypeface(Typeface.DefaultBold);
            //    X += 20;
            //}

            DrawText(context, timeLeft, 10, X + 90, Y + 75, 28);
        }

        private string TimeToString()
        {
            TimeSpan interval = TimeSpan.FromSeconds(_time);
            return interval.ToString("mm\\:ss");
        }

        private void DrawText(CGContext context, string text, int textHeight, nfloat X, nfloat Y, int textSize)
        {
            var x = X;
            var y = Y + textHeight;

            context.TranslateCTM(x, y);

            context.ScaleCTM(1, -1);
            context.SetFillColor(UIColor.Red.CGColor);

            var attributedString = new NSAttributedString(text,
                new CTStringAttributes
                {
                    ForegroundColorFromContext = true,
                    Font = new CTFont("Arial", textSize)
                });

            using (var textLine = new CTLine(attributedString))
            {
                textLine.Draw(context);
            }
        }

    }
}