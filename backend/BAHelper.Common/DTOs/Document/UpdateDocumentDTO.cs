using BAHelper.Common.DTOs.Glossary;
using BAHelper.Common.DTOs.UserStory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.Document
{
    public class UpdateDocumentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectAim { get; set; }
        public List<GlossaryDTO> Glossaries { get; set; }
        public List<UserStoryDTO> UserStories { get; set; }
    }
}
