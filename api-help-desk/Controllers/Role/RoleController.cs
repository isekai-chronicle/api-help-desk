
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static api_help_desk.Controllers.Role.RoleModel;

namespace api_help_desk.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleInterface IMethod;
        public RoleController(RoleInterface IMethod)
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


        [HttpPost("Post")]
        public async Task<IActionResult> Post(RoleDataIn data)
        {
            try
            {
                var list = await IMethod.Post(data);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Put")]
        public async Task<IActionResult> Put(RoleDataIn data)
        {
            try
            {
                var list = await IMethod.Put(data);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(RoleDataIdIn data)
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
    }
}