namespace TodoApiDTO.Interfaces
{
    /// <summary>
    /// Интерфейс сущности с идентификатором.
    /// </summary>
    public interface IHaveId
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public long Id { get; set; }
    }
}