using System;

namespace Tomato.Interface
{
    public interface IPomodoro
    {
        event EventHandler<PercentageProgressArgs> Progress;

        IPomodoroProgress Current();

        void Run();
    }
}