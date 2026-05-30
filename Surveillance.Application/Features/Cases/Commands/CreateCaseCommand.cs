using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Surveillance.Application.Features.Cases.Commands
{
    public class CreateCaseCommand : IRequest<int>
    {
        [Required(ErrorMessage = "Patient name is required")]
        [StringLength(100)]
        public string PatientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter valid 10-digit mobile number")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Disease name is required")]
        public string DiseaseName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string AddressOfPatient { get; set; } = string.Empty;

        [Required]
        public DateTime OnsetDate { get; set; }

        [Required]
        public DateTime DateReported { get; set; }

        public string UserId { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Facility is required")]
        public int FacilityId { get; set; }

        [MinLength(1, ErrorMessage = "Select at least one symptom")]
        public List<int> SymptomIds { get; set; } = new();

        [MinLength(1, ErrorMessage = "Select at least one sample type")]
        public List<int> SampleTypeIds { get; set; } = new();

        [MinLength(1, ErrorMessage = "Select at least one lab test")]
        public List<int> LabTestIds { get; set; } = new();
    }
}