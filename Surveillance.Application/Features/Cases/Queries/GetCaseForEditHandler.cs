using MediatR;
using Surveillance.Application.DTOs;
using Surveillance.Application.Interfaces.Repositories;

namespace Surveillance.Application.Features.Cases.Queries
{
    public class GetCaseForEditHandler
        : IRequestHandler<GetCaseForEditQuery, EditCaseDto?>
    {
        private readonly IRepository _repository;

        public GetCaseForEditHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<EditCaseDto?> Handle(
            GetCaseForEditQuery request,
            CancellationToken cancellationToken)
        {
            var caseRecord = await _repository.GetCaseForEditAsync(request.Id);

            if (caseRecord == null)
                return null;

            return new EditCaseDto
            {
                Id = caseRecord.Id,
                PatientName = caseRecord.PatientName,
                Phone = caseRecord.Phone,
                DiseaseName = caseRecord.DiseaseName,
                AddressOfPatient = caseRecord.AddressOfPatient,
                OnsetDate = caseRecord.OnsetDate,
                DateReported = caseRecord.DateReported,
                Notes = caseRecord.Notes,
                CaseStatus = caseRecord.Status.ToString(),
                FacilityId = caseRecord.FacilityId,
                SelectedSymptomIds = caseRecord.SelectedSymptomIds,
                SelectedSampleTypeIds = caseRecord.SelectedSampleTypeIds,
                SelectedLabTestIds = caseRecord.SelectedLabTestIds
            };
        }
    }
}