using Surveillance.Application.DTOs;
using Surveillance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendNotification(int caseRecordId, NotificationType type);
        Task MarkAsChecked(int id);
        Task NotifyDashboardUpdate();

        Task SendFacilityNotification(
             int caseRecordId,
             int facilityId,
             NotificationType type,
             params string[] roles);

        Task SendSurveillanceNotification(
            int caseRecordId,
            NotificationType type);

        Task SendFacilityLabResultNotification(
            int caseRecordId,
            int facilityId,
            NotificationType type,
            Guid labResultId,
            params string[] roles);

        Task SendSurveillanceLabResultNotification(
            int caseRecordId,
            NotificationType type,
            Guid labResultId);


     
    }
}
