using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPHC.SurveillanceDashboard.Migrations
{
    /// <inheritdoc />
    public partial class AddPerformanceIndexes_new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CaseRecords_FacilityId",
                table: "CaseRecords");

            migrationBuilder.CreateIndex(
                name: "IX_CaseRecords_FacilityId_CreatedDate",
                table: "CaseRecords",
                columns: new[] { "FacilityId", "CreatedDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CaseRecords_FacilityId_CreatedDate",
                table: "CaseRecords");

            migrationBuilder.CreateIndex(
                name: "IX_CaseRecords_FacilityId",
                table: "CaseRecords",
                column: "FacilityId");
        }
    }
}
