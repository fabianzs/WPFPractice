#region copyright

// Copyright 2007 - 2021 Innoveo AG, Zurich/Switzerland
// All rights reserved. Use is subject to license terms.

#endregion

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataContextExamples.Models
{
    public class DataBaseClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}