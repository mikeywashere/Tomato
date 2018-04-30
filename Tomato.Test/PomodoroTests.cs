using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Tomato.Test
{
    [TestClass]
    public class PomodoroTests
    {
        private int EventFiredCounter = 0;

        [TestMethod]
        public void Pom()
        {
            //var waitTimeStep = new WaitTimeStep(TimeSpan.FromSeconds(6));
            //EventFiredCounter = 0;
            //waitTimeStep.Progress += WaitTimeStep_Progress;
            //waitTimeStep.Run();
            //Assert.IsTrue(EventFiredCounter < 5, "Less than 5 events were fired");
        }
    }
}

