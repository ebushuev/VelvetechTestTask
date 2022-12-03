namespace TodoApiDTO.Interfaces
{
    /// <summary>
    /// Интерфейс сущности с идентификатором.
    /// </summary>
    public interface IHaveId
    {
        public long Id { get; set; }
    }
}