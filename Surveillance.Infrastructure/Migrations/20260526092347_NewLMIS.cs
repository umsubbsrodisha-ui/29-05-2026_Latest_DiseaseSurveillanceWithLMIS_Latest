using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPHC.SurveillanceDashboard.Migrations
{
    /// <inheritdoc />
    public partial class NewLMIS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseRecordLabTests_LabTests_LabTestId1",
                table: "CaseRecordLabTests");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseRecordLabTests_Samples_SampleId",
                table: "CaseRecordLabTests");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseRecordSymptoms_Symptoms_SymptomId1",
                table: "CaseRecordSymptoms");

            migrationBuilder.DropIndex(
                name: "IX_CaseRecordSymptoms_SymptomId1",
                table: "CaseRecordSymptoms");

            migrationBuilder.DropIndex(
                name: "IX_CaseRecordLabTests_LabTestId1",
                table: "CaseRecordLabTests");

            migrationBuilder.DropColumn(
                name: "SymptomId1",
                table: "CaseRecordSymptoms");

            migrationBuilder.DropColumn(
                name: "Symptoms",
                table: "CaseRecords");

            migrationBuilder.DropColumn(
                name: "LabTestId1",
                table: "CaseRecordLabTests");

            migrationBuilder.AddColumn<Guid>(
                name: "LabResultId",
                table: "Notifications",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "LabTests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "LabTests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpectedTurnaroundHours",
                table: "LabTests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LabTests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresVerification",
                table: "LabTests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ShortCode",
                table: "LabTests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportLink",
                table: "LabResults",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClinicalNotes",
                table: "CaseRecords",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinalReportPath",
                table: "CaseRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReportExpiryDate",
                table: "CaseRecords",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SymptomsSummary",
                table: "CaseRecords",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Complete Blood Count", 24, true, true, "CBC" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dengue NS1 Antigen Test", 24, true, true, "DNS1" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Dengue IgM Antibody Test", 48, true, true, "DIGM" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Peripheral Blood Smear", 24, true, true, "PS" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Malaria Rapid Antigen Test", 24, true, true, "MAG" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Real-Time PCR Test", 48, true, true, "RTPCR" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rapid Antigen Test", 24, true, true, "RAT" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Widal Test for Typhoid", 24, true, true, "WIDAL" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Blood Culture Test", 72, true, true, "BCULT" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sputum Acid Fast Bacilli Test", 48, true, true, "AFB" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cartridge Based Nucleic Acid Amplification Test", 48, true, true, "CBNAAT" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cerebrospinal Fluid Analysis", 24, true, true, "CSF" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Enzyme-Linked Immunosorbent Assay", 48, true, true, "ELISA" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Liver Function Test", 24, true, true, "LFT" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kidney Function Test", 24, true, true, "KFT" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Stool Culture Test", 72, true, true, "SCULT" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Urine Routine Examination", 24, true, true, "URINE" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Measles IgM Antibody Test", 48, true, true, "MIGM" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rubella IgM Antibody Test", 48, true, true, "RIGM" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "HIV ELISA Test", 48, true, true, "HIV" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "HBsAg Test", 48, true, true, "HBSAG" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "HCV Antibody Test", 48, true, true, "HCV" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Leptospira IgM Antibody Test", 48, true, true, "LEPIGM" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Scrub Typhus IgM Antibody Test", 48, true, true, "STIGM" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "Description", "ExpectedTurnaroundHours", "IsActive", "RequiresVerification", "ShortCode" },
                values: new object[] { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Japanese Encephalitis IgM ELISA", 48, true, true, "JEIGM" });

            migrationBuilder.AddForeignKey(
                name: "FK_CaseRecordLabTests_Samples_SampleId",
                table: "CaseRecordLabTests",
                column: "SampleId",
                principalTable: "Samples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseRecordLabTests_Samples_SampleId",
                table: "CaseRecordLabTests");

            migrationBuilder.DropColumn(
                name: "LabResultId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "ExpectedTurnaroundHours",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "RequiresVerification",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "ShortCode",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "ReportLink",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "ClinicalNotes",
                table: "CaseRecords");

            migrationBuilder.DropColumn(
                name: "FinalReportPath",
                table: "CaseRecords");

            migrationBuilder.DropColumn(
                name: "ReportExpiryDate",
                table: "CaseRecords");

            migrationBuilder.DropColumn(
                name: "SymptomsSummary",
                table: "CaseRecords");

            migrationBuilder.AddColumn<int>(
                name: "SymptomId1",
                table: "CaseRecordSymptoms",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Symptoms",
                table: "CaseRecords",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LabTestId1",
                table: "CaseRecordLabTests",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseRecordSymptoms_SymptomId1",
                table: "CaseRecordSymptoms",
                column: "SymptomId1");

            migrationBuilder.CreateIndex(
                name: "IX_CaseRecordLabTests_LabTestId1",
                table: "CaseRecordLabTests",
                column: "LabTestId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseRecordLabTests_LabTests_LabTestId1",
                table: "CaseRecordLabTests",
                column: "LabTestId1",
                principalTable: "LabTests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseRecordLabTests_Samples_SampleId",
                table: "CaseRecordLabTests",
                column: "SampleId",
                principalTable: "Samples",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CaseRecordSymptoms_Symptoms_SymptomId1",
                table: "CaseRecordSymptoms",
                column: "SymptomId1",
                principalTable: "Symptoms",
                principalColumn: "Id");
        }
    }
}
