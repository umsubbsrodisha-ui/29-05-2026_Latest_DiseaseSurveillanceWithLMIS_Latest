using System;
using System.Collections.Generic;
using System.Text;

using MediatR;
using Surveillance.Domain.Enums;

namespace Surveillance.Application.Features.Cases.Commands
{
    public class UpdateCaseStatusCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public CaseStatus NewStatus { get; set; }
    }
}