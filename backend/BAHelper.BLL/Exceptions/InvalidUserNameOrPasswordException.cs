using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.Exceptions
{
    public sealed class InvalidUserNameOrPasswordException : Exception
    {
        public InvalidUserNameOrPasswordException()
            : base("Invalid username or password.")
        { }
    }
}
