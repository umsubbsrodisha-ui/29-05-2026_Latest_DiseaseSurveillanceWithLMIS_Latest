using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Surveillance.Application.DTOs;
using Surveillance.Application.Interfaces.Services;
using Surveillance.Domain.Entities;
using Surveillance.Domain.Enums;
using Surveillance.Infrastructure.Data;
using Surveillance.Infrastructure.Hubs;


namespace Surveillance.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public NotificationService(
            IHubContext<NotificationHub> hubContext,
            IDbContextFactory<AppDbContext> dbFactory)
        {
            _hubContext = hubContext;
            _dbFactory = dbFactory;
        }


        public async Task SendNotification(
    int caseRecordId,
    NotificationType type)
        {
            var data = await GetOrCreateNotification(caseRecordId, type);

            if (data == null)
                return;

            using var db = _dbFactory.CreateDbContext();

            await AddGlobalRecipientsAsync(
                db,
                data.NotificationId);

            await _hubContext.Clients
                .Group(NotificationGroups.Analysts)
                .SendAsync(
                    "ReceiveNotification",
                    data.NotificationId,
                    type.ToString(),
                    data.DiseaseName,
                    data.FacilityName);
        }

        public async Task SendNotification_Old(int caseRecordId, NotificationType type)
        {
            var data = await GetOrCreateNotification(caseRecordId, type);

            if (data == null)
                return;

            await _hubContext.Clients
                .Group(NotificationGroups.Analysts)
                .SendAsync(
                    "ReceiveNotification",
                    data.NotificationId,
                    type.ToString(),
                    data.DiseaseName,
                    data.FacilityName);
        }

        public async Task SendFacilityNotification(
            int caseRecordId,
            int facilityId,
            NotificationType type,
            params string[] roles)
        {
            var data = await GetOrCreateNotification(caseRecordId, type);

            if (data == null)
                return;

            foreach (var role in roles)
            {
                string? groupName = role switch
                {
                    "LT" => NotificationGroups.LT(facilityId),
                    "MB" => NotificationGroups.MB(facilityId),
                    "MO" => NotificationGroups.MO(facilityId),
                    _ => null
                };

                if (groupName == null)
                    continue;

                await _hubContext.Clients
                    .Group(groupName)
                    .SendAsync(
                        "ReceiveNotification",
                        data.NotificationId,
                        type.ToString(),
                        data.DiseaseName,
                        data.FacilityName);
            }
        }

        public async Task SendSurveillanceLabResultNotification(int caseRecordId,NotificationType type,Guid labResultId)
        {
            var data = await GetOrCreateNotification(
                caseRecordId,
                type,
                labResultId);

            if (data == null)
                return;

            using var db = _dbFactory.CreateDbContext();

            await AddGlobalRecipientsAsync(
                db,
                data.NotificationId);

            await _hubContext.Clients
                .Group(NotificationGroups.Analysts)
                .SendAsync(
                    "ReceiveNotification",
                    data.NotificationId,
                    type.ToString(),
                    data.DiseaseName,
                    data.FacilityName);
        }

        public async Task SendSurveillanceNotification(
            int caseRecordId,
            NotificationType type)
        {
            await SendNotification(caseRecordId, type);
        }

        public async Task MarkAsChecked(int id)
        {
            using var db = _dbFactory.CreateDbContext();

            var n = await db.Notifications.FindAsync(id);

            if (n != null)
            {
                n.IsChecked = true;
                await db.SaveChangesAsync();
            }
        }

        public async Task NotifyDashboardUpdate()
        {
            await _hubContext.Clients
                .Group(NotificationGroups.Dashboard)
                .SendAsync("DashboardUpdated");
        }

        private async Task<NotificationPayload?> GetOrCreateNotification(
     int caseRecordId,
     NotificationType type,
     Guid? labResultId = null)
        {
            using var db = _dbFactory.CreateDbContext();

            var caseRecord = await db.CaseRecords
                .Include(c => c.Facility)
                .FirstOrDefaultAsync(c => c.Id == caseRecordId);

            if (caseRecord == null)
                return null;

            var notification = await db.Notifications
                .FirstOrDefaultAsync(n =>
                    n.CaseRecordId == caseRecordId &&
                    n.Type == type);

            if (notification == null)
            {
                notification = new Notification
                {
                    CaseRecordId = caseRecord.Id,
                    FacilityId = caseRecord.FacilityId,
                    DiseaseName = caseRecord.DiseaseName,
                    Type = type,
                    Timestamp = DateTime.UtcNow,
                    IsChecked = false,

                    // NEW
                    LabResultId = labResultId
                };

                db.Notifications.Add(notification);

                await db.SaveChangesAsync();
            }
            else if (labResultId.HasValue &&
                     notification.LabResultId == null)
            {
                notification.LabResultId = labResultId.Value;

                await db.SaveChangesAsync();
            }

           

            return new NotificationPayload
            {
                NotificationId = notification.Id,
                DiseaseName = caseRecord.DiseaseName,
                FacilityName = caseRecord.Facility?.FacilityName ?? "Unknown"
            };
        }

        private class NotificationPayload
        {
            public int NotificationId { get; set; }

            public string DiseaseName { get; set; } = string.Empty;

            public string FacilityName { get; set; } = string.Empty;
        }


        public async Task SendFacilityLabResultNotification(
        int caseRecordId,
        int facilityId,
        NotificationType type,
        Guid labResultId,
        params string[] roles)
        {
            var data = await GetOrCreateNotification(
                caseRecordId,
                type,
                labResultId);

            if (data == null)
                return;

            foreach (var role in roles)
            {
                string? groupName = role switch
                {
                    "LT" => NotificationGroups.LT(facilityId),
                    "MB" => NotificationGroups.MB(facilityId),
                    "MO" => NotificationGroups.MO(facilityId),
                    _ => null
                };

                if (groupName == null)
                    continue;

                await _hubContext.Clients
                    .Group(groupName)
                    .SendAsync(
                        "ReceiveNotification",
                        data.NotificationId,
                        type.ToString(),
                        data.DiseaseName,
                        data.FacilityName);
            }
        }

        public async Task SendSurveillanceLabResultNotification_old(int caseRecordId, NotificationType type, Guid labResultId)



        {
            var data = await GetOrCreateNotification(
                caseRecordId,
                type,
                labResultId);

            if (data == null)
                return;

            await _hubContext.Clients
                .Group(NotificationGroups.Analysts)
                .SendAsync(
                    "ReceiveNotification",
                    data.NotificationId,
                    type.ToString(),
                    data.DiseaseName,
                    data.FacilityName);
        }


        private async Task AddGlobalRecipientsAsync(AppDbContext db,int notificationId)
        {
            string[] globalRoles =
            {
        "Admin",
        "Analyst",
        "NodalOfficer",
        "AddlnCommissioner",
        "MD",
        "Commissioner",
        "JdAdmin"
    };

            var normalizedRoles = globalRoles
                .Select(r => r.ToUpperInvariant())
                .ToList();

            var userIds = await db.UserRoles
                .Join(
                    db.Roles,
                    userRole => userRole.RoleId,
                    role => role.Id,
                    (userRole, role) => new
                    {
                        userRole.UserId,
                        RoleName = role.Name
                    })
                .Where(x =>
                    x.RoleName != null &&
                    normalizedRoles.Contains(x.RoleName.ToUpper()))
                .Select(x => x.UserId)
                .Distinct()
                .ToListAsync();

            foreach (var userId in userIds)
            {
                var exists = await db.NotificationRecipients.AnyAsync(x =>
                    x.NotificationId == notificationId &&
                    x.UserId == userId);

                if (!exists)
                {
                    db.NotificationRecipients.Add(new NotificationRecipient
                    {
                        NotificationId = notificationId,
                        UserId = userId,
                        IsRead = false,
                        ReadAt = null
                    });
                }
            }

            await db.SaveChangesAsync();
        }

   

      
    }
}







































