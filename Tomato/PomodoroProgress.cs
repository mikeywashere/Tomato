using System;
using System.Collections.Generic;
using System.Text;

namespace Tomato.Core
{
    /// <summary>
    /// PomodoroProgress is a simple data container
    /// </summary>
    public class PomodoroProgress : IPomodoroProgress
    {
        public string Name { get; set; }
        public int Percentage { get; set; }
    }
}
