using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.AcceptanceCriteria
{
    public class AcceptanceCriteriaDTO
    {
        public int Id { get; set; }
        public int UserStoryId { get; set; }
        public string Text { get; set; }
    }
}
