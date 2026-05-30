
using Microsoft.AspNetCore.SignalR;

namespace Surveillance.Infrastructure.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task JoinAnalystGroup()
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                NotificationGroups.Analysts);
        }

        public async Task JoinDashboardGroup()
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                NotificationGroups.Dashboard);
        }

        public async Task JoinLTGroup(int facilityId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                NotificationGroups.LT(facilityId));
        }

        public async Task JoinMBGroup(int facilityId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                NotificationGroups.MB(facilityId));
        }

        public async Task JoinMOGroup(int facilityId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                NotificationGroups.MO(facilityId));
        }
    }
}



//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using System.Text.RegularExpressions;
//using UPHC.SurveillanceDashboard.Models;

//namespace Surveillance.Infrastructure.Hubs
//{
//    public class NotificationHub : Hub 
//    { 

//        // analysts will use this
//        public async Task JoinAnalystGroup() 
//        {
//             await Groups.AddToGroupAsync(Context.ConnectionId, "Analysts"); 

//        }


//        //Adding this for Dashboard auto update..
//        public async Task JoinDashboardGroup()
//        {
//            await Groups.AddToGroupAsync(Context.ConnectionId, "Dashboard");
//        }
//    }
//}