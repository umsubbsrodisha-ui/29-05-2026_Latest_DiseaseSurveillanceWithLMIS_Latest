using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.DTOs
{
    public class DiseaseDistributionDto
    {
        public string DiseaseName { get; set; } = string.Empty;

        public int Count { get; set; }
    }
}
