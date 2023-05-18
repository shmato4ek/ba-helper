using BAHelper.Common.Enums;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities
{
    public class Cluster
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public List<User> Users { get; set; }
        public List<ClusterData> Data { get; set; }
    }
}
