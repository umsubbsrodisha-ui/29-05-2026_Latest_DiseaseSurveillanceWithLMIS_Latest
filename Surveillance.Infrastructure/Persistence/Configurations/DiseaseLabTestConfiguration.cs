//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Surveillance.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Surveillance.Infrastructure.Persistence.Configurations
//{
//    internal class DiseaseLabTestConfiguration
//    {
//    }
//}

// =======================================================
// DiseaseLabTestConfiguration.cs
// =======================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveillance.Domain.Entities;
using Surveillance.Infrastructure.Persistence.Seeds;

namespace Surveillance.Infrastructure.Persistence.Configurations;

public class DiseaseLabTestConfiguration
    : IEntityTypeConfiguration<DiseaseLabTest>
{
    public void Configure(EntityTypeBuilder<DiseaseLabTest> builder)
    {
        builder.HasKey(x => new
        {
            x.DiseaseId,
            x.LabTestId
        });

        builder.HasOne(x => x.Disease)
            .WithMany(x => x.DiseaseLabTests)
            .HasForeignKey(x => x.DiseaseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.LabTest)
            .WithMany(x => x.DiseaseLabTests)
            .HasForeignKey(x => x.LabTestId)
            .OnDelete(DeleteBehavior.Cascade);

      
    }
}
