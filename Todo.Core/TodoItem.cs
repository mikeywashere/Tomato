using System.Diagnostics;

namespace Todo.Core
{
    /// <summary>
    /// TodoItem
    /// A simple data container
    /// </summary>
    [DebuggerDisplay("{Description}")]
    public class TodoItem : ITodoItem
    {
        public TodoItem(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}