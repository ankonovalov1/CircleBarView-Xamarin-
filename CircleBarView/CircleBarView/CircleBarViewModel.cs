using Android.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CircleBarView
{
    public class CircleBarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private float progress;
        private Color backColor;
        private Color frontColor;
        private bool timerIsRunning;
        private float time;
        private string timeLeft;
        public float Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public Color BackColor
        {
            get { return backColor; }
            set
            {
                backColor = value;
                OnPropertyChanged("BackColor");
            }
        }

        public Color FrontColor
        {
            get { return frontColor; }
            set
            {
                frontColor = value;
                OnPropertyChanged("FrontColor");
            }
        }

        public bool TimerIsRunning
        {
            get { return timerIsRunning; }
            set
            {
                timerIsRunning = value;
                OnPropertyChanged("TimerIsRunning");
            }
        }
        public float Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }
        public string TimeLeft
        {
            get { return timeLeft; }
            set
            {
                timeLeft = value;
                OnPropertyChanged("TimeLeft");
            }
        }




        public void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
