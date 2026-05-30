using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class FacilityCaseInfo
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string DiseaseName { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;
    }
}
