using System;
using System.IO;
using System.Collections.Generic;
using Todo.Core;
using Todo.Extensions;
using CommandLine;
using CommandLine.Text;

namespace Todo.Command
{
    class Program
    {
        class Options
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

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
                .WithNotParsed<Options>((errs) => HandleParseError(errs));

            Console.ReadKey();
        }

        static int RunOptionsAndReturnExitCode(Options options)
        {
            var list = new TodoItemSortedList();
            if (File.Exists("data.list"))
                list.LoadFromFile("data.list");
            if (options.AddCommand)
            {
                if (string.IsNullOrEmpty(options.Text))
                {
                    Console.WriteLine("Gotta have some text to add!");
                    return -1;
                }
                TodoItem todoItem = new TodoItem(options.Text);
                list.Add(todoItem);
                list.SaveToFile("data.list");
            }

            return 0;
        }

        static int HandleParseError(IEnumerable<Error> errors)
        {
            return -1;
        }
    }
}
