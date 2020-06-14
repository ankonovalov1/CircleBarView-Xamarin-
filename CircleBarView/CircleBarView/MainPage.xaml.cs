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
        public MainPage()
        {
            InitializeComponent();   
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            btn1.Clicked += ClickMe;            
        }
        
        private void ClickMe(object sender, System.EventArgs e)
        {
            CircleBar.IsActive = true;
        }
       
    }
}
