using BAHelper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.Services
{
    public class RaciMatrix
    {
        public List<string> Executors { get; set; }
        public List<string> Tasks { get; set; }
        public List<List<RaciState>> RACI { get ; set; }
    }
}
