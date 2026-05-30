using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class DiseaseSymptom
    {
        public int DiseaseId { get; set; }


public Disease Disease { get; set; }

        public int SymptomId { get; set; }

        public Symptom Symptom { get; set; }

        public int Weight { get; set; }


}

}
