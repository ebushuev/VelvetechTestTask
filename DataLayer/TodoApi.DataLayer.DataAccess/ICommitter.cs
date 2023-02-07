using System.Threading.Tasks;

namespace TodoApi.DataLayer.DataAccess
{
    public interface ICommitter
    {
        Task Commit();
    }
}