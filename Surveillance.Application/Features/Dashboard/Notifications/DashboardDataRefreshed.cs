using MediatR;
using System;
using System.Collections.Generic;
using System.Text;


namespace Surveillance.Application.Features.Dashboard.Notifications
{
    public class DashboardDataRefreshed : INotification
    {
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
