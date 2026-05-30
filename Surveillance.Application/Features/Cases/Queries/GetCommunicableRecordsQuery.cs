using MediatR;
using Surveillance.Application.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.Features.Cases.Queries
{
    public class GetCommunicableRecordsQuery: IRequest<List<CommunicableRecordDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

    }
}
