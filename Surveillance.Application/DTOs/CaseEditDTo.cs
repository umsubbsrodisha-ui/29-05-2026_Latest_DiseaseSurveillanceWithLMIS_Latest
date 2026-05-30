using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.DTOs
{
    public class CaseEditDTo
    {
        public int Id { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string DiseaseName { get; set; } = string.Empty;

        public string Symptoms { get; set; } = string.Empty;

        public string AddressOfPatient { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public bool IsCommunicable { get; set; }

        public string Status { get; set; } = string.Empty;    //Only status changes

        public int FacilityId { get; set; }

        public string FacilityName { get; set; } = string.Empty;
    }
}
