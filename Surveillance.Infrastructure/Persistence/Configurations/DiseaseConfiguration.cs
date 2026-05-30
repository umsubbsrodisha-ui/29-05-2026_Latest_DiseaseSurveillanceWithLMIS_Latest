using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveillance.Domain.Entities;
using Surveillance.Infrastructure.Persistence.Seeds ;


namespace Surveillance.Infrastructure.Persistence.Configurations
{
  

    public class DiseaseConfiguration
        : IEntityTypeConfiguration<Disease>
    {
        public void Configure(EntityTypeBuilder<Disease> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(x => x.Name)
                .IsUnique();

           
        }
    }
}
