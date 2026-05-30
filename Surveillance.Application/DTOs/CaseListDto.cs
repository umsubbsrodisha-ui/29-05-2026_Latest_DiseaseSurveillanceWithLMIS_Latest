using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.DTOs
{
    public class CaseListDto
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string DiseaseName { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;
    }
}
