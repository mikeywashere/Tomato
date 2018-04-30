using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace Tomato.Core
{
    public class WaitTimeStep : IProcessStep
    {
        public WaitTimeStep(TimeSpan waitTime)
        {
            WaitTime = waitTime;
        }

        public TimeSpan WaitTime { get; set; }

        public event EventHandler<PercentageProgressArgs> Progress;

        private Timer ticker;
        private DateTime whenStarted;
        private bool Complete;
        private Guid _id = Guid.NewGuid();

        public Guid Id { get => _id; }

        public void TimerMethod(object state)
        {
            var now = DateTime.Now;
            var nowMinusWaitTime = now.Subtract(WaitTime);
            if (nowMinusWaitTime > whenStarted)
                Complete = true;
            Progress?.Invoke(this, new PercentageProgressArgs(Percentage()));
        }

        public int Percentage()
        {
            var runForSeconds = Math.Abs(DateTime.Now.Subtract(whenStarted).TotalSeconds);
            int percentage = Convert.ToInt32(((float)runForSeconds / (float)WaitTime.TotalSeconds) * 100);
            percentage = Math.Min(percentage, 100);
            return percentage;
        }

        public int MinutesRemaining()
        {
            var now = DateTime.Now;
            var nowMinusStartTime = now.Subtract(whenStarted);
            var minutesRemaining = WaitTime.TotalMinutes - nowMinusStartTime.TotalMinutes;
            return Convert.ToInt32(minutesRemaining);
        }

        public void Run()
        {
            Complete = false;
            whenStarted = DateTime.Now;
            using (ticker = new Timer(TimerMethod, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1)))
                while (!Complete)
                    Thread.Sleep(250);
        }

        public void Cancel()
        {
            Complete = true;
        }
    }
}
