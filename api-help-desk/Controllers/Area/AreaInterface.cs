using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static api_help_desk.Controllers.Area.AreaModel;

namespace api_help_desk.Controllers.Area
{
    public interface AreaInterface
    {
        Task<List<AreaListOut>> Get();
        Task<List<AreaComboOut>> GetCombo();
        Task<AreaDataOut> Post(AreaDataIn data);
        Task<AreaDataOut> Put(AreaDataIn data);
        Task<IActionResult> Delete(AreaDataIdIn data);
    }
}