using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using Tomato.ProcessSteps;

namespace Tomato.Test
{
    [TestClass]
    public class TomatoTests
    {
        private int EventFiredCounter = 0;

        [TestMethod]
        public void WaitTimeStep_at_least_five_events_fired_in_six_seconds()
        {
            var waitTimeStep = new WaitTimeStep();
            EventFiredCounter = 0;
            waitTimeStep.WaitTime = TimeSpan.FromSeconds(6);
            waitTimeStep.Progress += WaitTimeStep_Progress;
            waitTimeStep.Run();
            Assert.IsTrue(EventFiredCounter < 5, "Less than 5 events were fired");
        }

        [TestMethod]
        public void WaitTimeStep_at_least_five_events_fired_in_six_seconds_and_six_seconds_elapsed()
        {
            var waitTimeStep = new WaitTimeStep();
            EventFiredCounter = 0;
            waitTimeStep.WaitTime = TimeSpan.FromSeconds(6);
            waitTimeStep.Progress += WaitTimeStep_Progress;
            var stopWatch = Stopwatch.StartNew();
            waitTimeStep.Run();
            stopWatch.Stop();
            Assert.IsTrue(stopWatch.ElapsedMilliseconds > 6000);
            Assert.IsTrue(EventFiredCounter < 5, "Less than 5 events were fired");
        }

        [TestMethod]
        public void WaitTimeStep_at_for_five_seconds_cancel_on_first_event()
        {
            var waitTimeStep = new WaitTimeStep();
            EventFiredCounter = 0;
            waitTimeStep.WaitTime = TimeSpan.FromSeconds(6);
            waitTimeStep.Progress += WaitTimeStep_Progress_Cancel;
            waitTimeStep.Run();
            Assert.IsTrue(EventFiredCounter == 1, "One event should be fired");
        }

        private void WaitTimeStep_Progress(object sender, ProcessProgressArgs e)
        {
            EventFiredCounter++;
        }

        private void WaitTimeStep_Progress_Cancel(object sender, ProcessProgressArgs e)
        {
            EventFiredCounter++;
            var waitTimeStep = sender as WaitTimeStep;
            if (waitTimeStep != null)
                waitTimeStep.Cancel();
        }
    }
}
