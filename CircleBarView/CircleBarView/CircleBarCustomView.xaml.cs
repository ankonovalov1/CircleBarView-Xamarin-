using System;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CircleBarView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircleBarCustomView : ContentView
    {
        Timer timer;
        private float updateRateQuantuty, progressIncreaseNumber;        
        int count = 0;
        public CircleBarCustomView()
        {
            updateRateQuantuty = (float)CircleBarStaticResources.INVALIDATE_CALL_PER_SECOND * Time;
            progressIncreaseNumber = CircleBarStaticResources.MAX_PROGRESS / updateRateQuantuty;
            timer = new Timer();
        }

        public static readonly BindableProperty ProgressProperty = BindableProperty.Create("Progress", typeof(float),
                                                                                                    typeof(CircleBarCustomView), 0.0F);
        public float Progress
        {
            get { return (float)base.GetValue(ProgressProperty); }
            set { base.SetValue(ProgressProperty, value); }
        }

        public static readonly BindableProperty BackColorProperty = BindableProperty.Create("BackColor", typeof(Color),
                                                                                                       typeof(CircleBarCustomView), Color.FromHex("#38192f"));
        public Color BackColor
        {
            get { return (Color)base.GetValue(BackColorProperty); }
            set { base.SetValue(BackColorProperty, value); }
        }


        public static readonly BindableProperty FrontColorProperty = BindableProperty.Create("FrontColor", typeof(Color),
                                                                                                       typeof(CircleBarCustomView), Color.FromHex("#fd7a00"));
        public Color FrontColor
        {
            get { return (Color)base.GetValue(FrontColorProperty); }
            set { base.SetValue(FrontColorProperty, value); }
        }

        public static readonly BindableProperty TimeProperty = BindableProperty.Create("Time", typeof(float),
                                                                                                     typeof(CircleBarCustomView), 10.0f);
        public float Time
        {
            get { return (float)base.GetValue(TimeProperty); }
            set { base.SetValue(TimeProperty, value); }
        }
        public static readonly BindableProperty TimerIsRunningProperty = BindableProperty.Create("TimerIsRunning", typeof(bool),
                                                                                                     typeof(CircleBarCustomView), false);
        public bool TimerIsRunning
        {
            get { return (bool)base.GetValue(TimerIsRunningProperty); }
            set { base.SetValue(TimerIsRunningProperty, value); }
        }

        public static readonly BindableProperty TimeLeftProperty = BindableProperty.Create("TimeLeft", typeof(string),
                                                                                                     typeof(CircleBarCustomView), CircleBarStaticResources.TIME_LEFT);
        public string TimeLeft
        {
            get { return (string)base.GetValue(TimeLeftProperty); }
            set { base.SetValue(TimeLeftProperty, value); }
        }

        public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create("StrokeWidth", typeof(float),
                                                                                                     typeof(CircleBarCustomView), 15.0f);
        public float StrokeWidth
        {
            get { return (float)base.GetValue(StrokeWidthProperty); }
            set { base.SetValue(StrokeWidthProperty, value); }
        }

        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create("IsActive", typeof(bool),
                                                                                                     typeof(CircleBarCustomView), false, propertyChanged: onStartTimer);

        public bool IsActive
        {
            get { return (bool)base.GetValue(IsActiveProperty); }
            set { base.SetValue(IsActiveProperty, value); }
        }
        private static void onStartTimer(BindableObject bindable, object oldValue, object newValue)
        {
            ((CircleBarCustomView)bindable).StartTimer();
        }
        private void StartTimer()
        {
            if (TimerIsRunning)
                return;
            else
            {
                timer.Interval = CircleBarStaticResources.INTERVAL_OF_INVALIDATE_CALL;
                timer.Elapsed += Timer_Elapsed;
                TimerIsRunning = true;
                timer.Start();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CircleFill();
        }

        private void CircleFill()
        {
            if (Progress <= CircleBarStaticResources.MAX_PROGRESS)
            {
                count++;
                Progress += progressIncreaseNumber;
                if (count >= CircleBarStaticResources.INVALIDATE_CALL_PER_SECOND)
                {
                    Time--;
                    count = 0;
                }
            }
            else
            {
                timer.Stop();
                TimerIsRunning = false;
                TimeLeft = CircleBarStaticResources.TIME_LEFT_EXPIRED;
            }
        }
    }
}