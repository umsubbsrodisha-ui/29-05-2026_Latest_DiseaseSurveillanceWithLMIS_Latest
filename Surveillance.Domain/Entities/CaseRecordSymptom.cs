using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class CaseRecordSymptom
    {
        public Guid Id { get; set; }


public int CaseRecordId { get; set; }

        public CaseRecord CaseRecord { get; set; }

        public int SymptomId { get; set; }

        public Symptom Symptom { get; set; }


}

}
