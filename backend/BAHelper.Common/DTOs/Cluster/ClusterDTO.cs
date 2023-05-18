﻿using BAHelper.Common.DTOs.ClusterData;
using BAHelper.Common.DTOs.User;
using BAHelper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.Cluster
{
    public class ClusterDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public List<UserDTO> Users { get; set; }
        public List<ClusterDataDTO> Data { get; set; }
    }
}
