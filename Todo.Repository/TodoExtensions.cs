using System.IO;
using Todo.Core;

namespace Todo.Extensions
{
    public static class TodoExtensions
    {
        /// <summary>
        /// Load a TodoItemSortedList from a file
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filename"></param>
        public static void LoadFromFile(this TodoItemSortedList list, string filename)
        {
            string json = File.ReadAllText(filename);
            var value = Newtonsoft.Json.JsonConvert.DeserializeObject<TodoItemSortedList>(json);
            list.Clear();
            foreach (var item in value)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// Save a TodoItemSortedList to a file
        /// </summary>
        /// <param name="list"></param>
        /// <param name="filename"></param>
        public static void SaveToFile(this TodoItemSortedList list, string filename)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            File.WriteAllText(filename, json);
        }
    }
}