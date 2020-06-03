using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = Android.Graphics.Color;

namespace CircleBarView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CircleBarCustomView : ContentView
    {

        public CircleBarCustomView()
        {

        }

        public static readonly BindableProperty ProgressProperty = BindableProperty.Create("Progress", typeof(float),
                                                                                                    typeof(CircleBarCustomView), 0.0F);
        public float Progress
        {
            get { return (float)base.GetValue(ProgressProperty); }
            set { base.SetValue(ProgressProperty, value); }
        }

        public static readonly BindableProperty BackColorProperty = BindableProperty.Create("BackColor", typeof(Color),
                                                                                                       typeof(CircleBarCustomView), Color.ParseColor("#38192f"));
        public Color BackColor
        {
            get { return (Color)base.GetValue(BackColorProperty); }
            set { base.SetValue(BackColorProperty, value); }
        }


        public static readonly BindableProperty FrontColorProperty = BindableProperty.Create("FrontColor", typeof(Color),
                                                                                                       typeof(CircleBarCustomView), Color.ParseColor("#fd7a00"));
        public Color FrontColor
        {
            get { return (Color)base.GetValue(FrontColorProperty); }
            set { base.SetValue(FrontColorProperty, value); }
        }

        public static readonly BindableProperty TimeProperty = BindableProperty.Create("Time", typeof(float),
                                                                                                     typeof(CircleBarCustomView), 60.0f);
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

    }
}