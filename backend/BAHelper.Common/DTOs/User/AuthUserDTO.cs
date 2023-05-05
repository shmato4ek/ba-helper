using BAHelper.Common.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.User
{
    public class AuthUserDTO
    {
        public UserDTO User { get; set; }
        public TokenDTO Token { get; set; }
    }
}
