using System.Threading.Tasks;

namespace api_help_desk.Controllers.Project
{
    public interface ProjectInterface
    {
        Task<object> Get(string user_id);
        Task<object> Post();
        Task<object> Put();
        Task<object> Delete();
    }
}