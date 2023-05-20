using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.Auth
{
    public class TokenDTO
    {
        public string AccessToken { get; }
        public TokenDTO(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
