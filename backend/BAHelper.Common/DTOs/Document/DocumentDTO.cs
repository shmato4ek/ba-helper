using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAHelper.Common.DTOs.Glossary;
using BAHelper.Common.DTOs.UserStory;

namespace BAHelper.Common.DTOs.Document
{
    public class DocumentDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string ProjectAim { get; set; }
        public bool IsDeleted { get; set; }
        public List<GlossaryDTO> Glossaries { get; set; } = null;
        public List<UserStoryDTO> UserStories { get; set; } = null;
    }
}
