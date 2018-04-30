using System;
using System.Collections.Generic;
using System.Text;

namespace Tomato
{
    public struct PercentageProgressArgs
    {
        public PercentageProgressArgs(int percentageComplete)
        {
            PercentageComplete = percentageComplete;
        }

        public int PercentageComplete { get; set; }
    }
}
