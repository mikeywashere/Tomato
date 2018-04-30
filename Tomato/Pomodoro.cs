using System;
using System.Linq;
using Tomato.Interface;
using Tomato.Repository;

namespace Tomato.Core
{
    /// <summary>
    /// Pomodoro class
    /// A pomodoro is basically a workflow of timers
    /// That is the strategy this class uses
    /// </summary>
    public class Pomodoro : IPomodoro
    {
        private readonly Process _pomodoroProcess = new Process();
        private readonly IPropertyRepository _properties;

        public Pomodoro(IPropertyRepository properties)
        {
            _properties = properties;

            var settings = new Settings();
            foreach (var index in Enumerable.Range(1, 4))
            {
                var waitTimeStep = new WaitTimeStep(settings.GetPomodoroTime);
                waitTimeStep.Progress += ProgressHandler;
                _properties.Put(waitTimeStep.Id, "Name", $"Work Time {index}");
                _pomodoroProcess.AddStep(waitTimeStep);

                var restTimeStep = new WaitTimeStep(index == 4 ? settings.GetLongWaitTime : settings.GetSmallWaitTime);
                restTimeStep.Progress += ProgressHandler;
                _properties.Put(restTimeStep.Id, "Name", $"Rest Break {index}");
                _pomodoroProcess.AddStep(restTimeStep);
            }
            _pomodoroProcess.RotateToStartWhenDone = false;
            _pomodoroProcess.StartNextStepAutomatically = true;
        }

        /// <summary>
        /// Progress Event
        /// </summary>
        public event EventHandler<PercentageProgressArgs> Progress;

        public IPomodoroProgress Current()
        {
            if (!(_pomodoroProcess.Current() is WaitTimeStep current))
                return null;

            var pomodoroProgress = new PomodoroProgress()
            {
                Name = _properties.Get<string>(current.Id, "Name"),
                Percentage = current.Percentage()
            };

            return pomodoroProgress;
        }

        public void Run()
        {
            _pomodoroProcess.Start();
        }

        private void ProgressHandler(object sender, PercentageProgressArgs e)
        {
            Progress?.Invoke(sender, e);
        }
    }
}