using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAHelper.DAL.Entities.Configuration
{
    public class GlossaryConfiguration : IEntityTypeConfiguration<Glossary>
    {
        public void Configure(EntityTypeBuilder<Glossary> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne<Document>().WithMany().HasForeignKey(p => p.DocumentId);
        }
    }
}
