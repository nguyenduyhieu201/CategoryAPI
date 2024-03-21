using CategoryAPI.Model;
using CategoryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;
        private IAuthenService _authenService;
        public AuthController (IAuthenService authenService)
        {
            _authenService = authenService;
        }

        [HttpPost()]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            //if ((!loginModel.username.Equals(username)) || (!loginModel.password.Equals(password)))
            //{
            //    return BadRequest(new {message = "Password or Username is not correct" });
            //}
            //return Ok(LoginResponseModel);
            var response = _authenService.Authenticate(loginModel);
            if (!response.Success) {
                return BadRequest(new { message = "Password or Username is not correct" });
            }
            return Ok(response);
        }
    }
}
