using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.DTOs
{
    public class CaseSampleDto
    {
        // =====================================================
        // SAMPLE INFO
        // =====================================================

        public Guid SampleId { get; set; }

        public string SampleType { get; set; }
            = string.Empty;

        public string SampleStatus { get; set; }
            = string.Empty;

        // =====================================================
        // TRACKING
        // =====================================================

        public string? Barcode { get; set; }

        public string? DispatchReferenceNo { get; set; }

        // =====================================================
        // COLLECTION
        // =====================================================

        public DateTime? CollectedAt { get; set; }

        public string? CollectedBy { get; set; }

        public string? CollectionNotes { get; set; }

        // =====================================================
        // MOVEMENT
        // =====================================================

        public DateTime? DispatchedAt { get; set; }

        public DateTime? ReceivedAtLabAt { get; set; }

        // =====================================================
        // PROCESSING FACILITY
        // =====================================================

        public int? ProcessingFacilityId { get; set; }

        public string? ProcessingFacilityName { get; set; }
    }
}
