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
            var context = BuildLoginContext(HttpContext);
            var result = await IMethod.PostToken(user, context);

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
        public async Task<ActionResult> PostMenu([FromBody] List<UserAccess> menu)
        {
            await IMethod.PostMenu(menu);
            return Ok();
        }
        private static LoginContext BuildLoginContext(HttpContext httpContext)
        {
            var headers = httpContext.Request?.Headers;

            string? remoteIp = httpContext.Connection.RemoteIpAddress?.ToString();
            string? xff = headers != null && headers.ContainsKey("X-Forwarded-For") ? headers["X-Forwarded-For"].ToString() : null;
            if (string.IsNullOrWhiteSpace(xff) && headers != null && headers.ContainsKey("X-Real-IP"))
            {
                xff = headers["X-Real-IP"].ToString();
            }

            return new LoginContext
            {
                Ip = remoteIp,
                ForwardedFor = xff,
                UserAgent = headers != null && headers.ContainsKey("User-Agent") ? headers["User-Agent"].ToString() : null,
                AcceptLanguage = headers != null && headers.ContainsKey("Accept-Language") ? headers["Accept-Language"].ToString() : null,
                Referer = headers != null && headers.ContainsKey("Referer") ? headers["Referer"].ToString() : null,
                Host = httpContext.Request?.Host.Value,
                IsHttps = httpContext.Request?.IsHttps ?? false,
                Port = httpContext.Connection.LocalPort
            };
        }
    }

}
