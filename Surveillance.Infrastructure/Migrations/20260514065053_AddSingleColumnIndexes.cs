using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPHC.SurveillanceDashboard.Migrations
{
    /// <inheritdoc />
    public partial class AddSingleColumnIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CaseRecords_FacilityId",
                table: "CaseRecords",
                column: "FacilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CaseRecords_FacilityId",
                table: "CaseRecords");
        }
    }
}
