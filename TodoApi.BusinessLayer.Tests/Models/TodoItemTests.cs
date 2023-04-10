using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TodoApi.BusinessLayer.Models.Tests
{
    [TestClass()]
    public class TodoItemTests
    {
        [TestMethod()]
        public void Constructor_ShouldFillGivenValues()
        {
            var name = "Test TodoItem";
            var isComplete = true;

            var todoItem = new TodoItem(name, isComplete);

            Assert.AreEqual(name, todoItem.Name);
            Assert.AreEqual(isComplete, todoItem.IsComplete);
        }

        [TestMethod()]
        public void Constructor_DefaultIsComplete_ShouldBeFalse()
        {
            var name = "Test TodoItem";

            var todoItem = new TodoItem(name);

            Assert.AreEqual(false, todoItem.IsComplete);
        }

        [TestMethod()]
        public void Constructor_EmptyName_ShouldUseDefaultConstant()
        {
            var todoItem = new TodoItem(string.Empty);

            Assert.AreEqual(TodoItem.DefaultName, todoItem.Name);
        }

        [TestMethod()]
        public void Constructor_NullName_ShouldUseDefaultConstant()
        {
            var todoItem = new TodoItem(null);

            Assert.AreEqual(TodoItem.DefaultName, todoItem.Name);
        }

        [TestMethod()]
        public void SetName_NonEmptyName_ShouldReplaceOldValue()
        {
            var name = "Test TodoItem";
            var newName = "New Name";

            var todoItem = new TodoItem(name);
            Assert.AreEqual(name, todoItem.Name);

            todoItem.Name = newName;
            Assert.AreEqual(newName, todoItem.Name);            
        }

        [TestMethod()]
        public void SetName_EmptyName_ShouldRevertToDefaultConstant()
        {
            var name = "Test TodoItem";

            var todoItem = new TodoItem(name);
            Assert.AreEqual(name, todoItem.Name);

            todoItem.Name = string.Empty;
            Assert.AreEqual(TodoItem.DefaultName, todoItem.Name);
        }

        [TestMethod()]
        public void SetName_NullName_ShouldRevertToDefaultConstant()
        {
            var name = "Test TodoItem";

            var todoItem = new TodoItem(name);
            Assert.AreEqual(name, todoItem.Name);

            todoItem.Name = null;
            Assert.AreEqual(TodoItem.DefaultName, todoItem.Name);
        }

        [TestMethod()]
        public void SetName_UsingSameName_ShouldNotChangeValue()
        {
            var name = "Test TodoItem";

            var todoItem = new TodoItem(name);
            Assert.AreEqual(name, todoItem.Name);

            todoItem.Name = name;
            Assert.AreEqual(name, todoItem.Name);
        }

        [DataTestMethod]
        [DataRow(true, false, false)]
        [DataRow(false, true, true)]
        public void SetComplete_AlternateIsComplete_ShouldReplaceValue(bool originalValue, bool replacementValue, bool expected)
        {
            var name = "Test TodoItem";

            var todoItem = new TodoItem(name, originalValue);
            Assert.AreEqual(originalValue, todoItem.IsComplete);

            todoItem.IsComplete = replacementValue;
            Assert.AreEqual(expected, todoItem.IsComplete);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public void SetComplete_SameIsComplete_ShouldNotChangeValue(bool isComplete, bool expected)
        {
            var name = "Test TodoItem";

            var todoItem = new TodoItem(name, isComplete);
            Assert.AreEqual(isComplete, todoItem.IsComplete);

            todoItem.IsComplete = isComplete;
            Assert.AreEqual(expected, todoItem.IsComplete);
        }
    }
}