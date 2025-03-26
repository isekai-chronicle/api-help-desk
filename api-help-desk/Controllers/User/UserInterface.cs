using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static api_help_desk.Controllers.User.UserModel;

namespace api_help_desk.Controllers.User
{
    public interface UserInterface
    {
        Task<List<UserListOut>> Get();
        Task<List<UserComboOut>> GetCombo();
        Task<UserDataOut> Post(UserDataIn data);
        Task<UserDataOut> Put(UserDataIn data);
        Task<IActionResult> PutPassword(UserDataInPassword data);
        Task<IActionResult> Delete(UserDataIdIn data);
    }
}