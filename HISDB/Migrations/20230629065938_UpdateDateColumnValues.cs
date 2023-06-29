using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HISDB.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateColumnValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023062910001001",
                column: "PaymentTime",
                value: null);

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023062920001002",
                column: "PaymentTime",
                value: null);

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023062910001001",
                column: "DrugDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023062920001002",
                column: "DrugDate",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023062910001001",
                column: "PaymentTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023062920001002",
                column: "PaymentTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023062910001001",
                column: "DrugDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE2023062920001002",
                column: "DrugDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
