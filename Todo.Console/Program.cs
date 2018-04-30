using CommandLine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Todo.Core;
using Todo.Extensions;

namespace Todo.Command
{
    /// <summary>
    /// Todo console app
    /// </summary>
    internal class Program
    {
        private static int HandleParseError(IEnumerable<Error> errors)
        {
            return -1;
        }

        private static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
                .WithNotParsed<Options>((errs) => HandleParseError(errs));

            Console.ReadKey();
        }

        const string filename = "data.list";

        private static int RunOptionsAndReturnExitCode(Options options)
        {
            var list = new TodoItemSortedList();
            if (File.Exists(filename))
                list.LoadFromFile(filename);

            if (options.AddCommand)
            {
                if (string.IsNullOrEmpty(options.Text))
                {
                    Console.WriteLine("Gotta have some text to add!");
                    return -1;
                }
                TodoItem todoItem = new TodoItem(options.Text);
                list.Add(todoItem);
                list.SaveToFile(filename);
            }

            if (options.FirstCommand)
            {
                Console.WriteLine(list.FirstOrDefault());
            }

            if (options.RemoveCommand)
            {
                if (string.IsNullOrEmpty(options.Text))
                {
                    Console.WriteLine("Gotta have some text to remove!");
                    return -1;
                }
                TodoItem todoItem = new TodoItem(options.Text);
                list.Remove(todoItem);
                list.SaveToFile(filename);
            }

            return 0;
        }

        private class Options
        {
            [Option('a', "add", HelpText = "Add an item")]
            public bool AddCommand { get; set; }

            [Option('f', "first", HelpText = "Show the first item")]
            public bool FirstCommand { get; set; }

            [Option('r', "remove", HelpText = "Remove an item")]
            public bool RemoveCommand { get; set; }

            [Option('t', "text", HelpText = "Text of the item")]
            public string Text { get; set; }
        }
    }
}