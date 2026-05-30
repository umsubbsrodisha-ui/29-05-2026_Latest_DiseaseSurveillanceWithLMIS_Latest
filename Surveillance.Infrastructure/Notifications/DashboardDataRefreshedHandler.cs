using MediatR;
using Microsoft.AspNetCore.SignalR;
using Surveillance.Application.Features.Dashboard.Notifications;
using Surveillance.Infrastructure.Hubs;
using System;
using System.Collections.Generic;
using System.Text;



namespace Surveillance.Infrastructure.Notifications
{
    public class DashboardDataRefreshedHandler : INotificationHandler<DashboardDataRefreshed>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public DashboardDataRefreshedHandler(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(DashboardDataRefreshed notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.Group("Dashboard")
                .SendAsync("DashboardUpdated", cancellationToken);
        }
    }
}