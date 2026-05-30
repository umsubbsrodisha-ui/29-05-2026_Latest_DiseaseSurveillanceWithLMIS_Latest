using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.DTOs
{
    public class FacilityDiseaseBreakdownDto
    {
        public int FacilityId { get; set; }
        public List<DiseaseCountDto> Diseases { get; set; } = new();
    }

    public class DiseaseCountDto
    {
        public string DiseaseName { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
