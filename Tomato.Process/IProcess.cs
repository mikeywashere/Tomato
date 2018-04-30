using System;
using System.Linq;
using System.Collections.Generic;

namespace Tomato
{

    public interface IProcess
    {
        bool RotateToStartWhenDone { get; set; }

        bool StartNextStepAutomatically { get; set; }

        bool OnLastStep();

        IProcessStep Current();

        void AddStep(IProcessStep processStep);

        void Start();

        void Step();

        void Run();

        void Cancel();
    }
}