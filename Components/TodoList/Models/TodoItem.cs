namespace TodoApiDTO.Components.TodoList.Models
{
    using TodoApiDTO.Interfaces;

    /// <summary>
    /// Сущность TO-DO.
    /// </summary>
    public class TodoItem : IHaveId
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Признак выполнения.
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Секрет.
        /// </summary>
        public string Secret { get; set; }
    }
}