using BAHelper.API.Extensions;
using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.Document;
using BAHelper.Common.DTOs.Glossary;
using BAHelper.Common.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BAHelper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly DocumentService _documentService;
        public UserController(UserService userService, DocumentService documentService)
        {
            _userService = userService;
            _documentService = documentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(NewUserDTO userDTO)
        {
            UserDTO createdUser = await _userService.CreateUser(userDTO);
            return Ok(createdUser);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("tasks")]
        public async Task<IActionResult> GetAllUsersTasks(int userId)
        {
            return Ok(await _userService.GetAllUsersTasks(userId));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            return Ok(await _userService.DeleteUser(userId));
        }

        [HttpGet]
        [Route("FromToken")]
        public async Task<IActionResult> GetUserFromToken()
        {
            var user = await _userService.GetUserById(this.GetUserIdFromToken());

            return Ok(user);
        }
    }
}
