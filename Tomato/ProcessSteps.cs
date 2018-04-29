using System;
using System.Threading;

namespace Tomato.ProcessSteps
{
    public class WaitTimeStep : IProcessStep
    {
        public TimeSpan WaitTime { get; set; }

        public event EventHandler<ProcessProgressArgs> Progress;

        private Timer ticker;
        private DateTime whenStarted;
        private bool Complete;

        public void TimerMethod(object state)
        {
            if (DateTime.Now.Subtract(WaitTime) > whenStarted)
                Complete = true;
            Progress?.Invoke(this, new ProcessProgressArgs());
        }

        public void Run()
        {
            Complete = false;
            whenStarted = DateTime.Now;
            using (ticker = new Timer(TimerMethod, null, WaitTime, TimeSpan.FromSeconds(1)))
                while (!Complete)
                    Thread.Sleep(500);
        }

        public void Cancel()
        {
            Complete = true;
        }
    }
}
