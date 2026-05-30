using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class LabTest
    {
        // =====================================================
        // PRIMARY KEY
        // =====================================================

        public int Id { get; set; }

        // =====================================================
        // BASIC INFO
        // =====================================================

        public string Name { get; set; } = string.Empty;

        public string? ShortCode { get; set; }

        public string? Description { get; set; }

        // =====================================================
        // MASTER RELATIONS
        // WHICH SAMPLE TYPES ARE VALID FOR THIS TEST
        // =====================================================

        public ICollection<LabTestSampleType> LabTestSampleTypes
        { get; set; }
            = new List<LabTestSampleType>();

        // =====================================================
        // DISEASE MAPPING
        // WHICH TESTS ARE RECOMMENDED FOR DISEASE
        // =====================================================

        public ICollection<DiseaseLabTest> DiseaseLabTests
        { get; set; }
            = new List<DiseaseLabTest>();

        // =====================================================
        // ACTUAL TESTS PERFORMED FOR CASES
        // =====================================================

        public ICollection<CaseRecordLabTest> CaseRecordLabTests
        { get; set; }
            = new List<CaseRecordLabTest>();

        // =====================================================
        // OPTIONAL CONFIGURATION
        // =====================================================

        public bool IsActive { get; set; } = true;

        public bool RequiresVerification { get; set; } = true;

        public int? ExpectedTurnaroundHours { get; set; }

        // =====================================================
        // AUDIT
        // =====================================================

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }
}
