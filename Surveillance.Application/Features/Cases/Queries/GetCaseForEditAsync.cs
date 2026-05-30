using MediatR;
using Surveillance.Application.DTOs;

namespace Surveillance.Application.Features.Cases.Queries
{
    public class GetCaseForEditQuery : IRequest<EditCaseDto?>
    {
        public int Id { get; set; }
    }
}