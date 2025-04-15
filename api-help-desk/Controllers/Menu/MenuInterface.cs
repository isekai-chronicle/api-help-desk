using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static api_help_desk.Controllers.Menu.MenuModel;

namespace api_help_desk.Controllers.Menu
{
    public interface MenuInterface
    {
        Task<List<MenuListOut>> Get(Guid user_id);
        Task<MenuListComponentOut> GetMenuComponent(Guid menuData_id);
        Task<List<MenuComboOut>> GetCombo();
        Task<List<MenuDataComboOut>> GetComboMenuData(Guid menu_id);
        Task<MenuDataOut> PostMenu(MenuDataIn data);
        Task<MenuDataDataOut> PostMenuData(MenuDataDataIn data);
        Task<MenuDataOut> PutMenu(MenuDataIn data);
        Task<MenuDataDataOut> PutMenuData(MenuDataDataIn data);
        Task<IActionResult> PutMenuDataComponent(MenuDataComponentDataIn data);
        Task<IActionResult> Delete(MenuDataIdIn data);
        Task<IActionResult> DeleteData(MenuDataDataIdIn data);
    }
}