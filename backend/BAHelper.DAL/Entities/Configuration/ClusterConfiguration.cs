using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities.Configuration
{
    public class ClusterConfiguration : IEntityTypeConfiguration<Cluster>
    {
        public void Configure(EntityTypeBuilder<Cluster> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.Users).WithMany(p => p.Clusters);
            builder.HasOne<Project>().WithMany().HasForeignKey(p => p.ProjectId);
        }
    }
}