//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using Surveillance.Application.Interfaces.Services;
//using Surveillance.Domain.Entities;
//using Surveillance.Domain.Enums;
//using Surveillance.Infrastructure.Data;
//using Surveillance.Infrastructure.Hubs;

//namespace Surveillance.Infrastructure.Services
//{
//    public class NotificationService : INotificationService 
//    {
//        private readonly IHubContext<NotificationHub> _hubContext;
//        private readonly IDbContextFactory<AppDbContext> _dbFactory;

//        public NotificationService(
//            IHubContext<NotificationHub> hubContext,
//            IDbContextFactory<AppDbContext> dbFactory)
//        {
//            _hubContext = hubContext;
//            _dbFactory = dbFactory;
//        }

//        // =====================================================
//        // EXISTING DEFAULT SURVEILLANCE NOTIFICATION
//        // =====================================================

//        public async Task SendNotification(
//            int caseRecordId,
//            NotificationType type)
//        {
//            using var db = _dbFactory.CreateDbContext();

//            var caseRecord = await db.CaseRecords
//                .Include(c => c.Facility)
//                .FirstOrDefaultAsync(c => c.Id == caseRecordId);

//            if (caseRecord == null)
//                return;

//            // Prevent duplicate notifications
//            var exists = await db.Notifications.AnyAsync(n =>
//                n.CaseRecordId == caseRecordId &&
//                n.Type == type);

