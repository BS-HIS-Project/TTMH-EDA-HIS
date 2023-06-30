using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HISDB.Migrations
{
    /// <inheritdoc />
    public partial class AddNewDetailsColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Diagnostic",
                table: "Details",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PartialPayment",
                table: "Details",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Charts",
                columns: new[] { "ChaID", "DepartmentName", "History", "Object", "Subject", "VDate" },
                values: new object[,]
                {
                    { "CHA2023063011001001", "心臟內科", "先天心臟病", "心臟病", "有些緩和了", new DateTime(2023, 6, 30, 11, 27, 16, 0, DateTimeKind.Unspecified) },
                    { "CHA2023063016001002", "心臟內科", "先天心臟病", "心臟病", "有些緩和了", new DateTime(2023, 6, 30, 16, 7, 30, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001001",
                columns: new[] { "Diagnostic", "PartialPayment" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001002",
                columns: new[] { "Diagnostic", "PartialPayment" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001003",
                columns: new[] { "Diagnostic", "PartialPayment" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001004",
                columns: new[] { "Diagnostic", "PartialPayment" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001005",
                columns: new[] { "Diagnostic", "PartialPayment" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023060613001001",
                columns: new[] { "Diagnostic", "PartialPayment" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023060613001002",
                columns: new[] { "Diagnostic", "PartialPayment" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023062910001001",
                columns: new[] { "Diagnostic", "PartialPayment" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023062920001002",
                columns: new[] { "Diagnostic", "PartialPayment" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "A118992634",
                column: "Status",
                value: "HealthInsurance");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "H255590997",
                column: "Status",
                value: "HealthInsurance");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "L198058112",
                column: "Status",
                value: "HealthInsurance");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "O101929955",
                column: "Status",
                value: "HealthInsurance");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "S257920071",
                column: "Status",
                value: "HealthInsurance");

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientID", "Address", "BirthDate", "Blood", "CaseHistory", "Gender", "Mobile", "NHICard", "PatientName", "Status" },
                values: new object[,]
                {
                    { "AC12345678", "台南市", new DateTime(1982, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "B", "PAT2018081113001001", "1", "0997224115", null, "鮭魚", "Japan" },
                    { "HD12345678", "新北市", new DateTime(2000, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "AB", "PAT2019081113001001", "2", "0912847214", null, "鯨魚", "Korea" }
                });

            migrationBuilder.InsertData(
                table: "Doctors_Patients_Charts",
                columns: new[] { "ChaID", "PatientID", "DoctorID" },
                values: new object[,]
                {
                    { "CHA2023063011001001", "AC12345678", "D11201002" },
                    { "CHA2023063016001002", "HD12345678", "D11201002" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023063011001001", "AC12345678" });

            migrationBuilder.DeleteData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023063016001002", "HD12345678" });

            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023063011001001");

            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023063016001002");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "AC12345678");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "HD12345678");

            migrationBuilder.DropColumn(
                name: "Diagnostic",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "PartialPayment",
                table: "Details");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "A118992634",
                column: "Status",
                value: "健保");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "H255590997",
                column: "Status",
                value: "健保");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "L198058112",
                column: "Status",
                value: "健保");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "O101929955",
                column: "Status",
                value: "健保");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "S257920071",
                column: "Status",
                value: "健保");
        }
    }
}
