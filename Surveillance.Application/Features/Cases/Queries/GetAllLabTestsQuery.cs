using MediatR;
using Surveillance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.Features.Cases.Queries
{
    public class GetAllLabTestsQuery : IRequest<List<LabTest>>
    {
    }
}
