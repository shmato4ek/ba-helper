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
        public int DocumetnId { get; set; }
        public string Name { get; set; }
        public List<UserStoryFormulaDTO> UserStoryFormulas { get; set; }
        public List<AcceptanceCriteriaDTO> AcceptanceCriterias { get; set; }
    }
}
