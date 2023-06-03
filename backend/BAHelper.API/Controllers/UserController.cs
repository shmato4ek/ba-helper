using BAHelper.BLL.JWT;
using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace BAHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtFactory _jwtFactory;
        public UserController(UserService userService, JwtFactory jwtFactory)
        {
            _userService = userService;
            _jwtFactory = jwtFactory;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser()
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            await _userService.DeleteUser(userId);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDTO updatedUser)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _userService.UpdateUser(updatedUser, userId));
        }

        [HttpGet("statistics/me")]
        public async Task<ActionResult> GetOwnStatistic()
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _userService.GetUserStatistic(userId));
        }
    }
}
