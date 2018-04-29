using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tomato.Test
{
    [TestClass]
    public class ProcessTests
    {
        public class DummyProcessStep : IProcessStep
        {
            public event EventHandler<ProcessProgressArgs> Progress;

            public void Cancel()
            {
                throw new NotImplementedException();
            }

            public void Run()
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_AddStep_Null()
        {
            var process = new Process();
            process.AddStep(null);
        }

        [TestMethod]
        public void Test_AddStep()
        {
            var process = new Process();
            process.AddStep(new DummyProcessStep());
        }

        [TestMethod]
        public void Test_AddStep_then_Start_then_OnLastStep()
        {
            var process = new Process();
            process.AddStep(new DummyProcessStep());
            process.Start();
            Assert.IsTrue(process.OnLastStep());
        }

        [TestMethod]
        public void Test_AddStep_then_Current_is_null_before_Start()
        {
            var process = new Process();
            process.AddStep(new DummyProcessStep());
            Assert.IsNull(process.Current());
        }

        [TestMethod]
        public void Test_AddStep_twice_then_Start_then_OnLastStep()
        {
            var process = new Process();
            process.AddStep(new DummyProcessStep());
            process.AddStep(new DummyProcessStep());
            process.Start();
            Assert.IsFalse(process.OnLastStep());
        }

        [TestMethod]
        public void Test_AddStep_then_Start_then_Current()
        {
            var dummyOne = new DummyProcessStep();
            var process = new Process();
            process.AddStep(dummyOne);
            process.Start();
            var current = process.Current();
            Assert.AreEqual(current, dummyOne);
        }

        [TestMethod]
        public void Test_AddStep_Twice_then_Start_then_Step_Twice()
        {
            var dummyOne = new DummyProcessStep();
            var process = new Process();
            process.AddStep(dummyOne);
            process.Start();
            var current = process.Current();
            Assert.AreEqual(current, dummyOne);
        }
    }
}
