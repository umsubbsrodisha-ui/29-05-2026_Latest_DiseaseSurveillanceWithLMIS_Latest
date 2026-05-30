
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class CaseRecordLabTest
    {
        public Guid Id { get; set; }


        public int CaseRecordId { get; set; }

        public CaseRecord CaseRecord { get; set; } = default!;

        public Guid SampleId { get; set; }

        public Sample Sample { get; set; } = default!;
        public int LabTestId { get; set; }

        public LabTest LabTest { get; set; } = default! ;

        public DateTime? TestedAt { get; set; }

        public string? ReportPath { get; set; }

        public ICollection<LabResult> LabResults { get; set; }
            = new List<LabResult>();

}

}
