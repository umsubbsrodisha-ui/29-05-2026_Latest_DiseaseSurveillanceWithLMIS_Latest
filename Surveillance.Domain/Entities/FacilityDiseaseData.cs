using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class FacilityDiseaseData
    {
        public int FacilityId { get; set; }

        public string FacilityName { get; set; } = string.Empty;

        public string DiseaseName { get; set; } = string.Empty;

        public int Count { get; set; }
    }
}
