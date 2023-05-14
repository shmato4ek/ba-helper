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
        private readonly DocumentService _documentService;
        private readonly JwtFactory _jwtFactory;
        public UserController(UserService userService, DocumentService documentService, JwtFactory jwtFactory)
        {
            _userService = userService;
            _documentService = documentService;
            _jwtFactory = jwtFactory;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(NewUserDTO userDTO)
        {
            return Ok(await _userService.CreateUser(userDTO));
        }

        //[HttpGet]
        //public async Task<ActionResult> GetAllUsers()
        //{
        //    return Ok(await _userService.GetAllUsers());
        //}

        //[HttpGet("tasks")]
        //public async Task<ActionResult> GetAllUsersTasks()
        //{
        //    return Ok(await _userService.GetAllUsersTasks(this.GetUserIdFromToken()));
        //}

        [HttpDelete]
        public async Task<ActionResult> DeleteUser()
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            await _userService.DeleteUser(userId);
            return NoContent();
        }

        [HttpGet]
        [Route("FromToken")]
        public async Task<IActionResult> GetUserFromToken()
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _userService.GetUserById(userId));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDTO updatedUser)
        {
            var token = Request.Headers["x-auth-token"].ToString();
            var userId = _jwtFactory.GetValueFromToken(token);
            return Ok(await _userService.UpdateUser(updatedUser, userId));
        }
    }
}
