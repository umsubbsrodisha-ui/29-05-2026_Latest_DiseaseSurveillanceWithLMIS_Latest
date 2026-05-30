using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class SampleType
    {
        public int Id { get; set; }


public string Name { get; set; } = string.Empty;

        public ICollection<LabTestSampleType> LabTestSampleTypes { get; set; }
            = new List<LabTestSampleType>();


}

}
