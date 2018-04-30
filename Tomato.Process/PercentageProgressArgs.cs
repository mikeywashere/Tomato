using System;
using System.Collections.Generic;
using System.Text;

namespace Tomato
{
    /// <summary>
    /// PercentageProgressArgs is a simple data container
    /// </summary>
    public struct PercentageProgressArgs
    {
        public PercentageProgressArgs(int percentageComplete)
        {
            PercentageComplete = percentageComplete;
        }

        public int PercentageComplete { get; set; }
    }
}
