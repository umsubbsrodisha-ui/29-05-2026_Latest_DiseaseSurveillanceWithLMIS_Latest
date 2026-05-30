using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Application.DTOs
{
    public class DashboardMetricDto
    {
        public string Title { get; set; } = string.Empty;

        public int Count { get; set; }
    }
}

//May be used for the following metrics in the dashboard:

//Total Cases
//Confirmed Cases
//Facilities Reporting
//Alerts
//Notifications
