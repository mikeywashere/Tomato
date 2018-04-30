using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Tomato.Interface;
using Tomato.Repository;

namespace Tomato.Core
{
    public class Pomodoro : IPomodoro
    {
        private Process pomodoroProcess = new Process();
        private IPropertyRepository _properties;

        public event EventHandler<PercentageProgressArgs> Progress;

        public Pomodoro(IPropertyRepository properties)
        {
            _properties = properties;

            var settings = new Settings();
            foreach (var index in Enumerable.Range(1, 4))
            {
                var waitTimeStep = new WaitTimeStep(settings.GetPomodoroTime);
                waitTimeStep.Progress += ProgressHandler;
                _properties.Put(waitTimeStep.Id, "Name", $"Work Time {index}");
                pomodoroProcess.AddStep(waitTimeStep);

                var restTimeStep = new WaitTimeStep(index == 4 ? settings.GetLongWaitTime : settings.GetSmallWaitTime);
                restTimeStep.Progress += ProgressHandler;
                _properties.Put(restTimeStep.Id, "Name", $"Rest Break {index}");
                pomodoroProcess.AddStep(restTimeStep);
            }
            pomodoroProcess.RotateToStartWhenDone = false;
            pomodoroProcess.StartNextStepAutomatically = true;
        }

        private void ProgressHandler(object sender, PercentageProgressArgs e)
        {
            Progress?.Invoke(sender, e);
        }

        public void Run()
        {
            pomodoroProcess.Start();
        }

        public IPomodoroProgress Current()
        {
            var current = pomodoroProcess.Current() as WaitTimeStep;
            if (current == null)
                return null;

            var pomodoroProgress = new PomodoroProgress()
            {
                Name = _properties.Get<string>(current.Id, "Name"),
                Percentage = current.Percentage()
            };

            return pomodoroProgress;
        }
    }
}
