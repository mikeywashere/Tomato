using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Todo.Core;

namespace Todo.Core.Test
{
    [TestClass]
    public class TodoCoreTests
    {
        [TestMethod]
        public void TodoList_Add_one_item_Count_is_one()
        {
            var list = new TodoItemSortedList();
            list.Add(new TodoItem("Test 1"));
            Assert.AreEqual(list.Count, 1);
        }

        [TestMethod]
        public void TodoList_Add_two_items_Move_second_item_before_first()
        {
            var list = new TodoItemSortedList();
            var todoOne = new TodoItem("Test 1");
            var todoTwo = new TodoItem("Test 2");
            list.Add(todoOne);
            list.Add(todoTwo);
            list.Move(todoTwo, todoOne);
            Assert.AreEqual(list.First(), todoTwo);
            Assert.AreEqual(list.Last(), todoOne);
        }

        [TestMethod]
        public void TodoList_Add_one_item_Insert_second_item_before_first()
        {
            var list = new TodoItemSortedList();
            var todoOne = new TodoItem("Test 1");
            var todoTwo = new TodoItem("Test 2");
            list.Add(todoOne);
            list.Insert(1, todoTwo);
            Assert.AreEqual(list.First(), todoTwo);
            Assert.AreEqual(list.Last(), todoOne);
        }

        [TestMethod]
        public void TodoList_Add_ten_items_Move_last_item_to_first_one_step_at_a_time()
        {
            var list = new TodoItemSortedList();
            foreach (var index in Enumerable.Range(1, 10))
            {
                var todoItem = new TodoItem($"Test {index}");
                list.Add(todoItem);
            }

            var itemTen = list.First(item => item.Description == $"Test 10");

            foreach (var index in Enumerable.Range(1, 9).OrderByDescending(i => i))
            {
                var todoItem = list.First(item => item.Description == $"Test {index}");
                list.Move(itemTen, todoItem);
            }

            var itemNine = list.First(item => item.Description == $"Test 9");

            Assert.AreEqual(list.First(), itemTen);
            Assert.AreEqual(list.Last(), itemNine);
        }
    }
}
