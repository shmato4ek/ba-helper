﻿using BAHelper.Common.DTOs.AcceptanceCriteria;
using BAHelper.Common.DTOs.UserStoryFormula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.UserStory
{
    public class NewUserStoryDTO
    {
        public string Name { get; set; }
        public List<NewUserStoryFormulaDTO> UserStoryFormulas { get; set; }
        public List<NewAcceptanceCriteriaDTO> AcceptanceCriterias { get; set; }
    }
}
