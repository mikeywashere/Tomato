using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Todo.Core
{
    class TodoItemSortedList : IList<TodoItem>
    {
        private SortedList<int, TodoItem> sortedList = new SortedList<int, TodoItem>();

        public TodoItem this[int index] { get => sortedList[index]; set => sortedList[index] = value; }

        public int Count => sortedList.Count;

        public bool IsReadOnly => false;

        public void Add(TodoItem item)
        {
            var max = sortedList.Any() ? sortedList.Keys.Max() : 0;
            sortedList.Add(max + 1, item);
        }

        public void Insert(TodoItem item, int beforeIndex)
        {
            SortedList<int, TodoItem> newSortedList = new SortedList<int, TodoItem>();

            foreach (var sortedItem in sortedList)
            {
                if (sortedItem.Key < beforeIndex)
                {
                    newSortedList.Add(sortedItem.Key, sortedItem.Value);
                    continue;
                }
                if (sortedItem.Key == beforeIndex)
                {
                    newSortedList.Add(beforeIndex, item);
                }
                if (sortedItem.Key >= beforeIndex)
                {
                    newSortedList.Add(sortedItem.Key + 1, sortedItem.Value);
                }

            }
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
            throw new NotImplementedException();
        }

        public void Insert(int index, TodoItem item)
        {
            SortedList<int, TodoItem> newSortedList = new SortedList<int, TodoItem>();

            foreach (var sortedItem in sortedList)
            {
                if (sortedItem.Key < beforeIndex)
                {
                    newSortedList.Add(sortedItem.Key, sortedItem.Value);
                    continue;
                }
                if (sortedItem.Key == beforeIndex)
                {
                    newSortedList.Add(beforeIndex, item);
                }
                if (sortedItem.Key >= beforeIndex)
                {
                    newSortedList.Add(sortedItem.Key + 1, sortedItem.Value);
                }
            }

            sortedList = newSortedList;
        }

        public bool Remove(TodoItem item)
        {
            var keys = from sortedItem in sortedList
                      where sortedItem.Value == item
                      select sortedItem.Key;
            var key = keys?.FirstOrDefault();
            if (key != null && key.HasValue)
                RemoveAt(key.Value);
            }
        }

        public void RemoveAt(int index)
        {
            sortedList.Remove(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
