namespace Surveillance.Application.DTOs
{
    public class EditCaseDto
    {
        public int Id { get; set; }

        public string PatientName { get; set; }
            = string.Empty;

        public string Phone { get; set; }
            = string.Empty;

        public string DiseaseName { get; set; }
            = string.Empty;

        public string AddressOfPatient { get; set; }
            = string.Empty;

        public DateTime OnsetDate { get; set; }

        public DateTime DateReported { get; set; }

        public string? Notes { get; set; }

        public string CaseStatus { get; set; }
            = string.Empty;

        public int FacilityId { get; set; }

        public List<int> SelectedSymptomIds { get; set; }
            = new();

        public List<int> SelectedSampleTypeIds { get; set; }
            = new();

        public List<int> SelectedLabTestIds { get; set; }
            = new();
    }
}
