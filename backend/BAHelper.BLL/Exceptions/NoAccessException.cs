using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.BLL.Exceptions
{
    public sealed class NoAccessException : Exception
    {
        public NoAccessException(int userId)
            :base($"No access to {userId}.")
        { }
    }
}
