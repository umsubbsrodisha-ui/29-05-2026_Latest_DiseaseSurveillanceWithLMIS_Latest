

//One caution: because your Notification table has a unique index on CaseRecordId + Type, 
//SendFacilityNotification and then SendNotification may conflict if both insert NewCase. 
//If you get duplicate-key error, we’ll adjust service to reuse existing notification instead of inserting twice.


using MediatR;
using Surveillance.Application.Features.Dashboard.Notifications;
using Surveillance.Application.Interfaces.Repositories;
using Surveillance.Application.Interfaces.Services;
using Surveillance.Domain.Entities;
using Surveillance.Domain.Enums;

namespace Surveillance.Application.Features.Cases.Commands
{
    public class CreateCaseHandler : IRequestHandler<CreateCaseCommand, int>
    {
        private readonly IRepository _repository;
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;

        private static readonly List<string> OutbreakDiseases = new()
        {
            "Dengue", "Malaria", "Chikungunya", "Zika Virus Disease", "COVID-19",
            "Influenza (ILI/SARI)", "Tuberculosis", "Measles", "Rubella",
            "Varicella (Chickenpox)", "Mumps", "Diphtheria",
            "Pertussis (Whooping Cough)", "Acute Flaccid Paralysis (Polio)",
            "Meningitis", "AES/JE", "Cholera", "ADD", "Typhoid",
            "Hepatitis A & E", "Hepatitis B", "Hepatitis C", "Leptospirosis",
            "Scrub Typhus", "Rabies", "Tetanus", "Plague", "Anthrax",
            "Brucellosis", "Kala-azar", "Filariasis", "Leprosy", "HIV/AIDS", "HFMD"
        };

        public CreateCaseHandler(
            IRepository repository,
            INotificationService notificationService,
            IMediator mediator)
        {
            _repository = repository;
            _notificationService = notificationService;
            _mediator = mediator;
        }

        public async Task<int> Handle(
            CreateCaseCommand request,
            CancellationToken cancellationToken)
        {
            bool isCommunicable = OutbreakDiseases.Any(d =>
                d.Equals(request.DiseaseName, StringComparison.OrdinalIgnoreCase));

            var caseRecord = new CaseRecord
            {
                PatientName = request.PatientName,
                Phone = request.Phone,
                DiseaseName = request.DiseaseName,
                AddressOfPatient = request.AddressOfPatient,
                OnsetDate = DateTime.SpecifyKind(request.OnsetDate, DateTimeKind.Utc),
                DateReported = DateTime.SpecifyKind(request.DateReported, DateTimeKind.Utc),
                CreatedDate = DateTime.UtcNow,
                IsCommunicable = isCommunicable,
                Status = CaseStatus.Suspected,
                FacilityId = request.FacilityId,
                UserId = request.UserId
            };

            await _repository.AddCaseAsync(
                caseRecord,
                request.SymptomIds,
                request.SampleTypeIds,
                request.LabTestIds);

            if (caseRecord.IsCommunicable)
            {
                await _notificationService.SendFacilityNotification(
                    caseRecord.Id,
                    caseRecord.FacilityId,
                    NotificationType.NewCase,
                    "LT",
                    "MB",
                    "MO");

                await _notificationService.SendNotification(
                    caseRecord.Id,
                    NotificationType.NewCase);
            }

            await _mediator.Publish(
                new DashboardDataRefreshed(),
                cancellationToken);

            return caseRecord.Id;
        }
    }
}








































//using MediatR;
//using Surveillance.Application.Features.Dashboard.Notifications;
//using Surveillance.Application.Interfaces.Repositories;
//using Surveillance.Application.Interfaces.Services;
//using Surveillance.Domain.Entities;
//using Surveillance.Domain.Enums;

//namespace Surveillance.Application.Features.Cases.Commands
//{





//        public class CreateCaseHandler : IRequestHandler<CreateCaseCommand, int>
//        {
//            private readonly IRepository _repository;
//            private readonly INotificationService _notificationService;
//            private readonly IMediator _mediator;

//            private static readonly List<string> OutbreakDiseases = new()
//        {
//            "Dengue", "Malaria", "Chikungunya", "Zika Virus Disease", "COVID-19",
//            "Influenza (ILI/SARI)", "Tuberculosis", "Measles", "Rubella",
//            "Varicella (Chickenpox)", "Mumps", "Diphtheria",
//            "Pertussis (Whooping Cough)", "Acute Flaccid Paralysis (Polio)",
//            "Meningitis", "AES/JE", "Cholera", "ADD", "Typhoid",
//            "Hepatitis A & E", "Hepatitis B", "Hepatitis C", "Leptospirosis",
//            "Scrub Typhus", "Rabies", "Tetanus", "Plague", "Anthrax",
//            "Brucellosis", "Kala-azar", "Filariasis", "Leprosy", "HIV/AIDS", "HFMD"
//        };

