using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UPHC.SurveillanceDashboard.Migrations
{
    /// <inheritdoc />
    public partial class InitialLMIS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsNotifiable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SampleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseLabTests",
                columns: table => new
                {
                    DiseaseId = table.Column<int>(type: "integer", nullable: false),
                    LabTestId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseLabTests", x => new { x.DiseaseId, x.LabTestId });
                    table.ForeignKey(
                        name: "FK_DiseaseLabTests_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiseaseLabTests_LabTests_LabTestId",
                        column: x => x.LabTestId,
                        principalTable: "LabTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabTestSampleTypes",
                columns: table => new
                {
                    LabTestId = table.Column<int>(type: "integer", nullable: false),
                    SampleTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTestSampleTypes", x => new { x.LabTestId, x.SampleTypeId });
                    table.ForeignKey(
                        name: "FK_LabTestSampleTypes_LabTests_LabTestId",
                        column: x => x.LabTestId,
                        principalTable: "LabTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabTestSampleTypes_SampleTypes_SampleTypeId",
                        column: x => x.SampleTypeId,
                        principalTable: "SampleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseRecordId = table.Column<int>(type: "integer", nullable: false),
                    SampleTypeId = table.Column<int>(type: "integer", nullable: false),
                    CollectedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Barcode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CollectedBy = table.Column<string>(type: "text", nullable: true),
                    CollectionNotes = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ProcessingFacilityId = table.Column<int>(type: "integer", nullable: true),
                    DispatchedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReceivedAtLabAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DispatchReferenceNo = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Samples_CaseRecords_CaseRecordId",
                        column: x => x.CaseRecordId,
                        principalTable: "CaseRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Samples_Facilities_ProcessingFacilityId",
                        column: x => x.ProcessingFacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Samples_SampleTypes_SampleTypeId",
                        column: x => x.SampleTypeId,
                        principalTable: "SampleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaseRecordSymptoms",
                columns: table => new
                {
                    CaseRecordId = table.Column<int>(type: "integer", nullable: false),
                    SymptomId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SymptomId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseRecordSymptoms", x => new { x.CaseRecordId, x.SymptomId });
                    table.ForeignKey(
                        name: "FK_CaseRecordSymptoms_CaseRecords_CaseRecordId",
                        column: x => x.CaseRecordId,
                        principalTable: "CaseRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseRecordSymptoms_Symptoms_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseRecordSymptoms_Symptoms_SymptomId1",
                        column: x => x.SymptomId1,
                        principalTable: "Symptoms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiseaseSymptoms",
                columns: table => new
                {
                    DiseaseId = table.Column<int>(type: "integer", nullable: false),
                    SymptomId = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseSymptoms", x => new { x.DiseaseId, x.SymptomId });
                    table.ForeignKey(
                        name: "FK_DiseaseSymptoms_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiseaseSymptoms_Symptoms_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "Symptoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseRecordLabTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseRecordId = table.Column<int>(type: "integer", nullable: false),
                    SampleId = table.Column<Guid>(type: "uuid", nullable: false),
                    LabTestId = table.Column<int>(type: "integer", nullable: false),
                    TestedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReportPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LabTestId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseRecordLabTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseRecordLabTests_CaseRecords_CaseRecordId",
                        column: x => x.CaseRecordId,
                        principalTable: "CaseRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseRecordLabTests_LabTests_LabTestId",
                        column: x => x.LabTestId,
                        principalTable: "LabTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaseRecordLabTests_LabTests_LabTestId1",
                        column: x => x.LabTestId1,
                        principalTable: "LabTests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CaseRecordLabTests_Samples_SampleId",
                        column: x => x.SampleId,
                        principalTable: "Samples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LabResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CaseRecordLabTestId = table.Column<Guid>(type: "uuid", nullable: false),
                    ResultValue = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ResultStatus = table.Column<int>(type: "integer", nullable: false),
                    EnteredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EnteredByUserId = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false),
                    VerifiedByUserId = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabResults_CaseRecordLabTests_CaseRecordLabTestId",
                        column: x => x.CaseRecordLabTestId,
                        principalTable: "CaseRecordLabTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Diseases",
                columns: new[] { "Id", "IsNotifiable", "Name" },
                values: new object[,]
                {
                    { 1, true, "Dengue" },
                    { 2, true, "Malaria" },
                    { 3, true, "Chikungunya" },
                    { 4, true, "COVID-19" },
                    { 5, true, "Influenza" },
                    { 6, true, "Tuberculosis" },
                    { 7, true, "Typhoid" },
                    { 8, true, "Cholera" },
                    { 9, true, "Measles" },
                    { 10, true, "Rubella" },
                    { 11, true, "Meningitis" },
                    { 12, true, "AES/JE" },
                    { 13, true, "Hepatitis B" },
                    { 14, true, "Hepatitis C" },
                    { 15, true, "Leptospirosis" },
                    { 16, true, "Scrub Typhus" },
                    { 17, true, "Rabies" },
                    { 18, true, "Kala-azar" },
                    { 19, true, "Filariasis" },
                    { 20, true, "Leprosy" },
                    { 21, true, "HIV/AIDS" }
                });

            migrationBuilder.InsertData(
                table: "LabTests",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "CBC" },
                    { 2, "Dengue NS1" },
                    { 3, "Dengue IgM" },
                    { 4, "Peripheral Smear" },
                    { 5, "Malaria Antigen" },
                    { 6, "RTPCR" },
                    { 7, "Rapid Antigen Test" },
                    { 8, "Widal Test" },
                    { 9, "Blood Culture" },
                    { 10, "Sputum AFB" },
                    { 11, "CBNAAT" },
                    { 12, "CSF Analysis" },
                    { 13, "ELISA" },
                    { 14, "LFT" },
                    { 15, "KFT" },
                    { 16, "Stool Culture" },
                    { 17, "Urine Routine" },
                    { 18, "Measles IgM" },
                    { 19, "Rubella IgM" },
                    { 20, "HIV ELISA" },
                    { 21, "Hepatitis B Surface Antigen" },
                    { 22, "Hepatitis C Antibody" },
                    { 23, "Leptospira IgM" },
                    { 24, "Scrub Typhus IgM" },
                    { 25, "JE IgM ELISA" }
                });

            migrationBuilder.InsertData(
                table: "SampleTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Blood" },
                    { 2, "Serum" },
                    { 3, "Urine" },
                    { 4, "Stool" },
                    { 5, "Swab" },
                    { 6, "CSF" },
                    { 7, "Sputum" },
                    { 8, "Skin Scraping" },
                    { 9, "Biopsy" }
                });

            migrationBuilder.InsertData(
                table: "Symptoms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Fever" },
                    { 2, null, "Headache" },
                    { 3, null, "Rash" },
                    { 4, null, "Vomiting" },
                    { 5, null, "Diarrhea" },
                    { 6, null, "Cough" },
                    { 7, null, "Breathlessness" },
                    { 8, null, "Body Pain" },
                    { 9, null, "Joint Pain" },
                    { 10, null, "Bleeding" },
                    { 11, null, "Jaundice" },
                    { 12, null, "Abdominal Pain" },
                    { 13, null, "Neck Rigidity" },
                    { 14, null, "Seizure" },
                    { 15, null, "Paralysis" },
                    { 16, null, "Weight Loss" },
                    { 17, null, "Night Sweats" },
                    { 18, null, "Lymph Node Swelling" },
                    { 19, null, "Skin Lesions" },
                    { 20, null, "Sore Throat" },
                    { 21, null, "Conjunctivitis" },
                    { 22, null, "Fatigue" },
                    { 23, null, "Loss of Appetite" },
                    { 24, null, "Chills" },
                    { 25, null, "Chest Pain" }
                });

            migrationBuilder.InsertData(
                table: "DiseaseLabTests",
                columns: new[] { "DiseaseId", "LabTestId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 4 },
                    { 2, 5 },
                    { 4, 6 },
                    { 4, 7 },
                    { 6, 10 },
                    { 6, 11 },
                    { 7, 8 },
                    { 7, 9 },
                    { 8, 16 },
                    { 9, 18 },
                    { 10, 19 },
                    { 21, 20 }
                });

            migrationBuilder.InsertData(
                table: "DiseaseSymptoms",
                columns: new[] { "DiseaseId", "SymptomId", "Weight" },
                values: new object[,]
                {
                    { 1, 1, 10 },
                    { 1, 2, 8 },
                    { 1, 8, 8 },
                    { 1, 10, 9 },
                    { 2, 1, 10 },
                    { 2, 22, 7 },
                    { 2, 24, 9 },
                    { 4, 1, 8 },
                    { 4, 6, 10 },
                    { 4, 7, 10 },
                    { 4, 22, 7 },
                    { 6, 6, 10 },
                    { 6, 16, 10 },
                    { 6, 17, 9 },
                    { 8, 4, 8 },
                    { 8, 5, 10 },
                    { 9, 1, 7 },
                    { 9, 3, 10 },
                    { 9, 6, 7 },
                    { 13, 11, 10 },
                    { 13, 12, 7 },
                    { 13, 22, 6 }
                });

            migrationBuilder.InsertData(
                table: "LabTestSampleTypes",
                columns: new[] { "LabTestId", "SampleTypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 5 },
                    { 7, 5 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 7 },
                    { 11, 7 },
                    { 12, 6 },
                    { 13, 2 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 4 },
                    { 17, 3 },
                    { 18, 2 },
                    { 19, 2 },
                    { 20, 2 },
                    { 21, 2 },
                    { 22, 2 },
                    { 23, 2 },
                    { 24, 2 },
                    { 25, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseRecordLabTests_CaseRecordId",
                table: "CaseRecordLabTests",
                column: "CaseRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseRecordLabTests_LabTestId",
                table: "CaseRecordLabTests",
                column: "LabTestId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseRecordLabTests_LabTestId1",
                table: "CaseRecordLabTests",
                column: "LabTestId1");

            migrationBuilder.CreateIndex(
                name: "IX_CaseRecordLabTests_SampleId",
                table: "CaseRecordLabTests",
                column: "SampleId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseRecordLabTests_TestedAt",
                table: "CaseRecordLabTests",
                column: "TestedAt");

            migrationBuilder.CreateIndex(
                name: "IX_CaseRecordSymptoms_SymptomId",
                table: "CaseRecordSymptoms",
                column: "SymptomId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseRecordSymptoms_SymptomId1",
                table: "CaseRecordSymptoms",
                column: "SymptomId1");

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseLabTests_LabTestId",
                table: "DiseaseLabTests",
                column: "LabTestId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_Name",
                table: "Diseases",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseSymptoms_SymptomId",
                table: "DiseaseSymptoms",
                column: "SymptomId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_CaseRecordLabTestId",
                table: "LabResults",
                column: "CaseRecordLabTestId");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_EnteredAt",
                table: "LabResults",
                column: "EnteredAt");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_IsVerified",
                table: "LabResults",
                column: "IsVerified");

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_ResultStatus",
                table: "LabResults",
                column: "ResultStatus");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_Name",
                table: "LabTests",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabTestSampleTypes_SampleTypeId",
                table: "LabTestSampleTypes",
                column: "SampleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Samples_Barcode",
                table: "Samples",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Samples_CaseRecordId",
                table: "Samples",
                column: "CaseRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Samples_CollectedAt",
                table: "Samples",
                column: "CollectedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Samples_ProcessingFacilityId",
                table: "Samples",
                column: "ProcessingFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Samples_SampleTypeId",
                table: "Samples",
                column: "SampleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Samples_Status",
                table: "Samples",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SampleTypes_Name",
                table: "SampleTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_Name",
                table: "Symptoms",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseRecordSymptoms");

            migrationBuilder.DropTable(
                name: "DiseaseLabTests");

            migrationBuilder.DropTable(
                name: "DiseaseSymptoms");

            migrationBuilder.DropTable(
                name: "LabResults");

            migrationBuilder.DropTable(
                name: "LabTestSampleTypes");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Symptoms");

            migrationBuilder.DropTable(
                name: "CaseRecordLabTests");

            migrationBuilder.DropTable(
                name: "LabTests");

            migrationBuilder.DropTable(
                name: "Samples");

            migrationBuilder.DropTable(
                name: "SampleTypes");
        }
    }
}
