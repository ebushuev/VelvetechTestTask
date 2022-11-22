namespace TodoApi.Domain.Models
{
    public interface IDomainModel<TId> 
    { 
        TId Id { get; set; }
    }
}
