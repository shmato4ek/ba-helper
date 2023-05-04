using AutoMapper;
using BAHelper.BLL.Exceptions;
using BAHelper.BLL.JWT;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common.DTOs.Auth;
using BAHelper.Common.DTOs.User;
using BAHelper.Common.Security;
using BAHelper.DAL.Context;
using BAHelper.DAL.Entities;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.Services
{
    public class AuthService : BaseService
    {
        private protected readonly JwtFactory _jwtFactory;
        private readonly IConfiguration _configuration;
        public AuthService(BAHelperDbContext context, IMapper mapper, JwtFactory jwtFactory, IConfiguration configuration)
            :base(context, mapper)
        {
            _configuration = configuration;
            _jwtFactory = jwtFactory;
        }
        public async Task<AuthUserDTO> Authorize(LoginUserDTO userDto, bool checkPass = true)
        {
            var userEntity = await _context
                .Users
                .FirstOrDefaultAsync(user => user.Email == userDto.Email);
            if (userEntity is null)
            {
                throw new NotFoundException(nameof(User));
            }
            if (checkPass == true && !SecurityHelper.IsValidPassword(userEntity.Password, userDto.Password, userEntity.Salt))
            {
                throw new InvalidUserNameOrPasswordException();
            }

            var token = await GenerateAccessToken(userEntity.Id, userEntity.Name, userEntity.Email);
            var user = _mapper.Map<UserDTO>(userEntity);
            return new AuthUserDTO
            {
                Token = token,
                User = user,
            };
        }

        public async Task<TokenDTO> GenerateAccessToken(int id, string userName, string userEmail)
        {
            string refreshToken = _jwtFactory.GenerateRefreshToken();

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = id
            });

            await _context.SaveChangesAsync();

            string accessToken = await _jwtFactory.GenerateAccessToken(id, userName, userEmail);

            return new TokenDTO(accessToken, refreshToken);
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth)
        {
            try
            {
                string clientId = _configuration["GoogleClientId"];
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { clientId }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
                return payload;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