//            if (exists)
//                return;

//            // Create notification
//            var notification = new Notification
//            {
//                CaseRecordId = caseRecord.Id,
//                FacilityId = caseRecord.FacilityId,
//                DiseaseName = caseRecord.DiseaseName,
//                Type = type,
//                Timestamp = DateTime.UtcNow,
//                IsChecked = false
//            };

//            db.Notifications.Add(notification);

//            await db.SaveChangesAsync();

//            // EXISTING ANALYST FLOW
//            await _hubContext.Clients
//                .Group(NotificationGroups.Analysts)
//                .SendAsync(
//                    "ReceiveNotification",
//                    notification.Id,
//                    notification.Type.ToString(),
//                    caseRecord.DiseaseName,
//                    caseRecord.Facility?.FacilityName ?? "Unknown"
//                );
//        }

//        // =====================================================
//        // NEW FACILITY ROLE NOTIFICATION
//        // =====================================================

//        public async Task SendFacilityNotification(
//            int caseRecordId,
//            int facilityId,
//            NotificationType type,
//            params string[] roles)
//        {
//            using var db = _dbFactory.CreateDbContext();

//            var caseRecord = await db.CaseRecords
//                .Include(c => c.Facility)
//                .FirstOrDefaultAsync(c => c.Id == caseRecordId);

//            if (caseRecord == null)
//                return;

//            var notification = new Notification
//            {
//                CaseRecordId = caseRecord.Id,
//                FacilityId = caseRecord.FacilityId,
//                DiseaseName = caseRecord.DiseaseName,
//                Type = type,
//                Timestamp = DateTime.UtcNow,
//                IsChecked = false
//            };

//            db.Notifications.Add(notification);

//            await db.SaveChangesAsync();

//            foreach (var role in roles)
//            {
//                string? groupName = role switch
//                {
//                    "LT" => NotificationGroups.LT(facilityId),

//                    "MB" => NotificationGroups.MB(facilityId),

//                    "MO" => NotificationGroups.MO(facilityId),

//                    _ => null
//                };

//                if (groupName == null)
//                    continue;

//                await _hubContext.Clients
//                    .Group(groupName)
//                    .SendAsync(
//                        "ReceiveNotification",
//                        notification.Id,
//                        notification.Type.ToString(),
//                        caseRecord.DiseaseName,
//                        caseRecord.Facility?.FacilityName ?? "Unknown"
//                    );
//            }
//        }

//        // =====================================================
//        // NEW SURVEILLANCE ESCALATION
//        // =====================================================

//        public async Task SendSurveillanceNotification(
//            int caseRecordId,
//            NotificationType type)
//        {
//            await SendNotification(caseRecordId, type);
//        }

//        // =====================================================
//        // EXISTING METHODS
//        // =====================================================

//        public async Task MarkAsChecked(int id)
//        {
//            using var db = _dbFactory.CreateDbContext();

//            var n = await db.Notifications.FindAsync(id);

//            if (n != null)
//            {
//                n.IsChecked = true;

//                await db.SaveChangesAsync();
//            }
//        }

//        public async Task NotifyDashboardUpdate()
//        {
//            await _hubContext.Clients
//                .Group(NotificationGroups.Dashboard)
//                .SendAsync("DashboardUpdated");
//        }
//    }
//}







































//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using Surveillance.Application.Interfaces.Services;
//using Surveillance.Domain.Entities;
//using Surveillance.Domain.Enums;
//using Surveillance.Infrastructure.Data;
//using Surveillance.Infrastructure.Hubs;

//namespace Surveillance.Infrastructure.Services
//{
//    public class NotificationService : INotificationService
//    {
//        private readonly IHubContext<NotificationHub> _hubContext;
//        private readonly IDbContextFactory<AppDbContext> _dbFactory;

//        public NotificationService(
//            IHubContext<NotificationHub> hubContext,
//            IDbContextFactory<AppDbContext> dbFactory)
//        {
//            _hubContext = hubContext;
//            _dbFactory = dbFactory;
//        }

