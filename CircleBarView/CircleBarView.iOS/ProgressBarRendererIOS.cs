using CircleBarView;
using CircleBarView.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CircleBarCustomView), typeof(ProgressBarRendererIOS))]
namespace CircleBarView.iOS
{
    public class ProgressBarRendererIOS : ViewRenderer<CircleBarCustomView, ProgressViewBarIOS>
    {
        ProgressViewBarIOS circleBar;

        protected override void OnElementChanged(ElementChangedEventArgs<CircleBarCustomView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                circleBar = new ProgressViewBarIOS();

                SetNativeControl(circleBar);
            }
            if (e.NewElement != null)
            {
                circleBar.Progress = e.NewElement.Progress;
                circleBar.BackColor = e.NewElement.BackColor.ToUIColor();
                circleBar.FrontColor = e.NewElement.FrontColor.ToUIColor();
                circleBar.TimerIsRunning = e.NewElement.TimerIsRunning;
                circleBar.Time = e.NewElement.Time;
                circleBar.TimeLeft = e.NewElement.TimeLeft;
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
                Control.BackColor = Element.BackColor.ToUIColor();
            }
            if (e.PropertyName == CircleBarCustomView.FrontColorProperty.PropertyName)
            {
                Control.FrontColor = Element.FrontColor.ToUIColor();
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


        }
    }
}