using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsAgreedToNotification { get; set; } = true;
        public bool IsEnoughData { get; set; } = false;
        public List<Project> Projects { get; set; }
        public List<ProjectTask> Tasks { get; set; }
        public List<Document> Documents { get; set; }
        public List<StatisticData> Statistics { get; set; }
    }
}
