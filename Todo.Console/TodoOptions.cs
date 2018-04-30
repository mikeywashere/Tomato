using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Command
{
    [Verb("add", HelpText = "Add a todo item")]
    public class AddOptions
    {
        [Option('t', "text", Required = true, HelpText = "Text of the item to add")]
        public string Text { get; set; }
    }

    [Verb("list", HelpText = "Show the items.")]
    public class ListOptions
    {
        [Option('c', "count", Default = 10, HelpText = "How many items to show")]
        public int Count { get; set; }

        [Option('a', "all", HelpText = "List all of the items.")]
        public bool All { get; set; }
    }

    [Verb("remove", HelpText = "Remove a todo item.")]
    public class RemoveOptions
    {
        [Option('t', "text", Required = true, HelpText = "Text of the item to add")]
        public string Text { get; set; }
    }

    [Verb("move", HelpText = "Move a todo item.")]
    public class MoveOptions
    {
        [Option('f', "from", Required = true, HelpText = "Text of the item to move")]
        public string FromText { get; set; }

        [Option('t', "to", Required = true, HelpText = "Text of the item to move above")]
        public string ToText { get; set; }
    }
}
