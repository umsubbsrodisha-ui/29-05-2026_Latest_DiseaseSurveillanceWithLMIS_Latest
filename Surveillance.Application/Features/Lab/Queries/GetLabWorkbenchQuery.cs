using MediatR;
using Surveillance.Application.DTOs;

namespace Surveillance.Application.Features.Lab.Queries
{
    public class GetLabWorkbenchQuery : IRequest<List<LabWorkbenchDto>>
    {
        public int FacilityId { get; set; }
    }
}