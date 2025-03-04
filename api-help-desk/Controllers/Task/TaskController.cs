using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_help_desk.Controllers.Task
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskInterface IMethod;
        public TaskController(TaskInterface IMethod)
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
