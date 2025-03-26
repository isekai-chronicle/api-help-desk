using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static api_help_desk.Controllers.Paths.PathsModel;

namespace api_help_desk.Controllers.Paths
{
    public interface PathsInterface
    {
        Task<List<PathsListOut>> Get();
        Task<List<PathsComboOut>> GetCombo();
        Task<PathsDataOut> Post(PathsDataIn data);
        Task<PathsDataOut> Put(PathsDataIn data);
        Task<IActionResult> Delete(PathsDataIdIn data);
    }
}