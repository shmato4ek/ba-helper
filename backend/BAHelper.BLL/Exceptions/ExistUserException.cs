using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.Exceptions
{
    public sealed class ExistUserException : Exception
    {
        public ExistUserException(string email)
            :base($"User with email {email} already exists.")
        { }
    }
}
