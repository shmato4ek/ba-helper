using BAHelper.Common.DTOs.Glossary;
using BAHelper.Common.DTOs.UserStory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.Document
{
    public class NewDocumentDto
    {
        public string Name { get; set; }
        public string? ProjectAim { get; set; }
        public List<NewGlossaryDTO> Glossaries { get; set; }
        public List<NewUserStoryDTO> UserStories { get; set; }
    }
}
