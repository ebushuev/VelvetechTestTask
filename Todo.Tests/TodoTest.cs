using Todo.Domain.Models;
using Xunit;

namespace Todo.Tests
{
    public class TodoTest
    {
        [Fact]
        public void UpdateName()
        {
            var id = 1;
            var name = "test";
            var isComplete = false;

            var model = new TodoItemModel(id, name, isComplete);

            var newName = "updatedName";
            model.UpdateName(newName);

            Assert.Equal(newName, model.Name);
        }

        [Fact]
        public void SetComplete()
        {
            var id = 1;
            var name = "test";
            var isComplete = false;

            var model = new TodoItemModel(id, name, isComplete);

            model.Complete();

            Assert.True(model.IsComplete);
        }

        [Fact]
        public void SetUncomplete()
        {
            var id = 1;
            var name = "test";
            var isComplete = true;

            var model = new TodoItemModel(id, name, isComplete);

            model.UnComplete();

            Assert.False(model.IsComplete);
        }
    }
}
