using System;
using System.Collections.Generic;
using System.Text;

//namespace Surveillance.Domain.Enums
//{
//    public enum NotificationType
//    {
//        NewCase = 1,
//        ConfirmedPositive = 2,
//        ConfirmedNegative = 3
//    }
//}
namespace Surveillance.Domain.Enums
{
    public enum NotificationType
    {
        // --------------------------------
        // Existing Surveillance Workflow
        // --------------------------------
        NewCase = 1,

        ConfirmedPositive = 2,

        ConfirmedNegative = 3,

        // --------------------------------
        // LMIS Workflow Notifications
        // --------------------------------
        SampleCollectionRequested = 4,

        SampleCollected = 5,

        SampleDispatched = 6,

        SampleReceivedAtLab = 7,

        SampleRejected = 8,

        LabTestStarted = 9,

        LabTestCompleted = 10,

        LabResultApproved = 11,

        ReportGenerated = 12,
        LabResultPendingApproval=13,
     
    }
}