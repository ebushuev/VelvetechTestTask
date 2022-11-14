namespace TodoApiDTO.BuisnessLayer.Models
{
    #region snippet

    /// <summary>
    /// Бизнес модель задач
    /// </summary>
    public class TodoItemDTO
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
    }
    #endregion
}
