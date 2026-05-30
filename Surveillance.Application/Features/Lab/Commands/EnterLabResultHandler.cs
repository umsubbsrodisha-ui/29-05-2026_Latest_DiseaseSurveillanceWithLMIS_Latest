using MediatR;
using Surveillance.Application.Interfaces.Repositories;
using Surveillance.Application.Interfaces.Services;
using Surveillance.Domain.Enums;

namespace Surveillance.Application.Features.Lab.Commands
{
    public class EnterLabResultHandler
        : IRequestHandler<EnterLabResultCommand, bool>
    {
        private readonly IRepository _repository;
        private readonly INotificationService _notificationService;

        public EnterLabResultHandler(
            IRepository repository,
            INotificationService notificationService)
        {
            _repository = repository;
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(
            EnterLabResultCommand request,
            CancellationToken cancellationToken)
        {
            var labResultId = await _repository.EnterLabResultAsync(
                request.CaseRecordLabTestId,
                request.ResultValue,
                request.ResultStatus,
                request.EnteredByUserId,
                request.IsVerified,
                request.VerifiedByUserId,
                request.Remarks,
                request.ReportPath);

            if (labResultId == null)
                return false;

            var info = await _repository.GetLabResultNotificationDetailsAsync(
                request.CaseRecordLabTestId);

            if (info == null)
                return true;

            if (!request.IsVerified)
            {
                return true;
            }

            var allTestsVerified =
                await _repository.AreAllLabTestsVerifiedForCaseAsync(
                    info.CaseRecordId);

            if (!allTestsVerified)
            {
                return true;
            }

            var hasAnyPositiveResult =
                await _repository.HasAnyPositiveLabResultForCaseAsync(
                    info.CaseRecordId);

            var finalNotificationType = hasAnyPositiveResult
                ? NotificationType.ConfirmedPositive
                : NotificationType.ConfirmedNegative;

            await _notificationService.SendFacilityLabResultNotification(
                info.CaseRecordId,
                info.FacilityId,
                NotificationType.LabResultApproved,
                labResultId.Value,
                "MO");

            await _notificationService.SendSurveillanceLabResultNotification(
                info.CaseRecordId,
                finalNotificationType,
                labResultId.Value);

            return true;
        }
    }
}






































//using MediatR;
//using Surveillance.Application.Interfaces.Repositories;
//using Surveillance.Application.Interfaces.Services;
//using Surveillance.Domain.Enums;

//namespace Surveillance.Application.Features.Lab.Commands
//{
//    public class EnterLabResultHandler
//        : IRequestHandler<EnterLabResultCommand, bool>
//    {
//        private readonly IRepository _repository;
//        private readonly INotificationService _notificationService;

//        public EnterLabResultHandler(
//            IRepository repository,
//            INotificationService notificationService)
//        {
//            _repository = repository;
//            _notificationService = notificationService;
//        }

//        public async Task<bool> Handle(
//            EnterLabResultCommand request,
//            CancellationToken cancellationToken)
//        {
//            var labResultId = await _repository.EnterLabResultAsync(
//                request.CaseRecordLabTestId,
//                request.ResultValue,
//                request.ResultStatus,
//                request.EnteredByUserId,
//                request.IsVerified,
//                request.VerifiedByUserId,
//                request.Remarks,
//                request.ReportPath);

//            if (labResultId == null)
//                return false;

//            var info = await _repository.GetLabResultNotificationDetailsAsync(
//                request.CaseRecordLabTestId);

//            if (info == null)
//                return true;

//            // ============================================
//            // If result is saved but NOT verified,
//            // notify only MO that result is pending review.
//            // No Admin / Analyst notification here.
//            // ============================================

//            if (!request.IsVerified)
//            {
//                await _notificationService.SendFacilityLabResultNotification(
//                    info.CaseRecordId,
//                    info.FacilityId,
//                    NotificationType.LabResultPendingApproval,
//                    labResultId.Value,
//                    "MO");

//                return true;
//            }

//            // ============================================
//            // Verified individual result saved.
//            // DO NOT notify Admin/Analyst per individual test.
//            // Wait until ALL lab tests of this case are verified.
//            // ============================================

//            var allTestsVerified =
//                await _repository.AreAllLabTestsVerifiedForCaseAsync(
//                    info.CaseRecordId);

//            if (!allTestsVerified)
//            {
//                // Optional: notify MO that one result was verified,
//                // but final case report is not ready yet.
//                await _notificationService.SendFacilityLabResultNotification(
//                    info.CaseRecordId,
//                    info.FacilityId,
//                    NotificationType.LabResultApproved,
//                    labResultId.Value,
//                    "MO");

//                return true;
//            }

//            // ============================================
//            // All tests are verified.
//            // Now decide final case result.
//            // If ANY verified test is Positive => case positive.
//            // Else confirmed negative.
//            // ============================================

//            var hasAnyPositiveResult =
//                await _repository.HasAnyPositiveLabResultForCaseAsync(
//                    info.CaseRecordId);

//            var finalNotificationType = hasAnyPositiveResult
//                ? NotificationType.ConfirmedPositive
//                : NotificationType.ConfirmedNegative;

//            // Notify MO of that facility that final report is ready.
//            await _notificationService.SendFacilityLabResultNotification(
//                info.CaseRecordId,
//                info.FacilityId,
//                NotificationType.LabResultApproved,
//                labResultId.Value,
//                "MO");

//            // Notify Admin / Analyst / Higher-ups only ONCE final report is ready.
//            await _notificationService.SendSurveillanceLabResultNotification(
//                info.CaseRecordId,
//                finalNotificationType,
//                labResultId.Value);

//            return true;
//        }
//    }
//}






































//using MediatR;
//using Surveillance.Application.Interfaces.Repositories;
//using Surveillance.Application.Interfaces.Services;
//using Surveillance.Domain.Enums;

//namespace Surveillance.Application.Features.Lab.Commands
//{


//     public class EnterLabResultHandler
//            : IRequestHandler<EnterLabResultCommand, bool>
//        {
//            private readonly IRepository _repository;
//            private readonly INotificationService _notificationService;

//            public EnterLabResultHandler(
//                IRepository repository,
//                INotificationService notificationService)
//            {
//                _repository = repository;
//                _notificationService = notificationService;
//            }

//            public async Task<bool> Handle(
//                EnterLabResultCommand request,
//                CancellationToken cancellationToken)
//            {
//                var labResultId = await _repository.EnterLabResultAsync(
//                    request.CaseRecordLabTestId,
//                    request.ResultValue,
//                    request.ResultStatus,
//                    request.EnteredByUserId,
//                    request.IsVerified,
//                    request.VerifiedByUserId,
//                    request.Remarks,
//                    request.ReportPath);

//                if (labResultId == null)
//                    return false;

//                var info = await _repository.GetLabResultNotificationDetailsAsync(
//                    request.CaseRecordLabTestId);

//                if (info == null)
//                    return true;

//                if (!request.IsVerified)
//                {
//                    await _notificationService.SendFacilityLabResultNotification(
//                        info.CaseRecordId,
//                        info.FacilityId,
//                        NotificationType.LabResultPendingApproval,
//                        labResultId.Value,
//                        "MO");

//                    return true;
//                }

//                if (request.ResultStatus == LabResultStatus.Positive)
//                {
//                    await _notificationService.SendFacilityLabResultNotification(
//                        info.CaseRecordId,
//                        info.FacilityId,
//                        NotificationType.LabResultApproved,
//                        labResultId.Value,
//                        "MO");

//                    await _notificationService.SendSurveillanceLabResultNotification(
//                        info.CaseRecordId,
//                        NotificationType.ConfirmedPositive,
//                        labResultId.Value);
//                }
//                else if (request.ResultStatus == LabResultStatus.Negative)
//                {
//                    await _notificationService.SendFacilityLabResultNotification(
//                        info.CaseRecordId,
//                        info.FacilityId,
//                        NotificationType.LabResultApproved,
//                        labResultId.Value,
//                        "MO");

//                    await _notificationService.SendSurveillanceLabResultNotification(
//                        info.CaseRecordId,
//                        NotificationType.ConfirmedNegative,
//                        labResultId.Value);
//                }

//                return true;
//            }
//        }
//    }
