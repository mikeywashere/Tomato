using System;
using System.Collections.Generic;

namespace Tomato
{
    public interface IProcessStep
    {
        void Run();
    }

    public interface IProcess
    {
        bool RotateToStartWhenDone { get; set; }

        bool OnLastStep { get; set; }

        IProcessStep Current();

        void AddStep(IProcessStep processStep);

        void Step();
    }

    public class Process : IProcess
    {
        private List<IProcessStep> steps = new List<IProcessStep>();

        public bool RotateToStartWhenDone { get; set; }

        public bool OnLastStep { get; set; }

        public void AddStep(IProcessStep processStep)
        {
            if (processStep == null)
                throw new ArgumentNullException(nameof(processStep));

            steps.Add(processStep);
        }

        public IProcessStep Current()
        {
            throw new NotImplementedException();
        }

        public void Step()
        {
            throw new NotImplementedException();
        }
    }
}
