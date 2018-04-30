using System;
using System.Collections.Generic;
using System.Text;

namespace Tomato
{
    public interface IProcessStep
    {
        Guid Id { get; }

        event EventHandler<PercentageProgressArgs> Progress;

        void Run();

        void Cancel();
    }
}
