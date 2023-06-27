using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HISDB.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientAndDPCsNewColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationTime",
                table: "Doctors_Patients_Charts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023053013001001", "A118992634" },
                column: "RegistrationTime",
                value: new DateTime(2023, 5, 30, 13, 20, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023053013001003", "H255590997" },
                column: "RegistrationTime",
                value: new DateTime(2023, 5, 30, 13, 37, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023053013001004", "L198058112" },
                column: "RegistrationTime",
                value: new DateTime(2023, 5, 30, 13, 43, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023060613001002", "L198058112" },
                column: "RegistrationTime",
                value: new DateTime(2023, 6, 6, 13, 56, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023053013001002", "O101929955" },
                column: "RegistrationTime",
                value: new DateTime(2023, 5, 30, 13, 25, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023060613001001", "O101929955" },
                column: "RegistrationTime",
                value: new DateTime(2023, 6, 6, 13, 18, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "PatientID" },
                keyValues: new object[] { "CHA2023053013001005", "S257920071" },
                column: "RegistrationTime",
                value: new DateTime(2023, 5, 30, 13, 50, 13, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "009554",
                column: "UnitPrice",
                value: 140m);

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "021757",
                column: "UnitPrice",
                value: 40m);

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "026173",
                column: "UnitPrice",
                value: 80m);

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "026672",
                column: "UnitPrice",
                value: 500m);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RegistrationTime",
                table: "Doctors_Patients_Charts");

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "009554",
                column: "UnitPrice",
                value: 240m);

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "021757",
                column: "UnitPrice",
                value: 240m);

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "026173",
                column: "UnitPrice",
                value: 240m);

            migrationBuilder.UpdateData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "026672",
                column: "UnitPrice",
                value: 240m);
        }
    }
}
