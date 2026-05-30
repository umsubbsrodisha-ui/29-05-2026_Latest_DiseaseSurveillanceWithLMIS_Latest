using Surveillance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    // =========================================================
    // SAMPLE DETAILS
    // =========================================================

    public class CaseSampleDetails
    {
        public Guid SampleId { get; set; }

        public string SampleType { get; set; }
            = string.Empty;

        public SampleStatus SampleStatus { get; set; }

        public string? Barcode { get; set; }

        public string? DispatchReferenceNo { get; set; }

        public DateTime? CollectedAt { get; set; }

        public string? CollectedBy { get; set; }

        public string? CollectionNotes { get; set; }

        public DateTime? DispatchedAt { get; set; }

        public DateTime? ReceivedAtLabAt { get; set; }

        public int? ProcessingFacilityId { get; set; }

        public string? ProcessingFacilityName { get; set; }
    }
}
