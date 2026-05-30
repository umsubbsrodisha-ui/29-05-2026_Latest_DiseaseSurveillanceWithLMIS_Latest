//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Surveillance.Infrastructure.Persistence.Configurations
//{
//    internal class LabResultConfiguration
//    {
//    }
//}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveillance.Domain.Entities;

namespace Surveillance.Infrastructure.Persistence.Configurations;

public class LabResultConfiguration
    : IEntityTypeConfiguration<LabResult>
{
    public void Configure(EntityTypeBuilder<LabResult> builder)
    {
        // ===================================================
        // PRIMARY KEY
        // ===================================================

        builder.HasKey(x => x.Id);

        // ===================================================
        // PROPERTIES
        // ===================================================

        builder.Property(x => x.ResultValue)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.EnteredByUserId)
            .HasMaxLength(450);

        builder.Property(x => x.VerifiedByUserId)
            .HasMaxLength(450);

        builder.Property(x => x.Remarks)
            .HasMaxLength(1000);

        // ===================================================
        // RELATION
        // ONE CASE RECORD LAB TEST → MANY LAB RESULTS
        // ===================================================

        builder.HasOne(x => x.CaseRecordLabTest)
            .WithMany(x => x.LabResults)
            .HasForeignKey(x => x.CaseRecordLabTestId)
            .OnDelete(DeleteBehavior.Cascade);

        // ===================================================
        // INDEXES
        // ===================================================

        builder.HasIndex(x => x.CaseRecordLabTestId);

        builder.HasIndex(x => x.ResultStatus);

        builder.HasIndex(x => x.EnteredAt);

        builder.HasIndex(x => x.IsVerified);
    }
}