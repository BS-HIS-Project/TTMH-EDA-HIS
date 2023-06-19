using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HISDB.Migrations
{
    /// <inheritdoc />
    public partial class altercolumTotalinChartsDrugsDosage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "Charts_Drugs_Dosages",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "History",
                table: "Charts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023053013001001",
                column: "History",
                value: "先天心臟病");

            migrationBuilder.UpdateData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023053013001002",
                column: "History",
                value: "先天心臟病");

            migrationBuilder.UpdateData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023053013001003",
                column: "History",
                value: "先天心臟病");

            migrationBuilder.UpdateData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023053013001004",
                column: "History",
                value: "先天心臟病");

            migrationBuilder.UpdateData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023053013001005",
                column: "History",
                value: "先天心臟病");

            migrationBuilder.InsertData(
                table: "Charts",
                columns: new[] { "ChaID", "DepartmentName", "History", "Object", "Subject", "VDate" },
                values: new object[,]
                {
                    { "CHA2023060613001001", "心臟內科", "先天心臟病", "心臟病", "吃完藥會頭暈", new DateTime(2023, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CHA2023060613001002", "心臟內科", "先天心臟病", "心臟病", "有些緩和了", new DateTime(2023, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001001", "046404" },
                column: "Total",
                value: 9.0);

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001002", "046404" },
                column: "Total",
                value: 9.0);

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001003", "046404" },
                column: "Total",
                value: 9.0);

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001004", "046404" },
                column: "Total",
                value: 9.0);

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001005", "046404" },
                column: "Total",
                value: 9.0);

            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "DetID", "CasID", "MedicalCost", "PatientID", "Payable", "Registration" },
                values: new object[,]
                {
                    { "DET2023060613001001", "C11201002", 500m, "O101929955", 650m, 150m },
                    { "DET2023060613001002", "C11201002", 500m, "L198058112", 650m, 150m }
                });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "PresNo", "DrugDate", "PatientID", "PhaID" },
                values: new object[,]
                {
                    { "PRE2023060613001001", new DateTime(2023, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "O101929955", "P11201002" },
                    { "PRE2023060613001002", new DateTime(2023, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "L198058112", "P11201002" }
                });

            migrationBuilder.InsertData(
                table: "Charts_Drugs_Dosages",
                columns: new[] { "ChaID", "DrugID", "Days", "DosID", "Quantity", "Remark", "Total" },
                values: new object[,]
                {
                    { "CHA2023060613001001", "046404", 3, "BID", 1.0, "無", 9.0 },
                    { "CHA2023060613001002", "046404", 3, "Q6H", 1.0, "無", 9.0 }
                });

            migrationBuilder.InsertData(
                table: "Doctors_Patients_Charts",
                columns: new[] { "ChaID", "PatientID", "DoctorID" },
                values: new object[,]
                {
                    { "CHA2023060613001002", "L198058112", "D11201001" },
                    { "CHA2023060613001001", "O101929955", "D11201001" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023060613001001", "046404" });

            migrationBuilder.DeleteData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023060613001002", "046404" });

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023060613001001");

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023060613001002");

            migrationBuilder.DeleteData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023060613001002", "L198058112" });

            migrationBuilder.DeleteData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023060613001001", "O101929955" });

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023060613001001");

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023060613001002");

            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023060613001001");

            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023060613001002");

            migrationBuilder.AlterColumn<int>(
                name: "Total",
                table: "Charts_Drugs_Dosages",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "History",
                table: "Charts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023053013001001",
                column: "History",
                value: new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023053013001002",
                column: "History",
                value: new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023053013001003",
                column: "History",
                value: new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023053013001004",
                column: "History",
                value: new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023053013001005",
                column: "History",
                value: new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001001", "046404" },
                column: "Total",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001002", "046404" },
                column: "Total",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001003", "046404" },
                column: "Total",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001004", "046404" },
                column: "Total",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001005", "046404" },
                column: "Total",
                value: 9);
        }
    }
}
