using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_help_desk.Controllers.Security.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationInterface IMethod;

        public AuthenticationController(AuthenticationInterface IMethod) => this.IMethod = IMethod;

        [AllowAnonymous]
        [HttpPost("PostToken")]
        public async Task<ActionResult<List<Config>>> PostToken([FromBody] User user)
        {
            var result = await IMethod.PostToken(user);

            return result;
        }

        [AllowAnonymous]
        [HttpPost("PostLogOut")]
        public async Task<ActionResult> PostLogOut([FromBody] UserId user)
        {
            await IMethod.PostLogOut(user.user_id);
            return Ok();
        }

        [HttpGet("GetMenu")]
        public async Task<IActionResult> GetMenu(Guid user)
        {
            try
            {
                var list = await IMethod.GetMenu(user);
                return Ok(list);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("PostMenu")]
        public async Task<ActionResult> PostMenu([FromBody] UserAccess menu)
        {
            await IMethod.PostMenu(menu);
            return Ok();
        }
    }
}
