using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CircleBarView;
using CircleBarView.Droid;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(CircleBarCustomView), typeof(ProgressRingBarRendererDroid))]
namespace CircleBarView.Droid

{
    public class ProgressRingBarRendererDroid : ViewRenderer<CircleBarCustomView, ProgressBarViewDroid>
    {
        ProgressBarViewDroid circleBar;
        public ProgressRingBarRendererDroid(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<CircleBarCustomView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                circleBar = new ProgressBarViewDroid(Context, null);
                
                SetNativeControl(circleBar);
            }
            if (e.NewElement != null)
            {
                circleBar.Progress = e.NewElement.Progress;
                circleBar.BackColor = e.NewElement.BackColor.ToAndroid();
                circleBar.FrontColor = e.NewElement.FrontColor.ToAndroid();
                circleBar.TimerIsRunning = e.NewElement.TimerIsRunning;
                circleBar.Time = e.NewElement.Time;
                circleBar.TimeLeft = e.NewElement.TimeLeft;
                circleBar.StrokeWidth = e.NewElement.StrokeWidth;
                circleBar.IsActive = e.NewElement.IsActive;
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null || Element == null)
                return;

            if (e.PropertyName == CircleBarCustomView.ProgressProperty.PropertyName)
            {
                Control.Progress = Element.Progress;
            }
            if (e.PropertyName == CircleBarCustomView.BackColorProperty.PropertyName)
            {
                Control.BackColor = Element.BackColor.ToAndroid();
            }
            if (e.PropertyName == CircleBarCustomView.FrontColorProperty.PropertyName)
            {
                Control.FrontColor = Element.FrontColor.ToAndroid();
            }
            if (e.PropertyName == CircleBarCustomView.TimeProperty.PropertyName)
            {
                Control.Time = Element.Time;
            }
            if (e.PropertyName == CircleBarCustomView.TimerIsRunningProperty.PropertyName)
            {
                Control.TimerIsRunning = Element.TimerIsRunning;
            }
            if (e.PropertyName == CircleBarCustomView.TimeLeftProperty.PropertyName)
            {
                Control.TimeLeft = Element.TimeLeft;
            }
            if (e.PropertyName == CircleBarCustomView.StrokeWidthProperty.PropertyName)
            {
                Control.StrokeWidth = Element.StrokeWidth;
            }
            if (e.PropertyName == CircleBarCustomView.IsActiveProperty.PropertyName)
            {
                Control.IsActive = Element.IsActive;
            }

        }

    }
}

        

