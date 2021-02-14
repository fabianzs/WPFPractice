#region copyright

// Copyright 2007 - 2021 Innoveo AG, Zurich/Switzerland
// All rights reserved. Use is subject to license terms.

#endregion

using DataContextExamples.Models;

namespace DataContextExamples
{
    public class TankViewModel
    {
        public TankViewModel(TankData tankData1, TankData tankData2)
        {
            Tank1 = tankData1;
            Tank1.Initialize("Tank1");
            Tank2 = tankData2;
            Tank2.Initialize("Tank2");
        }

        public TankData Tank1 { get; set; }

        public TankData Tank2 { get; set; }
    }
}