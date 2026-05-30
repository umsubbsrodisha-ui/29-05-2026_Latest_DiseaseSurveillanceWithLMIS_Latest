using MediatR;
using Surveillance.Application.Interfaces.Repositories;

namespace Surveillance.Application.Features.Cases.Commands
{
    public class UpdateCaseHandler
        : IRequestHandler<UpdateCaseCommand, bool>
    {
        private readonly IRepository _repository;

        public UpdateCaseHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateCaseCommand request,CancellationToken cancellationToken)
        {
            return await _repository.UpdateCaseDetailsAsync(
                request.Id,
                request.PatientName,
                request.Phone,
                request.DiseaseName,
                request.AddressOfPatient,
                DateTime.SpecifyKind(request.OnsetDate, DateTimeKind.Utc),
                DateTime.SpecifyKind(request.DateReported, DateTimeKind.Utc),
                request.Notes,
                request.SymptomIds,
                request.SampleTypeIds,
                request.LabTestIds);
        }
    }
}