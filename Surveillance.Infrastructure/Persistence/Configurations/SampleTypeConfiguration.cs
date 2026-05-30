//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Surveillance.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Surveillance.Infrastructure.Persistence.Configurations
//{
//    internal class SampleTypeConfiguration
//    {
//    }
//}


// =======================================================
// SampleTypeConfiguration.cs
// =======================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveillance.Domain.Entities;
using Surveillance.Infrastructure.Persistence.Seeds;

namespace Surveillance.Infrastructure.Persistence.Configurations;

public class SampleTypeConfiguration
    : IEntityTypeConfiguration<SampleType>
{
    public void Configure(EntityTypeBuilder<SampleType> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(x => x.Name)
            .IsUnique();

       
    }
}
