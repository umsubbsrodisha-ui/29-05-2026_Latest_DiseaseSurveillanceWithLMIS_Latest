using System;
using System.Collections.Generic;
using System.Text;

using MediatR;

namespace Surveillance.Application.Features.Notifications.Commands
{
    public class MarkNotificationsReadCommand : IRequest
    {
        public List<int> NotificationIds { get; set; } = new();
    }
}
