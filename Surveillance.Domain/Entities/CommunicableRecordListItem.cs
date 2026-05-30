using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Entities
{
    public class CommunicableRecordListItem
    {
        public int Id { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string FacilityName { get; set; } = string.Empty;

        public DateTime OnsetDate { get; set; }

    }
}
