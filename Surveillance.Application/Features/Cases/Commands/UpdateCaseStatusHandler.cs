using MediatR;
using Surveillance.Application.Features.Dashboard.Notifications;
using Surveillance.Application.Interfaces.Repositories;
using Surveillance.Application.Interfaces.Services;
using Surveillance.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.Features.Cases.Commands
{
    public class UpdateCaseStatusHandler : IRequestHandler<UpdateCaseStatusCommand, bool>
    {
        private readonly IRepository _repository;
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;

        public UpdateCaseStatusHandler(
            IRepository repository,
            INotificationService notificationService,
            IMediator mediator)
        {
            _repository = repository;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task<bool> Handle(UpdateCaseStatusCommand request, CancellationToken cancellationToken)
        {
            var caseRecord = await _repository.GetCaseById_ForUpdateAsync(request.Id);

            if (caseRecord == null)
                return false;

            // Only update if status changed
            if (caseRecord.Status == request.NewStatus)
                return false;

            caseRecord.Status = request.NewStatus;

            // Set LabConfirmedDate if Confirmed or Negative
            if (request.NewStatus == CaseStatus.Confirmed || request.NewStatus == CaseStatus.Negative)
            {
                caseRecord.LabConfirmedDate = DateTime.UtcNow;
            }

            await _repository.UpdateCaseAsync(caseRecord);

            // Send notification if Confirmed or Negative
            if (request.NewStatus == CaseStatus.Confirmed)
            {
                await _notificationService.SendNotification(
                    caseRecord.Id,
                    NotificationType.ConfirmedPositive);
            }
            else if (request.NewStatus == CaseStatus.Negative)
            {
                await _notificationService.SendNotification(
                    caseRecord.Id,
                    NotificationType.ConfirmedNegative);
            }

            // Trigger dashboard refresh via MediatR
            await _mediator.Publish(new DashboardDataRefreshed(), cancellationToken);

            return true;
        }
    }
}
