public class CaseLabReportDto
{
    public int CaseRecordId { get; set; }

    public string PatientName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string DiseaseName { get; set; } = string.Empty;

    public string FacilityName { get; set; } = string.Empty;

    public string SampleType { get; set; } = string.Empty;

    public string Barcode { get; set; } = string.Empty;

    public DateTime? CollectedAt { get; set; }

    public bool IsVerified { get; set; }

    public List<CaseLabReportTestDto> Tests { get; set; }
        = new();
}