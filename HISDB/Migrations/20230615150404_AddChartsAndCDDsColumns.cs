using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HISDB.Migrations
{
    /// <inheritdoc />
    public partial class AddChartsAndCDDsColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023061313001006");

            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA2023061313001007");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "Charts_Drugs_Dosages",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "History",
                table: "Charts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                column: "Quantity",
                value: 1.0);

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001002", "046404" },
                column: "Quantity",
                value: 1.0);

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001003", "046404" },
                column: "Quantity",
                value: 1.0);

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001004", "046404" },
                column: "Quantity",
                value: 1.0);

            migrationBuilder.UpdateData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DrugID" },
                keyValues: new object[] { "CHA2023053013001005", "046404" },
                column: "Quantity",
                value: 1.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Charts_Drugs_Dosages");

            migrationBuilder.DropColumn(
                name: "History",
                table: "Charts");

            migrationBuilder.InsertData(
                table: "Charts",
                columns: new[] { "ChaID", "DepartmentName", "Object", "Subject", "VDate" },
                values: new object[,]
                {
                    { "CHA2023061313001006", "心臟內科", "", "", new DateTime(2023, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CHA2023061313001007", "心臟內科", "", "", new DateTime(2023, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }
    }
}
