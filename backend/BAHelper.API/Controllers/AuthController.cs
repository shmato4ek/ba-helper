using BAHelper.BLL.JWT;
using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace BAHelper.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly JwtFactory _jwtFactory;
        public AuthController(AuthService authService, UserService userService, JwtFactory jwtFactory)
        {
            _authService = authService;
            _userService = userService;
            _jwtFactory = jwtFactory;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthUserDTO>> Register(NewUserDTO newUser)
        {
            var createdUser = await _userService.CreateUser(newUser);
            var token = await _authService.GenerateAccessToken(createdUser.Id, createdUser.Name, createdUser.Email);

            var result = new AuthUserDTO
            {
                Token = token,
                User = createdUser,
            };
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthUserDTO>> Login([FromBody] LoginUserDTO userDTO)
        {
            return Ok(await _authService.Authorize(userDTO));
        }

        [HttpGet("auth/me")]
        public async Task<ActionResult> GetUserByToken()
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _userService.GetUserById(userId));
        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            Response.Headers.Remove("x-auth-token");
            return NoContent();
        }
    }
}
