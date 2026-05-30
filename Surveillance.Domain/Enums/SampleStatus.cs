using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Domain.Enums
{
    public enum SampleStatus
    {
        PendingCollection,
        Collected,
        Dispatched,
        Received,
        Rejected,
        Processing,
        Tested,
        Archived
    }

}
