using System.Threading.Tasks;

namespace api_help_desk.Interfaces
{
    public interface ListInterface
    {
        Task<object> Get(string user_id, string project_id);
        Task<object> Post();
        Task<object> Put();
        Task<object> Delete();
    }
}