using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities.Configuration
{
    public class StatisticDataConfiguration : IEntityTypeConfiguration<StatisticData>
    {
        public void Configure(EntityTypeBuilder<StatisticData> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne<User>().WithMany().HasForeignKey(p => p.UserId);
        }
    }
}
