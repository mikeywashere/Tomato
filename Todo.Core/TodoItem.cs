using System.Diagnostics;

namespace Todo.Core
{
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
