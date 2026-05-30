public class CaseLabReportTestDto
{
    public string LabTestName { get; set; } = string.Empty;

    public string ResultStatus { get; set; } = string.Empty;

    public string? ResultValue { get; set; }

    public string? Remarks { get; set; }

    public DateTime? TestedAt { get; set; }

    public bool IsVerified { get; set; }
}