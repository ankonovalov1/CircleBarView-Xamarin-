using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CircleBarView;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CircleBarCustomView), typeof(ProgressBarRenderer))]
namespace CircleBarView.iOS
{
    public class ProgressBarRendererIOS : ViewRenderer<CircleBarCustomView, ProgressViewBarIOS>
    {
        ProgressViewBarIOS circleBar;

        //protected override void OnElementChanged(ElementChangedEventArgs<CircleBarCustomView> e)
        //{
        //    base.OnElementChanged(e);
        //    if (Control == null)
        //    {
        //        circleBar = new ProgressViewBarIOS();

        //        SetNativeControl(circleBar);
        //    }
        //    if (e.NewElement != null)
        //    {
        //        circleBar.Progress = e.NewElement.Progress;
        //        circleBar.BackColor = e.NewElement.BackColor;
        //        circleBar.FrontColor = e.NewElement.FrontColor;
        //        circleBar.TimerIsRunning = e.NewElement.TimerIsRunning;
        //        circleBar.Time = e.NewElement.Time;
        //        circleBar.TimeLeft = e.NewElement.TimeLeft;
        //    }

        //}

        //protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);

        //    if (Control == null || Element == null)
        //        return;

        //    if (e.PropertyName == CircleBarCustomView.ProgressProperty.PropertyName)
        //    {
        //        Control.Progress = Element.Progress;
        //    }
        //    if (e.PropertyName == CircleBarCustomView.BackColorProperty.PropertyName)
        //    {
        //        Control.BackColor = Element.BackColor;
        //    }
        //    if (e.PropertyName == CircleBarCustomView.FrontColorProperty.PropertyName)
        //    {
        //        Control.FrontColor = Element.FrontColor;
        //    }
        //    if (e.PropertyName == CircleBarCustomView.TimeProperty.PropertyName)
        //    {
        //        Control.Time = Element.Time;
        //    }
        //    if (e.PropertyName == CircleBarCustomView.TimerIsRunningProperty.PropertyName)
        //    {
        //        Control.TimerIsRunning = Element.TimerIsRunning;
        //    }
        //    if (e.PropertyName == CircleBarCustomView.TimeLeftProperty.PropertyName)
        //    {
        //        Control.TimeLeft = Element.TimeLeft;
        //    }


        //}
    }
}