//            public CreateCaseHandler(
//                IRepository repository,
//                INotificationService notificationService,
//                IMediator mediator)
//            {
//                _repository = repository;
//                _notificationService = notificationService;
//                _mediator = mediator;
//            }

//            public async Task<int> Handle(
//                CreateCaseCommand request,
//                CancellationToken cancellationToken)
//            {
//                bool isCommunicable = OutbreakDiseases.Any(d =>
//                    d.Equals(request.DiseaseName, StringComparison.OrdinalIgnoreCase));

//                var caseRecord = new CaseRecord
//                {
//                    PatientName = request.PatientName,
//                    Phone = request.Phone,
//                    DiseaseName = request.DiseaseName,
//                    AddressOfPatient = request.AddressOfPatient,
//                    OnsetDate = DateTime.SpecifyKind(request.OnsetDate, DateTimeKind.Utc),
//                    DateReported = DateTime.SpecifyKind(request.DateReported, DateTimeKind.Utc),
//                    CreatedDate = DateTime.UtcNow,
//                    IsCommunicable = isCommunicable,
//                    Status = CaseStatus.Suspected,
//                    FacilityId = request.FacilityId,
//                    UserId = request.UserId
//                };

//                await _repository.AddCaseAsync(
//                    caseRecord,
//                    request.SymptomIds,
//                    request.SampleTypeIds,
//                    request.LabTestIds);

//                if (caseRecord.IsCommunicable)
//                {

//                await _notificationService.SendFacilityNotification(
//    caseRecord.Id,
//    caseRecord.FacilityId,
//    NotificationType.NewCase,
//    "LT",
//    "MB",
//    "MO");
//                await _notificationService.SendNotification(
//                        caseRecord.Id,
//                        NotificationType.NewCase);
//                }

//                await _mediator.Publish(
//                    new DashboardDataRefreshed(),
//                    cancellationToken);

//                return caseRecord.Id;
//            }
//        }











//public class CreateCaseHandler : IRequestHandler<CreateCaseCommand, int>
//{
//    private readonly IRepository _repository;
//    private readonly INotificationService _notificationService;
//    private readonly IMediator _mediator;

//    private static readonly List<string> OutbreakDiseases = new()
//    {
//        "Dengue","Malaria","Chikungunya","Zika Virus Disease","COVID-19",
//        "Influenza (ILI/SARI)","Tuberculosis","Measles","Rubella",
//        "Varicella (Chickenpox)","Mumps","Diphtheria",
//        "Pertussis (Whooping Cough)","Acute Flaccid Paralysis (Polio)",
//        "Meningitis","AES/JE","Cholera","ADD","Typhoid",
//        "Hepatitis A & E","Hepatitis B","Hepatitis C","Leptospirosis",
//        "Scrub Typhus","Rabies","Tetanus","Plague","Anthrax","Brucellosis",
//        "Kala-azar","Filariasis","Leprosy","HIV/AIDS","HFMD"
//    };

//    public CreateCaseHandler(
//        IRepository repository,
//        INotificationService notificationService,
//        IMediator mediator)
//    {
//        _repository = repository;
//        _notificationService = notificationService;
//        _mediator = mediator;
//    }

//    public async Task<int> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
//    {
//        bool isCommunicable = OutbreakDiseases
//            .Any(d => d.Equals(request.DiseaseName, StringComparison.OrdinalIgnoreCase));

//        var caseRecord = new CaseRecord
//        {
//            PatientName = request.PatientName,
//            Phone = request.Phone,
//            DiseaseName = request.DiseaseName,
//            Symptoms = request.Symptoms,
//            AddressOfPatient = request.AddressOfPatient,
//            OnsetDate = DateTime.SpecifyKind(request.OnsetDate, DateTimeKind.Utc),
//            DateReported = DateTime.SpecifyKind(request.DateReported, DateTimeKind.Utc),
//            CreatedDate = DateTime.UtcNow,
//            IsCommunicable = isCommunicable,
//            Status = CaseStatus.Suspected,
//            FacilityId = request.FacilityId,
//            UserId = request.UserId
//        };

//        await _repository.AddCaseAsync(caseRecord);

//        if (caseRecord.IsCommunicable)
//        {
//            await _notificationService.SendNotification(
//                caseRecord.Id,
//                NotificationType.NewCase);
//        }

//        await _mediator.Publish(new DashboardDataRefreshed(), cancellationToken);

//        return caseRecord.Id;
//    }
//}
