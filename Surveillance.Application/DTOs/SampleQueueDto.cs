using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.DTOs
{
    public class SampleQueueDto
    {
        public Guid SampleId { get; set; }

        public int CaseRecordId { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string DiseaseName { get; set; } = string.Empty;

        public int FacilityId { get; set; }

        public string FacilityName { get; set; } = string.Empty;

        public int SampleTypeId { get; set; }

        public string SampleTypeName { get; set; } = string.Empty;

        public string SampleStatus { get; set; } = string.Empty;

        public string Barcode { get; set; } = string.Empty;

        public string? CollectedBy { get; set; }

        public string? CollectionNotes { get; set; }

        public DateTime? CollectedAt { get; set; }

        public DateTime? DispatchedAt { get; set; }

        public DateTime? ReceivedAtLabAt { get; set; }

        public string? DispatchReferenceNo { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
