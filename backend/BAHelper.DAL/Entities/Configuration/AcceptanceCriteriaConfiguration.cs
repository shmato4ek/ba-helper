using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities.Configuration
{
    public class AcceptanceCriteriaConfiguration : IEntityTypeConfiguration<AcceptanceCriteria>
    {
        public void Configure(EntityTypeBuilder<AcceptanceCriteria> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne<UserStory>().WithMany().HasForeignKey(p => p.UserStoryId);
        }
    }
}
