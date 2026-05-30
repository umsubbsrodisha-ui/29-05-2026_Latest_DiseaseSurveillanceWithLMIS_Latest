using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPHC.SurveillanceDashboard.Migrations
{
    /// <inheritdoc />
    public partial class AddPerformanceIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notifications_FacilityId",
                table: "Notifications");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FacilityId_Timestamp",
                table: "Notifications",
                columns: new[] { "FacilityId", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_CaseRecords_IsCommunicable_OnsetDate",
                table: "CaseRecords",
                columns: new[] { "IsCommunicable", "OnsetDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notifications_FacilityId_Timestamp",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_CaseRecords_IsCommunicable_OnsetDate",
                table: "CaseRecords");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FacilityId",
                table: "Notifications",
                column: "FacilityId");
        }
    }
}
