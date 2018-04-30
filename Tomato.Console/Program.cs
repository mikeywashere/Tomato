using System;
using Tomato.Interface;
using Tomato.Core;
using Tomato.Repository;

namespace Tomato.CommandLine
{
    /// <summary>
    /// Tomato console app
    /// </summary>
    class Program
    {
        static IPropertyRepository propertyStore = new PropertyRepository();
        static int previousPercentage = -1;

        static void Main(string[] args)
        {
            var pom = new Pomodoro(propertyStore);
            pom.Progress += Pom_Progress;
            Console.WriteLine("Starting Pomodoro Timer");
            pom.Run();
        }

        private static void Pom_Progress(object sender, PercentageProgressArgs e)
        {
            var waitTimeStep = sender as WaitTimeStep;
            var Name = propertyStore.Get<string>(waitTimeStep.Id, "Name");
            var percentage = e.PercentageComplete;
            if (percentage != previousPercentage)
            {
                previousPercentage = percentage;
                Console.WriteLine($"{Name}: {percentage}: {waitTimeStep.MinutesRemaining()}");
            }
        }
    }
}
