
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static api_help_desk.Controllers.Dictionary.DictionaryModel;

namespace api_help_desk.Controllers.Dictionary
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DictionaryController : ControllerBase
    {
        private readonly DictionaryInterface IMethod;
        public DictionaryController(DictionaryInterface IMethod)
        {
            this.IMethod = IMethod;
        }

        [HttpPost("PostById")]
        public async Task<IActionResult> PostById(DictionaryListIn data)
        {
            try
            {
                var list = await IMethod.PostById(data);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("PostComponentById")]
        public async Task<IActionResult> PostComponentById(DictionaryTraductorIn data)
        {
            try
            {
                var list = await IMethod.PostComponentById(data);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Post(DictionaryDataIn data)
        {
            try
            {
                var list = await IMethod.Post(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Put")]
        public async Task<IActionResult> Put(List<DictionaryDataWordIn> data)
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
        public async Task<IActionResult> Delete(DictionaryDataIdIn data)
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