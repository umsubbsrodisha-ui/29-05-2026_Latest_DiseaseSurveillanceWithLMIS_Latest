using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class Symptom
    {
        public int Id { get; set; }


        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<CaseRecordSymptom> CaseRecordSymptoms { get; set; }
            = new List<CaseRecordSymptom>();

        public ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; }
            = new List<DiseaseSymptom>();


}

}
