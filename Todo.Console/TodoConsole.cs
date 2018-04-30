using System;
using System.IO;
using Todo.Core;
using Todo.Extensions;

namespace Todo.Command
{
    /// <summary>
    /// All required logic for the todo console app
    /// Injectable Action&lt;string&gt; to allow easier testing
    /// </summary>
    /// <TODO>Write tests</TODO>
    public class TodoConsole
    {
        const string filename = "data.list";
        private TodoItemSortedList _list = new TodoItemSortedList();
        private Action<string> _textWriter;

        public TodoConsole(Action<string> textWriter)
        {
            _textWriter = textWriter;

            if (File.Exists(filename))
                _list.LoadFromFile(filename);
        }

        private void Save()
        {
            _list.SaveToFile(filename);
        }
        
        public int Add(AddOptions options)
        {
            TodoItem todoItem = new TodoItem(options.Text);
            _list.Add(todoItem);
            Save();

            _textWriter.Invoke($"Item \"{todoItem.Description}\" added. Item count = {_list.Count}");

            return 0;
        }

        public int List(ListOptions options)
        {
            int count = 0;
            foreach (var item in _list)
            {
                _textWriter.Invoke($"{item.Description}");
                if (!options.All && ++count >= options.Count)
                    break;
            }

            return 0;
        }

        public int Remove(RemoveOptions options)
        {
            TodoItem todoItem = new TodoItem(options.Text);
            if (_list.Remove(todoItem))
            {
                _textWriter.Invoke($"\"{options.Text}\" removed");
            }
            else
            {
                _textWriter.Invoke($"\"{options.Text}\" not found");
            }
            Save();

            return 0;
        }

        public int Move(MoveOptions options)
        {
            TodoItem fromTodoItem = new TodoItem(options.FromText);
            TodoItem toTodoItem = new TodoItem(options.ToText);
            _list.Move(fromTodoItem, toTodoItem);
            _textWriter.Invoke($"\"{options.FromText}\" moved to \"{options.ToText}\".");
            Save();

            return 0;
        }
    }
}
