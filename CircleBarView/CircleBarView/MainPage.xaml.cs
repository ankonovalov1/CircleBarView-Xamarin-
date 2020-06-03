using Android.Animation;
using Android.Text.Format;
using Android.Widget;
using Plugin.Toast;
using System;
using System.ComponentModel;
using System.Timers;
using Xamarin.Forms;

namespace CircleBarView
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {       
        private float updateRateQuantuty, progressIncreaseNumber;
        Timer timer;
        int count = 0;
        public MainPage()
        {
            InitializeComponent();            
            CircleBar.Progress = CircleBarStaticResources.ZERO_PROGRESS;
            CircleBar.Time = 30.0f;
            updateRateQuantuty = (float)CircleBarStaticResources.INVALIDATE_CALL_PER_SECOND * CircleBar.Time;
            progressIncreaseNumber = CircleBarStaticResources.MAX_PROGRESS / updateRateQuantuty;            
            timer = new Timer();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            btn1.Clicked += ClickMe;
        }
        
        private void ClickMe(object sender, System.EventArgs e)
        {            
            if(CircleBar.TimerIsRunning)
                return;
            else
            {
                timer.Interval = CircleBarStaticResources.INTERVAL_OF_INVALIDATE_CALL;
                timer.Elapsed += Timer_Elapsed;
                CircleBar.TimerIsRunning = true;
                timer.Start();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CircleFill();

        }

        private void CircleFill()
        {
            if (CircleBar.Progress <= CircleBarStaticResources.MAX_PROGRESS)
            {
                count++;
                CircleBar.Progress += progressIncreaseNumber;
                if (count >= CircleBarStaticResources.INVALIDATE_CALL_PER_SECOND)
                {
                    CircleBar.Time--;
                    count = 0;
                }
            }
            else
            {
                timer.Stop();
                CircleBar.TimerIsRunning = false;
                CircleBar.TimeLeft = CircleBarStaticResources.TIME_LEFT_EXPIRED;
            }
        }

       
    }
}
