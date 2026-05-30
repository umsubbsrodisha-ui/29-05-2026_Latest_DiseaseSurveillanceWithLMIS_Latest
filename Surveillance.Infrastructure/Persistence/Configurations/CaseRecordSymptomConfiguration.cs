//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Surveillance.Infrastructure.Persistence.Configurations
//{
//    internal class CaseRecordSymptomConfiguration
//    {
//    }
//}
// =======================================================
// CaseRecordSymptomConfiguration.cs
// =======================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveillance.Domain.Entities;

namespace Surveillance.Infrastructure.Persistence.Configurations;

public class CaseRecordSymptomConfiguration
    : IEntityTypeConfiguration<CaseRecordSymptom>
{
    public void Configure(EntityTypeBuilder<CaseRecordSymptom> builder)
    {
        builder.HasKey(x => new
        {
            x.CaseRecordId,
            x.SymptomId
        });

        builder.HasOne(x => x.CaseRecord)
            .WithMany(x => x.CaseRecordSymptoms)
            .HasForeignKey(x => x.CaseRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Symptom)
            .WithMany(x => x.CaseRecordSymptoms)
            .HasForeignKey(x => x.SymptomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}