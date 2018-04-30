﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace Tomato
{
    /// <summary>
    /// A Process is a set of IProcessSteps
    /// It progresses from step to step until done/complete
    /// If RotateToStartWhenDone = true then it automatically wraps around infinitely.
    /// If StartNextStepAutomatically is true then the process is atomatic.
    /// </summary>
    public class Process : IProcess
    {
        private List<IProcessStep> steps = new List<IProcessStep>();

        public bool RotateToStartWhenDone { get; set; }

        public bool StartNextStepAutomatically { get; set; }

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
            if (!OnLastStep())
            {
                var index = steps.FindIndex((test) => current == test);
                current = steps[index + 1];
                return;
            }

            if (RotateToStartWhenDone)
            {
                current = steps.First();
                return;
            }

            current = null;
        }

        public void Start()
        {
            current = steps.First();
            Run();
        }

        public void Run()
        {
            while (current != null)
            {
                current?.Run();
                if (!StartNextStepAutomatically)
                    break;
                Step();
            }
        }

        public void Cancel()
        {
            steps.ForEach(step => step?.Cancel());
            current = null;
        }
    }
}
