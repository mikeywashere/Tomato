using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Tomato.Core;

namespace Tomato.Test
{
    [TestClass]
    public class TomatoTests
    {
        private int EventFiredCounter;

        [TestMethod]
        public void WaitTimeStep_between_four_events_and_six_events_fired_in_five_seconds()
        {
            var waitTimeStep = new WaitTimeStep(TimeSpan.FromSeconds(5));
            EventFiredCounter = 0;
            waitTimeStep.Progress += WaitTimeStep_Progress;
            waitTimeStep.Run();
            Assert.IsTrue(EventFiredCounter >= 4 && EventFiredCounter <= 6);
        }

        [TestMethod]
        public void WaitTimeStep_at_least_six_milliseconds_elapsed()
        {
            var waitTimeStep = new WaitTimeStep(TimeSpan.FromSeconds(6));
            EventFiredCounter = 0;
            waitTimeStep.Progress += WaitTimeStep_Progress;
            var stopWatch = Stopwatch.StartNew();
            waitTimeStep.Run();
            stopWatch.Stop();
            Assert.IsTrue(stopWatch.ElapsedMilliseconds > 5999);
        }

        [TestMethod]
        public void WaitTimeStep_for_five_seconds_cancel_on_first_event()
        {
            var waitTimeStep = new WaitTimeStep(TimeSpan.FromSeconds(6));
            EventFiredCounter = 0;
            waitTimeStep.Progress += WaitTimeStep_Progress_Cancel;
            waitTimeStep.Run();
            Assert.IsTrue(EventFiredCounter == 1, "One event should be fired");
        }

        [TestMethod]
        public void WaitTimeStep_for_ten_seconds_each_event_about_ten_percent()
        {
            var waitTimeStep = new WaitTimeStep(TimeSpan.FromSeconds(10));
            EventFiredCounter = 0;
            waitTimeStep.Progress += WaitTimeStep_Progress_Add_To_List;
            waitTimeStep.Run();
            Assert.IsTrue(listOfPercentages.Any(value => value == 10));
            Assert.IsTrue(listOfPercentages.Any(value => value == 20));
            Assert.IsTrue(listOfPercentages.Any(value => value == 30));
            Assert.IsTrue(listOfPercentages.Any(value => value == 40));
            Assert.IsTrue(listOfPercentages.Any(value => value == 50));
            Assert.IsTrue(listOfPercentages.Any(value => value == 60));
            Assert.IsTrue(listOfPercentages.Any(value => value == 70));
            Assert.IsTrue(listOfPercentages.Any(value => value == 80));
            Assert.IsTrue(listOfPercentages.Any(value => value == 90));
            Assert.IsTrue(listOfPercentages.Any(value => value == 100));
        }

        private void WaitTimeStep_Progress(object sender, PercentageProgressArgs e)
        {
            EventFiredCounter++;
        }

        private void WaitTimeStep_Progress_Cancel(object sender, PercentageProgressArgs e)
        {
            EventFiredCounter++;
            if (sender is WaitTimeStep waitTimeStep)
                waitTimeStep.Cancel();
        }

        List<int> listOfPercentages = new List<int>();

        private void WaitTimeStep_Progress_Add_To_List(object sender, PercentageProgressArgs e)
        {
            EventFiredCounter++;
            listOfPercentages.Add(e.PercentageComplete);
        }

    }
}
