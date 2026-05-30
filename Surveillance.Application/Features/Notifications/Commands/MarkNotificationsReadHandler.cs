using MediatR;
using Surveillance.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.Features.Notifications.Commands
{
    public class MarkNotificationsReadHandler : IRequestHandler<MarkNotificationsReadCommand>
    {
        private readonly IRepository _repository;

        public MarkNotificationsReadHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(MarkNotificationsReadCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.NotificationIds)
            {
                var notification = await _repository.GetNotificationByIdAsync(id);
                if (notification != null)
                {
                    notification.IsChecked = true;
                    await _repository.UpdateNotificationAsync(notification);
                }
            }
        }
    }
}
