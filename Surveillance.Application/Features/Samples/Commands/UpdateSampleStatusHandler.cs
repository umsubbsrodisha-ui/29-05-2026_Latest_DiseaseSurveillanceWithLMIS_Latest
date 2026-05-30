using MediatR;
using Surveillance.Application.Interfaces.Repositories;
using Surveillance.Application.Interfaces.Services;
using Surveillance.Domain.Enums;

namespace Surveillance.Application.Features.Samples.Commands
{
    public class UpdateSampleStatusHandler
        : IRequestHandler<UpdateSampleStatusCommand, bool>
    {
        private readonly IRepository _repository;
        private readonly INotificationService _notificationService;

        public UpdateSampleStatusHandler(
            IRepository repository,
            INotificationService notificationService)
        {
            _repository = repository;
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(
            UpdateSampleStatusCommand request,
            CancellationToken cancellationToken)
        {
            var updated = await _repository.UpdateSampleStatusAsync(
                request.SampleId,
                request.NewStatus,
                request.Barcode,
                request.CollectedBy,
                request.CollectionNotes,
                request.DispatchReferenceNo);

            if (!updated)
                return false;

            var info = await _repository.GetSampleNotificationDetailsAsync(
                request.SampleId);

            if (info == null)
                return true;

            if (request.NewStatus == SampleStatus.Collected)
            {
                await _notificationService.SendFacilityNotification(
                    info.CaseRecordId,
                    info.FacilityId,
                    NotificationType.SampleCollected,
                    "MB");
                   
            }
            else if (request.NewStatus == SampleStatus.Dispatched)
            {
                await _notificationService.SendFacilityNotification(
                    info.CaseRecordId,
                    info.FacilityId,
                    NotificationType.SampleDispatched,
                    "MB");
            }
            else if (request.NewStatus == SampleStatus.Received)
            {
                await _notificationService.SendFacilityNotification(
                    info.CaseRecordId,
                    info.FacilityId,
                    NotificationType.SampleReceivedAtLab,
                    "MO");
            }

            else if (request.NewStatus == SampleStatus.Processing)
            {
                await _notificationService.SendFacilityNotification(
                    info.CaseRecordId,
                    info.FacilityId,
                    NotificationType.SampleReceivedAtLab,
                    "MO");
            }
            else if (request.NewStatus == SampleStatus.Rejected)
            {
                await _notificationService.SendFacilityNotification(
                    info.CaseRecordId,
                    info.FacilityId,
                    NotificationType.SampleRejected,
                    "MB",
                    "MO");
            }

            return true;
        }
    }
}






































//using MediatR;
//using Surveillance.Application.Interfaces.Repositories;

//namespace Surveillance.Application.Features.Samples.Commands
//{
//    public class UpdateSampleStatusHandler
//        : IRequestHandler<UpdateSampleStatusCommand, bool>
//    {
//        private readonly IRepository _repository;

//        public UpdateSampleStatusHandler(IRepository repository)
//        {
//            _repository = repository;
//        }

//        public async Task<bool> Handle(
//            UpdateSampleStatusCommand request,
//            CancellationToken cancellationToken)
//        {
//            return await _repository.UpdateSampleStatusAsync(
//                request.SampleId,
//                request.NewStatus,
//                request.Barcode,
//                request.CollectedBy,
//                request.CollectionNotes,
//                request.DispatchReferenceNo);
//        }
//    }
//}