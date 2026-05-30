using Surveillance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class Sample
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // =========================================
        // CASE RELATION
        // =========================================

        public int CaseRecordId { get; set; }

        public CaseRecord CaseRecord { get; set; } = default!;

        // =========================================
        // SAMPLE TYPE
        // =========================================

        public int SampleTypeId { get; set; }

        public SampleType SampleType { get; set; } = default!;

        // =========================================
        // COLLECTION DETAILS
        // =========================================

        public DateTime? CollectedAt { get; set; }
           

        public string Barcode { get; set; } = string.Empty;

        public string? CollectedBy { get; set; }

        public string? CollectionNotes { get; set; }

        // =========================================
        // SAMPLE STATUS
        // =========================================

        public SampleStatus Status { get; set; }
            = SampleStatus.PendingCollection;

        // =========================================
        // LAB / FACILITY PROCESSING
        // =========================================

        public int? ProcessingFacilityId { get; set; }

        public Facility? ProcessingFacility { get; set; }

        public DateTime? DispatchedAt { get; set; }

        public DateTime? ReceivedAtLabAt { get; set; }

        public string? DispatchReferenceNo { get; set; }

        // =========================================
        // TESTS
        // =========================================

        public ICollection<CaseRecordLabTest> LabTests { get; set; }
            = new List<CaseRecordLabTest>();

        // =========================================
        // AUDIT
        // =========================================

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;
    }

}
