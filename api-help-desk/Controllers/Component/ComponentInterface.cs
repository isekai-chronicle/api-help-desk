using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static api_help_desk.Controllers.Component.ComponentModel;

namespace api_help_desk.Controllers.Component
{
    public interface ComponentInterface
    {
        Task<List<ComponentListOut>> Get();
        Task<List<ComponentComboOut>> GetCombo();
        Task<ComponentDataOut> Post(ComponentDataIn data);
        Task<ComponentDataOut> Put(ComponentDataIn data);
        Task<IActionResult> Delete(ComponentDataIdIn data);
    }
}