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
            return Ok(await _userService.DeleteUser(this.GetUserIdFromToken()));
        }

        [HttpGet]
        [Route("FromToken")]
        public async Task<IActionResult> GetUserFromToken()
        {
            return Ok(await _userService.GetUserById(this.GetUserIdFromToken()));
        }
    }
}
