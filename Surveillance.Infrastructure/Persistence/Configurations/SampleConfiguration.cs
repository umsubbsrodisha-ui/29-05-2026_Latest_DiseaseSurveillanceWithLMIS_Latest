//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Surveillance.Infrastructure.Persistence.Configurations
//{
//    internal class SampleConfiguration
//    {
//    }
//}

// =======================================================
// SampleConfiguration.cs
// =======================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveillance.Domain.Entities;

namespace Surveillance.Infrastructure.Persistence.Configurations;

public class SampleConfiguration
    : IEntityTypeConfiguration<Sample>
{
    public void Configure(EntityTypeBuilder<Sample> builder)
    {
        // ===================================================
        // PRIMARY KEY
        // ===================================================

        builder.HasKey(x => x.Id);

        // ===================================================
        // PROPERTIES
        // ===================================================

        builder.Property(x => x.Barcode)
            .HasMaxLength(100)
            .IsRequired();

        // ===================================================
        // CASE RECORD RELATION
        // ONE CASE RECORD → MANY SAMPLES
        // ===================================================

        builder.HasOne(x => x.CaseRecord)
            .WithMany(x => x.Samples)
            .HasForeignKey(x => x.CaseRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        // ===================================================
        // SAMPLE TYPE RELATION
        // ===================================================

        builder.HasOne(x => x.SampleType)
            .WithMany()
            .HasForeignKey(x => x.SampleTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // ===================================================
        // PROCESSING FACILITY RELATION
        // ===================================================

        builder.HasOne(x => x.ProcessingFacility)
            .WithMany()
            .HasForeignKey(x => x.ProcessingFacilityId)
            .OnDelete(DeleteBehavior.Restrict);

        // ===================================================
        // INDEXES
        // ===================================================

        builder.HasIndex(x => x.Barcode)
            .IsUnique();

        builder.HasIndex(x => x.Status);

        builder.HasIndex(x => x.CollectedAt);

        builder.HasIndex(x => x.CaseRecordId);
    }
}
