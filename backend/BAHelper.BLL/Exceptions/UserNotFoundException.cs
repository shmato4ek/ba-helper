using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.Exceptions
{
    public sealed class UserNotFoundException : Exception
    {
        public UserNotFoundException(string email)
            : base($"User with email {email} not exists.")
        { }
    }
}
