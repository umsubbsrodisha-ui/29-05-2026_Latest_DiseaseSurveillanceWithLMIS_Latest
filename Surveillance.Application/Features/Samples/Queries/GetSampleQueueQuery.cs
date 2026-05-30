using MediatR;
using Surveillance.Application.DTOs;

namespace Surveillance.Application.Features.Samples.Queries
{
    public class GetSampleQueueQuery : IRequest<List<SampleQueueDto>>
    {
        public int FacilityId { get; set; }
    }
}