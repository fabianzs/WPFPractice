#region copyright

// Copyright 2007 - 2021 Innoveo AG, Zurich/Switzerland
// All rights reserved. Use is subject to license terms.

#endregion

using DataContextExamples.Models;

namespace DataContextExamples
{
    public class EnvironmentViewModel
    {
        public EnvironmentViewModel(EnvironmentData environmentData)
        {
            EnviroData = environmentData;
        }

        public EnvironmentData EnviroData { get; set; }
    }
}