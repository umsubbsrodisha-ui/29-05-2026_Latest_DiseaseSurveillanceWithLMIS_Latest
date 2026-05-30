using Surveillance.Domain.Enums;

namespace Surveillance.Domain.Entities
{
    public class LabWorkbenchDetails
    {
        public Guid CaseRecordLabTestId { get; set; }

        public int CaseRecordId { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string DiseaseName { get; set; } = string.Empty;

        public int FacilityId { get; set; }

        public string FacilityName { get; set; } = string.Empty;

        public Guid SampleId { get; set; }

        public string SampleBarcode { get; set; } = string.Empty;

        public string SampleTypeName { get; set; } = string.Empty;

        public SampleStatus SampleStatus { get; set; }

        public int LabTestId { get; set; }

        public string LabTestName { get; set; } = string.Empty;

        public DateTime? TestedAt { get; set; }

        public string? ReportPath { get; set; }

        public string? LatestResultValue { get; set; }

        public LabResultStatus? LatestResultStatus { get; set; }

        public DateTime? LatestResultEnteredAt { get; set; }

        public bool? LatestResultIsVerified { get; set; }

        public string? LatestResultRemarks { get; set; }
    }
}