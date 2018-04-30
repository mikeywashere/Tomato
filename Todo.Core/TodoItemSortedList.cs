using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Todo.Core
{
    /// <summary>
    /// TodoItemSortedList
    /// Keeps TodoItems in simple order
    /// Allows for reordering
    /// </summary>
    public class TodoItemSortedList : IList<TodoItem>
    {
        private SortedList<int, TodoItem> sortedList = new SortedList<int, TodoItem>();

        public bool IsReadOnly => false;
        public int Count => sortedList.Count;

        public TodoItem this[int index] { get => sortedList[index + 1]; set => sortedList[index + 1] = value; }

        public void Add(TodoItem item)
        {
            var max = sortedList.Any() ? sortedList.Keys.Max() : 0;
            sortedList.Add(max + 1, item);
        }

        public void Clear()
        {
            sortedList.Clear();
        }

        public bool Contains(TodoItem item)
        {
            return sortedList.Values.Contains(item);
        }

        public void CopyTo(TodoItem[] array, int arrayIndex)
        {
            sortedList.Values.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TodoItem> GetEnumerator()
        {
            var items = from sortedItem in sortedList
                        orderby sortedItem.Key
                        select sortedItem.Value;
            return items.GetEnumerator();
        }

        public int IndexOf(TodoItem item)
        {
            var keys = from sortedItem in sortedList
                       where sortedItem.Value.Description == item.Description
                       select sortedItem.Key;
            var key = keys?.FirstOrDefault();
            if (key != null && key.HasValue)
            {
                return key.Value;
            }
            return -1;
        }

        public void Insert(TodoItem item, int beforeIndex)
        {
            Renumber(beforeIndex, item);
        }

        public void Insert(TodoItem item, TodoItem before)
        {
            Insert(item, IndexOf(before));
        }

        public void Insert(int index, TodoItem item)
        {
            Renumber(index, item);
        }

        public void Move(TodoItem item, TodoItem before)
        {
            Remove(item);
            Insert(item, IndexOf(before));
        }

        public bool Remove(TodoItem item)
        {
            var key = IndexOf(item);
            if (key != -1 && sortedList.ContainsKey(key))
            {
                RemoveAt(key);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            sortedList.Remove(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var items = from sortedItem in sortedList
                        orderby sortedItem.Key
                        select sortedItem.Value;
            return items.GetEnumerator();
        }

        private void Renumber()
        {
            SortedList<int, TodoItem> newSortedList = new SortedList<int, TodoItem>();

            var index = 1;
            foreach (var sortedItem in sortedList)
            {
                newSortedList.Add(index++, sortedItem.Value);
            }

            sortedList = newSortedList;
        }

        private void Renumber(int insertAt, TodoItem todoItem)
        {
            SortedList<int, TodoItem> newSortedList = new SortedList<int, TodoItem>();

            var index = 1;
            foreach (var sortedItem in sortedList)
            {
                if (index == insertAt)
                {
                    newSortedList.Add(index++, todoItem);
                }
                newSortedList.Add(index++, sortedItem.Value);
            }

            sortedList = newSortedList;
        }
    }
}