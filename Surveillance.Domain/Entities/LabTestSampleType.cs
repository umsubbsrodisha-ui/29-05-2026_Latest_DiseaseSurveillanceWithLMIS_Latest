using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class LabTestSampleType
    {
        public int LabTestId { get; set; }


public LabTest LabTest { get; set; }

        public int SampleTypeId { get; set; }

        public SampleType SampleType { get; set; }


}

}
