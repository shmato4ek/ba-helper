using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class UserStory
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public ICollection<UserStoryFormula> Formulas { get; set; }
        public ICollection<AcceptanceCriteria> AcceptanceCriterias { get; set; }
    }
}
