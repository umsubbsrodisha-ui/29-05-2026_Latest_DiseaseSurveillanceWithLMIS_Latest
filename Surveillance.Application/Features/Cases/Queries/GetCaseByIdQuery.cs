using MediatR;
using Surveillance.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;



namespace Surveillance.Application.Features.Cases.Queries
{
    public class GetCaseByIdQuery : IRequest<CaseDetailsDto?>
    {
        public int Id { get; set; }
    }
}