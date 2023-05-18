using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities.Configuration
{
    public class TaskTopicConfiguration : IEntityTypeConfiguration<TaskTopic>
    {
        public void Configure(EntityTypeBuilder<TaskTopic> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne<ProjectTask>().WithMany().HasForeignKey(p => p.TaskId);
        }
    }
}
