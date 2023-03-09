using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities.Configuration
{
    public class UserStoryFormulaConfiguration : IEntityTypeConfiguration<UserStoryFormula>
    {
        public void Configure(EntityTypeBuilder<UserStoryFormula> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne<UserStory>().WithMany().HasForeignKey(p => p.UserStoryId);
        }
    }
}