//        public async Task SendNotification(int caseRecordId, NotificationType type)
//        {
//            using var db = _dbFactory.CreateDbContext();

//            var caseRecord = await db.CaseRecords
//                .Include(c => c.Facility)
//                .FirstOrDefaultAsync(c => c.Id == caseRecordId);

//            if (caseRecord == null)
//                return;

//            // Prevent duplicate notifications
//            var exists = await db.Notifications.AnyAsync(n =>
//                n.CaseRecordId == caseRecordId &&
//                n.Type == type);

//            if (exists)
//                return;

//            // Create notification
//            var notification = new Notification
//            {
//                CaseRecordId = caseRecord.Id,
//                FacilityId = caseRecord.FacilityId,
//                DiseaseName = caseRecord.DiseaseName,
//                Type = type,
//                Timestamp = DateTime.UtcNow,
//                IsChecked = false
//            };

//            db.Notifications.Add(notification);
//            await db.SaveChangesAsync();

//            // Send via SignalR - convert enum to string
//            await _hubContext.Clients.Group("Analysts")
//                .SendAsync(
//                    "ReceiveNotification",
//                    notification.Id,
//                    notification.Type.ToString(),
//                    caseRecord.DiseaseName,
//                    caseRecord.Facility?.FacilityName ?? "Unknown"
//                );
//        }

//        public async Task MarkAsChecked(int id)
//        {
//            using var db = _dbFactory.CreateDbContext();

//            var n = await db.Notifications.FindAsync(id);
//            if (n != null)
//            {
//                n.IsChecked = true;
//                await db.SaveChangesAsync();
//            }
//        }

//        public async Task NotifyDashboardUpdate()
//        {
//            await _hubContext.Clients.Group("Dashboard")
//                .SendAsync("DashboardUpdated");
//        }
//    }
//}






































//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using Surveillance.Application.Interfaces.Services;
//using Surveillance.Domain.Entities;
//using Surveillance.Domain.Enums;
//using Surveillance.Infrastructure.Data;
//using Surveillance.Infrastructure.Hubs;


//namespace Surveillance.Infrastructure.Services
//{
//    public class NotificationService: INotificationService
//    {
//        private readonly IHubContext<NotificationHub> _hubContext;
//        private readonly IDbContextFactory<AppDbContext> _dbFactory;

//        public NotificationService(
//            IHubContext<NotificationHub> hubContext,
//            IDbContextFactory<AppDbContext> dbFactory)
//        {
//            _hubContext = hubContext;
//            _dbFactory = dbFactory;
//        }


//        public async Task SendNotification(int caseRecordId, NotificationType type)
//        {
//            using var db = _dbFactory.CreateDbContext();

//            var caseRecord = await db.CaseRecords
//                .Include(c => c.Facility)  
//                .FirstOrDefaultAsync(c => c.Id == caseRecordId);

//            if (caseRecord == null)
//                return;

//            // 🔒 Prevent duplicate notifications
//            var exists = await db.Notifications.AnyAsync(n =>
//                n.CaseRecordId == caseRecordId &&
//                n.Type == type
//            );

//            if (exists)
//                return;

//            //  Create notification
//            var notification = new Notification
//            {
//                CaseRecordId = caseRecord.Id,
//                FacilityId = caseRecord.FacilityId,   
//                DiseaseName = caseRecord.DiseaseName,
//                Type = type,                          
//                Timestamp = DateTime.UtcNow,          
//                IsChecked = false
//            };

//            db.Notifications.Add(notification);
//            await db.SaveChangesAsync();

//            //  Send via SignalR
//            await _hubContext.Clients.Group("Analysts")
//                .SendAsync(
//                    "ReceiveNotification",
//                    notification.Id,
//                    notification.Type,           
//                    caseRecord.DiseaseName,
//                    caseRecord.Facility?.FacilityName ?? "Unknown"
//                );
//        }

//        //  Mark as checked
//        public async Task MarkAsChecked(int id)
//        {
//            using var db = _dbFactory.CreateDbContext();

//            var n = await db.Notifications.FindAsync(id);
//            if (n != null)
//            {
//                n.IsChecked = true;
//                await db.SaveChangesAsync();
//            }
//        }

//        //  Dashboard refresh trigger
//        public async Task NotifyDashboardUpdate()
//        {
//            await _hubContext.Clients.Group("Dashboard")
//                .SendAsync("DashboardUpdated");
//        }
//    }
//}


























