using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class Disease
    {
        public int Id { get; set; }


public string Name { get; set; } = string.Empty;

        public bool IsNotifiable { get; set; }

        public ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }
            = new List<DiseaseSymptom>();

        public ICollection<DiseaseLabTest> DiseaseLabTests { get; set; }
            = new List<DiseaseLabTest>();


}

}
