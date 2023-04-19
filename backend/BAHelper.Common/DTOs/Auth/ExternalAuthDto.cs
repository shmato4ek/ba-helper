using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.Auth
{
    public class ExternalAuthDto
    {
        public string Provider { get; set; }
        public string IdToken { get; set; }
        public string Name { get; set; }
    }
}
