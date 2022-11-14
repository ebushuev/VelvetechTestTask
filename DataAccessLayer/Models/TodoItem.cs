namespace TodoApiDTO.DataAccessLayer.Models
{
    #region snippet
    /// <summary>
    /// Задача 
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Завершена или нет
        /// </summary>
        public bool IsComplete { get; set; }
        /// <summary>
        /// Секрет
        /// </summary>
        public string Secret { get; set; }
    }
    #endregion
}