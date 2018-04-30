using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Tomato
{
    public class WaitTimeStep : IProcessStep
    {
        public WaitTimeStep(TimeSpan waitTime)
        {
            WaitTime = waitTime;
        }

        public TimeSpan WaitTime { get; set; }

        public event EventHandler<PercentageProgressArgs> Progress;

        private Timer ticker;
        private DateTime whenStarted;
        private bool Complete;

        public void TimerMethod(object state)
        {
            var now = DateTime.Now;
            var nowMinusWaitTime = now.Subtract(WaitTime);
            if (nowMinusWaitTime > whenStarted)
                Complete = true;
            Progress?.Invoke(this, new PercentageProgressArgs(Percentage()));
        }

        public int Percentage()
        {
            var runForSeconds = Math.Abs(DateTime.Now.Subtract(whenStarted).TotalSeconds);
            int percentage = Convert.ToInt32(((float)runForSeconds / (float)WaitTime.TotalSeconds) * 100);
            percentage = Math.Min(percentage, 100);
            return percentage;
        }

        public int MinutesRemaining()
        {
            var now = DateTime.Now;
            var nowMinusStartTime = now.Subtract(whenStarted);
            var minutesRemaining = WaitTime.TotalMinutes - nowMinusStartTime.TotalMinutes;
            return Convert.ToInt32(minutesRemaining);
        }

        public void Run()
        {
            Complete = false;
            whenStarted = DateTime.Now;
            using (ticker = new Timer(TimerMethod, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1)))
                while (!Complete)
                    Thread.Sleep(250);
        }

        public void Cancel()
        {
            Complete = true;
        }
    }

    public class PomodoroProgress
    {
        public string Name { get; set; }
        public int Percentage { get; set; }
    }

    public class PropertyStore : IPropertyStore
    {
        private Dictionary<object, ExpandoObject> propertyStore = new Dictionary<object, ExpandoObject>();

        public T Get<T>(object obj, string key) where T : class
        {
            if (!propertyStore.ContainsKey(obj))
                return null;
            if (propertyStore.TryGetValue(obj, out ExpandoObject expando))
            {
                var where = expando.Where(kvp => kvp.Key == key);
                var first = where?.First();
                return first?.Value as T;
            }
            return null;
        }

        public void Put<T>(object obj, string key, T value) where T : class
        {
            if (!propertyStore.ContainsKey(obj))
            {
                propertyStore.Add(obj, new ExpandoObject());
            }

            if (propertyStore.TryGetValue(obj, out ExpandoObject expando))
            {
                expando.TryAdd(key, value);
            }
        }
    }

    public class Pomodoro
    {
        private Process pomodoroProcess = new Process();
        private IPropertyStore propertyStore;

        public event EventHandler<PercentageProgressArgs> Progress;

        public Pomodoro(IPropertyStore propertyStore)
        {
            this.propertyStore = propertyStore;

            var settings = new Settings();
            foreach (var index in Enumerable.Range(1, 4))
            {
                var waitTimeStep = new WaitTimeStep(settings.GetPomodoroTime);
                waitTimeStep.Progress += ProgressHandler;
                propertyStore.Put(waitTimeStep, "Name", $"Work Time {index}");
                pomodoroProcess.AddStep(waitTimeStep);

                var restTimeStep = new WaitTimeStep(index == 4 ? settings.GetLongWaitTime : settings.GetSmallWaitTime);
                restTimeStep.Progress += ProgressHandler;
                propertyStore.Put(restTimeStep, "Name", $"Rest Break {index}");
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

        public PomodoroProgress Current()
        {
            var pomodoroProgress = new PomodoroProgress();
            var current = pomodoroProcess.Current();
            pomodoroProgress.Name = propertyStore.Get<string>(current, "Name");
            pomodoroProgress.Percentage = (current as WaitTimeStep).Percentage();
            return pomodoroProgress;
        }
    }
}
