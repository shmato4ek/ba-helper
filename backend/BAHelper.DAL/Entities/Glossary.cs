using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class Glossary
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
    }
}
