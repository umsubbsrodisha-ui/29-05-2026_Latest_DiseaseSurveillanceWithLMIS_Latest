//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Surveillance.Infrastructure.Persistence.Configurations
//{
//    internal class LabTestSampleTypeConfiguration
//    {
//    }
//}
// =======================================================
// LabTestSampleTypeConfiguration.cs
// =======================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surveillance.Domain.Entities;
using Surveillance.Infrastructure.Persistence.Seeds;

namespace Surveillance.Infrastructure.Persistence.Configurations;

public class LabTestSampleTypeConfiguration
    : IEntityTypeConfiguration<LabTestSampleType>
{
    public void Configure(EntityTypeBuilder<LabTestSampleType> builder)
    {
        builder.HasKey(x => new
        {
            x.LabTestId,
            x.SampleTypeId
        });

        builder.HasOne(x => x.LabTest)
            .WithMany(x => x.LabTestSampleTypes)
            .HasForeignKey(x => x.LabTestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.SampleType)
            .WithMany(x => x.LabTestSampleTypes)
            .HasForeignKey(x => x.SampleTypeId)
            .OnDelete(DeleteBehavior.Cascade);

       
    }
}