//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using UPHC.SurveillanceDashboard.Data;
//using UPHC.SurveillanceDashboard.Models;
//using UPHC.SurveillanceDashboard.Hubs;

//namespace UPHC.SurveillanceDashboard.Services
//{
//    public class NotificationService
//    {
//        private readonly IHubContext<NotificationHub> _hubContext;
//        private readonly IDbContextFactory<AppDbContext> _dbFactory;

//        public NotificationService(
//            IHubContext<NotificationHub> hubContext,
//            IDbContextFactory<AppDbContext> dbFactory)
//        {
//            _hubContext = hubContext;
//            _dbFactory = dbFactory;
//        }

//        // ✅ sending notification
//        public async Task SendNotification(int caseRecordId, string type)
//        {
//            using var db = _dbFactory.CreateDbContext();

//            var caseRecord = await db.CaseRecords
//                .Include(c => c.UPHC)
//                .FirstOrDefaultAsync(c => c.Id == caseRecordId);

//            if (caseRecord == null) return;

//            //  Save notification
//            var notification = new Notification
//            {
//                CaseRecordId = caseRecord.Id,
//                UPHCId = caseRecord.UPHCId,
//                DiseaseName = caseRecord.DiseaseName,
//                Type = type,
//                IsChecked = false
//            };

//            db.Notifications.Add(notification);
//            await db.SaveChangesAsync();

//            // SEND FULL DATA 
//            await _hubContext.Clients.Group("Analysts")
//                .SendAsync(
//                    "ReceiveNotification",
//                    notification.Id,                 // for marking read
//                    notification.Type,               // Suspected / Confirmed / Negative
//                    caseRecord.DiseaseName,          // Disease
//                    caseRecord.UPHC.Name             // UPHC
//                );
//        }

//        //Checked
//        public async Task MarkAsChecked(int id)
//        {
//            using var db = _dbFactory.CreateDbContext();

//            var n = await db.Notifications.FindAsync(id);
//            if (n != null)
//            {
//                n.IsChecked = true;
//                await db.SaveChangesAsync();
//            }
//        }




//        //  Dashboard refresh trigger
//        public async Task NotifyDashboardUpdate()
//        {
//            await _hubContext.Clients.Group("Dashboard")
//                .SendAsync("DashboardUpdated");
//        }







//    }
//}








































//    using Microsoft.AspNetCore.SignalR;
//    using Microsoft.EntityFrameworkCore;
//    using UPHC.SurveillanceDashboard.Data;
//    using UPHC.SurveillanceDashboard.Models;

//    using UPHC.SurveillanceDashboard.Hubs;


//namespace UPHC.SurveillanceDashboard.Services
//{




//    public class NotificationService
//    {
//        private readonly IHubContext<NotificationHub> _hubContext;
//        private readonly IDbContextFactory<AppDbContext> _dbFactory;

//        public NotificationService(IHubContext<NotificationHub> hubContext, IDbContextFactory<AppDbContext> dbFactory)
//        {
//            _hubContext = hubContext;
//            _dbFactory = dbFactory;
//        }



//        public async Task SendNotification(int caseRecordId, string type)        //<-----------static polymorphism
//        {
//            using var db = _dbFactory.CreateDbContext();

//            var caseRecord = await db.CaseRecords
//                .Include(c => c.UPHC)
//                .FirstOrDefaultAsync(c => c.Id == caseRecordId);

//            if (caseRecord == null) return;

//            var notification = new Notification
//            {
//                CaseRecordId = caseRecord.Id,
//                UPHCId = caseRecord.UPHCId,
//                DiseaseName = caseRecord.DiseaseName,
//                Type = type
//            };

//            db.Notifications.Add(notification);
//            await db.SaveChangesAsync();

//            //await _hubContext.Clients.Group("Analysts")
//            //    .SendAsync("ReceiveNotification",
//            //        caseRecord.UPHC.Name,
//            //        caseRecord.DiseaseName);
//            await _hubContext.Clients.Group("Analysts")
//    .SendAsync("ReceiveNotification", notification.Id);
//        }

//        public async Task MarkAsChecked(int id)
//        {
//            using var db = _dbFactory.CreateDbContext();

//            var n = await db.Notifications.FindAsync(id);
//            if (n != null)
//            {
//                n.IsChecked = true;
//                await db.SaveChangesAsync();
//            }
//        }
//    }


//}


