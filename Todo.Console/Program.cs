using CommandLine;
using System;

namespace Todo.Command
{
    /// <summary>
    /// Todo console app
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var returnCode = CommandLine.Parser.Default.ParseArguments<AddOptions, ListOptions, RemoveOptions, MoveOptions>(args)
                .MapResult(
                    (AddOptions opts) => RunAdd(opts),
                    (ListOptions opts) => RunList(opts),
                    (RemoveOptions opts) => RunRemove(opts),
                    (MoveOptions opts) => RunMove(opts),
                errs => 1);

            Environment.Exit(returnCode);
        }

        private static void WriteToConsole(string message)
        {
            Console.WriteLine(message);
        }

        private static int RunAdd(AddOptions options)
        {
            TodoConsole console = new TodoConsole(WriteToConsole);
            return console.Add(options);
        }

        private static int RunList(ListOptions options)
        {
            TodoConsole console = new TodoConsole(WriteToConsole);
            return console.List(options);
        }

        private static int RunRemove(RemoveOptions options)
        {
            TodoConsole console = new TodoConsole(WriteToConsole);
            return console.Remove(options);
        }

        private static int RunMove(MoveOptions options)
        {
            TodoConsole console = new TodoConsole(WriteToConsole);
            return console.Move(options);
        }
    }
}