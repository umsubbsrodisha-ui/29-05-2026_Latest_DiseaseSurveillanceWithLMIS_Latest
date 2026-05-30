using System;
using System.Collections.Generic;
using System.Text;

using MediatR;
using Surveillance.Application.DTOs;

namespace Surveillance.Application.Features.Notifications.Queries
{
    public class GetNotificationsQuery : IRequest<List<NotificationDto>>
    {
    }
}
