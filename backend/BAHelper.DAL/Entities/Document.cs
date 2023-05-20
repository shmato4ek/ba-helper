using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string? ProjectAim { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime ArchivedDate { get; set; }
        public List<Glossary> Glossary { get; set; }
        public List<UserStory> UserStories { get; set; }
    }
}
