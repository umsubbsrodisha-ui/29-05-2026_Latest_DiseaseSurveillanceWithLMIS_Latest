//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Surveillance.Infrastructure.Persistence.Configurations
//{
//    internal class CaseRecordLabTestConfiguration
//    {
//    }
//}
// =======================================================
// CaseRecordLabTestConfiguration.cs
// =======================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveillance.Domain.Entities;

namespace Surveillance.Infrastructure.Persistence.Configurations;

public class CaseRecordLabTestConfiguration
    : IEntityTypeConfiguration<CaseRecordLabTest>
{
    public void Configure(EntityTypeBuilder<CaseRecordLabTest> builder)
    {
        // ===================================================
        // PRIMARY KEY
        // ===================================================

        builder.HasKey(x => x.Id);

        // ===================================================
        // PROPERTIES
        // ===================================================

        //builder.Property(x => x.ResultValue)
        //    .HasMaxLength(500);

        builder.Property(x => x.ReportPath)
            .HasMaxLength(500);

        // ===================================================
        // CASE RECORD RELATION
        // ONE CASE RECORD → MANY LAB TESTS
        // ===================================================

        builder.HasOne(x => x.CaseRecord)
            .WithMany(x => x.LabTests)
            .HasForeignKey(x => x.CaseRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        // ===================================================
        // LAB TEST RELATION
        // ===================================================

        builder.HasOne(x => x.LabTest)
      .WithMany(x => x.CaseRecordLabTests)
      .HasForeignKey(x => x.LabTestId)
      .OnDelete(DeleteBehavior.Restrict);

        // ===================================================
        // SAMPLE RELATION
        // ONE SAMPLE → MANY TESTS
        // ===================================================

        builder.HasOne(x => x.Sample)
            .WithMany(x => x.LabTests)
            .HasForeignKey(x => x.SampleId)
            .OnDelete(DeleteBehavior.Restrict);

        // ===================================================
        // INDEXES
        // ===================================================

        //builder.HasIndex(x => x.ResultStatus);

        builder.HasIndex(x => x.TestedAt);

        builder.HasIndex(x => x.CaseRecordId);

        builder.HasIndex(x => x.SampleId);
    }
}