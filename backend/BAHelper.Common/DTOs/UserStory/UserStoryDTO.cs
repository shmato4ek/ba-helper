using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAHelper.Common.DTOs.AcceptanceCriteria;
using BAHelper.Common.DTOs.UserStoryFormula;

namespace BAHelper.Common.DTOs.UserStory
{
    public class UserStoryDTO
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public List<string> Formulas { get; set; }
        public List<string> AcceptanceCriterias { get; set; }
    }
}
