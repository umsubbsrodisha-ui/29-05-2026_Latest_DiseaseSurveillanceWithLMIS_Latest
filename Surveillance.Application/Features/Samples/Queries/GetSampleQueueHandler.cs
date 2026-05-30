using MediatR;
using Surveillance.Application.DTOs;
using Surveillance.Application.Interfaces.Repositories;

namespace Surveillance.Application.Features.Samples.Queries
{
    public class GetSampleQueueHandler
        : IRequestHandler<GetSampleQueueQuery, List<SampleQueueDto>>
    {
        private readonly IRepository _repository;

        public GetSampleQueueHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SampleQueueDto>> Handle(
            GetSampleQueueQuery request,
            CancellationToken cancellationToken)
        {
            var samples = await _repository
                .GetSampleQueueByFacilityAsync(request.FacilityId);

            return samples.Select(s => new SampleQueueDto
            {
                SampleId = s.SampleId,
                CaseRecordId = s.CaseRecordId,
                PatientName = s.PatientName,
                Phone = s.Phone,
                DiseaseName = s.DiseaseName,
                FacilityId = s.FacilityId,
                FacilityName = s.FacilityName,
                SampleTypeId = s.SampleTypeId,
                SampleTypeName = s.SampleTypeName,
                SampleStatus = s.Status.ToString(),
                Barcode = s.Barcode,
                CollectedBy = s.CollectedBy,
                CollectionNotes = s.CollectionNotes,
                CollectedAt = s.CollectedAt,
                DispatchedAt = s.DispatchedAt,
                ReceivedAtLabAt = s.ReceivedAtLabAt,
                DispatchReferenceNo = s.DispatchReferenceNo,
                CreatedAt = s.CreatedAt
            }).ToList();
        }
    }
}