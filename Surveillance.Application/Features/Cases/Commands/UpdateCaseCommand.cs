using MediatR;

namespace Surveillance.Application.Features.Cases.Commands
{
    public class UpdateCaseCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string DiseaseName { get; set; } = string.Empty;

        public string AddressOfPatient { get; set; } = string.Empty;

        public DateTime OnsetDate { get; set; }

        public DateTime DateReported { get; set; }

        public string? Notes { get; set; }

        public List<int> SymptomIds { get; set; } = new();

        public List<int> SampleTypeIds { get; set; } = new();

        public List<int> LabTestIds { get; set; } = new();
    }
}