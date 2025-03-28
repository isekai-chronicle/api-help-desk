
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static api_help_desk.Controllers.Menu.MenuModel;

namespace api_help_desk.Controllers.Menu
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuInterface IMethod;
        public MenuController(MenuInterface IMethod)
        {
            this.IMethod = IMethod;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var list = await IMethod.Get();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetMenuComponent")]
        public async Task<IActionResult> GetMenuComponent(Guid menuData_id)
        {
            try
            {
                var list = await IMethod.GetMenuComponent(menuData_id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetCombo")]
        public async Task<IActionResult> GetCombo()
        {
            try
            {
                var list = await IMethod.GetCombo();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetComboMenuData")]
        public async Task<IActionResult> GetComboMenuData(Guid menu_id)
        {
            try
            {
                var list = await IMethod.GetComboMenuData(menu_id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("PostMenu")]
        public async Task<IActionResult> PostMenu(MenuDataIn data)
        {
            try
            {
                var list = await IMethod.PostMenu(data);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("PostMenuData")]
        public async Task<IActionResult> PostMenuData(MenuDataDataIn data)
        {
            try
            {
                var list = await IMethod.PostMenuData(data);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("PutMenu")]
        public async Task<IActionResult> PutMenu(MenuDataIn data)
        {
            try
            {
                var list = await IMethod.PutMenu(data);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("PutMenuData")]
        public async Task<IActionResult> PutMenuData(MenuDataDataIn data)
        {
            try
            {
                var list = await IMethod.PutMenuData(data);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("PutMenuDataComponent")]
        public async Task<IActionResult> PutMenuDataComponent(MenuDataComponentDataIn data)
        {
            try
            {
                await IMethod.PutMenuDataComponent(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(MenuDataIdIn data)
        {
            try
            {
                await IMethod.Delete(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteData")]
        public async Task<IActionResult> DeleteData(MenuDataDataIdIn data)
        {
            try
            {
                await IMethod.DeleteData(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}