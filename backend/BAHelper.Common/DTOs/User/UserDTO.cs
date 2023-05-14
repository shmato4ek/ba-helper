using BAHelper.Common.DTOs.Document;
using BAHelper.Common.DTOs.Project;
using BAHelper.Common.DTOs.ProjectTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAgreedToNotification { get; set; }
        public List<ProjectDTO> Projects { get; set; }
        public List<ProjectTaskDTO> Tasks { get; set; }
        public List<DocumentDTO> Documents { get; set; }
    }
}
