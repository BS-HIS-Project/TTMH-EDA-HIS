using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HISDB.Migrations
{
    /// <inheritdoc />
    public partial class AddAndUpdateNewDateColumnValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DrugDate",
                table: "Prescriptions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentTime",
                table: "Details",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Charts",
                columns: new[] { "ChaID", "DepartmentName", "History", "Object", "Subject", "VDate" },
                values: new object[,]
                {
                    { "CHA2023062910001001", "心臟內科", "先天心臟病", "心臟病", "有些緩和了", new DateTime(2023, 6, 29, 10, 29, 14, 0, DateTimeKind.Unspecified) },
                    { "CHA2023062920001002", "心臟內科", "先天心臟病", "心臟病", "有些緩和了", new DateTime(2023, 6, 29, 20, 47, 16, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001001",
                column: "PaymentTime",
                value: new DateTime(2023, 5, 30, 9, 29, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001002",
                column: "PaymentTime",
                value: new DateTime(2023, 5, 30, 10, 2, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001003",
                column: "PaymentTime",
                value: new DateTime(2023, 5, 30, 15, 28, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001004",
                column: "PaymentTime",
                value: new DateTime(2023, 5, 30, 15, 50, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001005",
                column: "PaymentTime",
                value: new DateTime(2023, 5, 30, 19, 25, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023060613001001",
                column: "PaymentTime",
                value: new DateTime(2023, 6, 6, 19, 45, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023060613001002",
                column: "PaymentTime",
                value: new DateTime(2023, 6, 6, 10, 30, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "DetID", "CasID", "MedicalCost", "PatientID", "Payable", "PaymentTime", "Registration" },
                values: new object[,]
                {
                    { "DET2023062910001001", "C11201002", 500m, "O101929955", 650m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m },
                    { "DET2023062920001002", "C11201002", 500m, "S257920071", 650m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m }
                });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023053013001001",
                column: "DrugDate",
                value: new DateTime(2023, 5, 30, 9, 29, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023053013001002",
                column: "DrugDate",
                value: new DateTime(2023, 5, 30, 10, 2, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023053013001003",
                column: "DrugDate",
                value: new DateTime(2023, 5, 30, 15, 28, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023053013001004",
                column: "DrugDate",
                value: new DateTime(2023, 5, 30, 15, 50, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023053013001005",
                column: "DrugDate",
                value: new DateTime(2023, 5, 30, 19, 25, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023060613001001",
                column: "DrugDate",
                value: new DateTime(2023, 6, 6, 19, 45, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023060613001002",
                column: "DrugDate",
                value: new DateTime(2023, 6, 6, 10, 30, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "PresNo", "DrugDate", "PatientID", "PhaID" },
                values: new object[,]
                {
                    { "PRE2023062910001001", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "O101929955", "P11201002" },
                    { "PRE2023062920001002", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "S257920071", "P11201002" }
                });

            migrationBuilder.InsertData(
                table: "Charts_Drugs_Dosages",
                columns: new[] { "ChaID", "DrugID", "Days", "DosID", "Quantity", "Remark", "Total" },
                values: new object[,]
                {
                    { "CHA2023062910001001", "046404", 3, "Q6H", 1.0, "無", 9.0 },
                    { "CHA2023062920001002", "046404", 3, "Q6H", 1.0, "無", 9.0 }
                });

            migrationBuilder.InsertData(
                table: "Doctors_Patients_Charts",
                columns: new[] { "ChaID", "PatientID", "DoctorID" },
                values: new object[,]
                {
                    { "CHA2023062910001001", "O101929955", "D11201002" },
                    { "CHA2023062920001002", "S257920071", "D11201002" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023062910001001", "046404" });

            migrationBuilder.DeleteData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023062920001002", "046404" });

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023062910001001");

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023062920001002");

            migrationBuilder.DeleteData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023062910001001", "O101929955" });

            migrationBuilder.DeleteData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023062920001002", "S257920071" });

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023062910001001");

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023062920001002");

            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023062910001001");

            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023062920001002");

            migrationBuilder.DropColumn(
                name: "PaymentTime",
                table: "Details");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DrugDate",
                table: "Prescriptions",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023053013001001",
                column: "DrugDate",
                value: new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023053013001002",
                column: "DrugDate",
                value: new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023053013001003",
                column: "DrugDate",
                value: new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023053013001004",
                column: "DrugDate",
                value: new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023053013001005",
                column: "DrugDate",
                value: new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023060613001001",
                column: "DrugDate",
                value: new DateTime(2023, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023060613001002",
                column: "DrugDate",
                value: new DateTime(2023, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
