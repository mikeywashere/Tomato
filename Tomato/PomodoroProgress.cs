using System;
using System.Collections.Generic;
using System.Text;

namespace Tomato.Core
{
    public class PomodoroProgress : IPomodoroProgress
    {
        public string Name { get; set; }
        public int Percentage { get; set; }
    }
}
