using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.DTOs
{
    public class TrendPointDto
    {
        public string Label { get; set; } = string.Empty;

        public int ConfirmedCount { get; set; }

        public int SuspectedCount { get; set; }
    }
}
