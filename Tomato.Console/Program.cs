using System;

namespace Tomato.CommandLine
{
    class Program
    {
        static IPropertyStore propertyStore = new PropertyStore();
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
            var Name = propertyStore.Get<string>(sender, "Name");
            var percentage = e.PercentageComplete;
            if (percentage != previousPercentage)
            {
                previousPercentage = percentage;
                Console.WriteLine($"{Name}: {percentage}: {waitTimeStep.MinutesRemaining()}");
            }
        }
    }
}
