using System;
using System.Linq;
using System.Collections.Generic;

namespace Tomato
{
    public struct ProcessProgressArgs
    {
        public int PercentageComplete { get; set; }
    }

    public interface IProcessStep
    {
        event EventHandler<ProcessProgressArgs> Progress;

        void Run();

        void Cancel();
    }

    public interface IProcess
    {
        bool RotateToStartWhenDone { get; set; }

        bool OnLastStep();

        IProcessStep Current();

        void AddStep(IProcessStep processStep);

        void Start();

        void Step();
    }

    public class Process : IProcess
    {
        private List<IProcessStep> steps = new List<IProcessStep>();

        public bool RotateToStartWhenDone { get; set; }

        private IProcessStep current = null;

        public bool OnLastStep()
        {
            return steps.Last() == current;
        }

        public void AddStep(IProcessStep processStep)
        {
            if (processStep == null)
                throw new ArgumentNullException(nameof(processStep));

            steps.Add(processStep);
        }

        public IProcessStep Current()
        {
            return current;
        }

        public void Step()
        {
            var index = steps.FindIndex((test) => current == test);
            if (index == steps.Count - 1)
            {
                if (RotateToStartWhenDone)
                {

                }
            }
            else
            {
                current = steps[++index];
            }
        }

        public void Start()
        {
            current = steps.First();
        }
    }
}
