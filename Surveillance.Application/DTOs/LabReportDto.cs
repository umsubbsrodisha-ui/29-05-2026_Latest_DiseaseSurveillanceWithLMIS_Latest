namespace Surveillance.Application.DTOs
{
    public class LabReportDto
    {
        public Guid LabResultId { get; set; }

        public Guid CaseRecordLabTestId { get; set; }

        public int CaseRecordId { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string DiseaseName { get; set; } = string.Empty;

        public string FacilityName { get; set; } = string.Empty;

        public string SampleTypeName { get; set; } = string.Empty;

        public string SampleBarcode { get; set; } = string.Empty;

        public string LabTestName { get; set; } = string.Empty;

        public string ResultValue { get; set; } = string.Empty;

        public string ResultStatus { get; set; } = string.Empty;

        public DateTime EnteredAt { get; set; }

        public bool IsVerified { get; set; }

        public string? Remarks { get; set; }

        public string? ReportLink { get; set; }
    }
}