using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tomato;
using System;

namespace Tomato.Test
{
    [TestClass]
    public class ProcessTests
    {
        public class TestIProcessStep : IProcessStep
        {
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
            process.AddStep(new TestIProcessStep());
        }
    }
}
