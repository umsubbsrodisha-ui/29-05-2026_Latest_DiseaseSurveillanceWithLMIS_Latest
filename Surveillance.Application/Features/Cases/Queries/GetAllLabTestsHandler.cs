using MediatR;
using Surveillance.Application.Interfaces.Repositories;
using Surveillance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.Features.Cases.Queries
{
    public class GetAllLabTestsHandler
        : IRequestHandler<GetAllLabTestsQuery, List<LabTest>>
    {
        private readonly IRepository _repository;

        public GetAllLabTestsHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LabTest>> Handle(
            GetAllLabTestsQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetAllLabTestsAsync();
        }
    }
}
