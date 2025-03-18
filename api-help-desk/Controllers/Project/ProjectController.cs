using api_help_desk.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace api_help_desk.Controllers.Project
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectInterface IMethod;
        public ProjectController(ProjectInterface IMethod)
        {
            this.IMethod = IMethod;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string user_id)
        {
            try
            {
                var list = await IMethod.Get(user_id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Post()
        {
            try
            {
                var list = await IMethod.Post();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Put")]
        public async Task<IActionResult> Put()
        {
            try
            {
                var list = await IMethod.Put();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete()
        {
            try
            {
                var list = await IMethod.Delete();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}