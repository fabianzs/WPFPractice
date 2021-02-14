#region copyright

// Copyright 2007 - 2021 Innoveo AG, Zurich/Switzerland
// All rights reserved. Use is subject to license terms.

#endregion

using System;
using System.Timers;

namespace DataContextExamples.Models
{
    public class TankData : DataBaseClass
    {
        private int _dataValue;
        private int _maximum;
        private int _minimum;
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                NotifyPropertyChanged();
            }
        }

        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                NotifyPropertyChanged();
            }
        }

        public int DataValue
        {
            get => _dataValue;
            set
            {
                if (value < Minimum || value > Maximum)
                    return;
                _dataValue = value;
                NotifyPropertyChanged();
            }
        }


        public void Initialize(string name, int min = -100, int max = 100)
        {
            Name = name;
            Minimum = min;
            Maximum = max;

            // Create a timer with a one second interval.
            var aTimer = new Timer(1000);
            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            var rnd = new Random();
            DataValue = rnd.Next(DataValue - 10, DataValue + 10);
        }
    }
}