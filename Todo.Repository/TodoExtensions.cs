using System.Linq;
using Todo.Core;
using System.IO;

namespace Todo.Extensions
{
    public static class TodoExtensions
    {
        public static void SaveToFile(this TodoItemSortedList list, string filename)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            File.WriteAllText(filename, json);
        }

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
    }
}
