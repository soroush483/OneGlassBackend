using Microsoft.AspNetCore.Mvc;
using OneGlassApi.DTO;
using OneGlassApi.Interfaces;

namespace OneGlassApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                var result = _loginService.GenearteJwtToken(loginDTO.UserName, loginDTO.Password);
                return Ok(result);
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}
