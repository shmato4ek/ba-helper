﻿using BAHelper.API.Extensions;
using BAHelper.BLL.Services;
using BAHelper.Common.DTOs.User;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BAHelper.API.Controllers
{
    [Route("api")]
    [ApiController]
    [EnableCors()]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        public AuthController(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("Register")]
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

        [HttpPost("Login")]
        public async Task<ActionResult<AuthUserDTO>> Login([FromBody] LoginUserDTO userDTO)
        {
            return Ok(await _authService.Authorize(userDTO));
        }

        [HttpGet("auth/me")]
        public async Task<ActionResult> GetUserByToken(string token)
        {
            return Ok(await _userService.GetUserById(this.GetUserIdFromToken()));
        }
    }
}
