using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.DTOs
{
    public class FacilityStatDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int TotalCases { get; set; }
    }
}
