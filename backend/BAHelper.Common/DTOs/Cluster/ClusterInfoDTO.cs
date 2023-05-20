using BAHelper.Common.DTOs.ClusterData;
using BAHelper.Common.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.Common.DTOs.Cluster
{
    public class ClusterInfoDTO
    {
        public string ProjectName { get; set; }
        public List<UserInfoDTO> Users { get; set; }
        public List <ClusterDataDTO> Data { get; set; }
    }
}
