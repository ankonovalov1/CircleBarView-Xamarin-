using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CircleBarView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomView : ContentView
    {
        public BottomView()
        {
            InitializeComponent();
        }

        private void OpenGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            openLabel.Text = "OOO yeah!";
        }

        private void SkipGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            skipLabel.Text = "Oh, shit";
        }
    }
}