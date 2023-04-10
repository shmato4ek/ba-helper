using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Project> Projects { get; set; }
        public List<ProjectTask> Tasks { get; set; }
        public List<Document> Documents { get; set; }
    }
}
