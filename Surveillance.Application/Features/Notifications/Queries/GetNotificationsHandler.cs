using MediatR;
using Surveillance.Application.DTOs;
using Surveillance.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.Features.Notifications.Queries
{
    public class GetNotificationsHandler : IRequestHandler<GetNotificationsQuery, List<NotificationDto>>
    {
        private readonly IRepository _repository;

        public GetNotificationsHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<NotificationDto>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _repository.GetAllNotificationsAsync();

            return notifications.Select(n => new NotificationDto
            {
                Id = n.Id,
                DiseaseName = n.DiseaseName,
                IsChecked = n.IsChecked,
                Timestamp = n.Timestamp,
                Type = n.Type.ToString(),
                CaseRecordId = n.CaseRecordId,
                FacilityId = n.FacilityId,
                FacilityName = n.FacilityName,
                LabResultId = n.LabResultId,
            }).ToList();
        }
    }
}