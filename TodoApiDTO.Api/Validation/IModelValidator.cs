using System.Threading.Tasks;

namespace TodoApiDTO.Api.Validation
{
    public interface IModelValidator<in TModel>
    {
        Task<ValidationResult> CheckAsync(TModel model);
    }

}