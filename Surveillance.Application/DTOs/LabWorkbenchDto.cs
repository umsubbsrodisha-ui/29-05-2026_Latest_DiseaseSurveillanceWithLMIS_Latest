namespace Surveillance.Application.DTOs
{
    public class LabWorkbenchDto
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

        public string SampleStatus { get; set; } = string.Empty;

        public int LabTestId { get; set; }

        public string LabTestName { get; set; } = string.Empty;

        public DateTime? TestedAt { get; set; }

        public string? ReportPath { get; set; }

        public string? LatestResultValue { get; set; }

        public string? LatestResultStatus { get; set; }

        public DateTime? LatestResultEnteredAt { get; set; }

        public bool? LatestResultIsVerified { get; set; }

        public string? LatestResultRemarks { get; set; }
    }
}