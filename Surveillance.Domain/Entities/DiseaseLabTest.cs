using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class DiseaseLabTest
    {
        public int DiseaseId { get; set; }


public Disease Disease { get; set; }

        public int LabTestId { get; set; }

        public LabTest LabTest { get; set; }


}

}
