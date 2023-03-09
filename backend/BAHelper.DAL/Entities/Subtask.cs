using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class Subtask
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
    }
}
