//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Surveillance.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Surveillance.Infrastructure.Persistence.Configurations
//{
//    internal class DiseaseSymptomConfiguration
//    {
//    }
//}

// =======================================================
// DiseaseSymptomConfiguration.cs
// =======================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveillance.Domain.Entities;
using Surveillance.Infrastructure.Persistence.Seeds;

namespace Surveillance.Infrastructure.Persistence.Configurations;

public class DiseaseSymptomConfiguration
    : IEntityTypeConfiguration<DiseaseSymptom>
{
    public void Configure(EntityTypeBuilder<DiseaseSymptom> builder)
    {
        builder.HasKey(x => new
        {
            x.DiseaseId,
            x.SymptomId
        });

        builder.HasOne(x => x.Disease)
            .WithMany(x => x.DiseaseSymptoms)
            .HasForeignKey(x => x.DiseaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Symptom)
            .WithMany(x => x.DiseaseSymptoms)
            .HasForeignKey(x => x.SymptomId)
            .OnDelete(DeleteBehavior.Cascade);

      
    }
}