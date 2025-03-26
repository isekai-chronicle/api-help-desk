using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static api_help_desk.Controllers.Role.RoleModel;

namespace api_help_desk.Controllers.Role
{
    public interface RoleInterface
    {
        Task<List<RoleListOut>> Get();
        Task<List<RoleComboOut>> GetCombo();
        Task<RoleDataOut> Post(RoleDataIn data);
        Task<RoleDataOut> Put(RoleDataIn data);
        Task<IActionResult> Delete(RoleDataIdIn data);
    }
}