using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HISDB.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnDrugPartialPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DrugPartialPayment",
                table: "Details",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001001",
                column: "DrugPartialPayment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001002",
                column: "DrugPartialPayment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001003",
                column: "DrugPartialPayment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001004",
                column: "DrugPartialPayment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023053013001005",
                column: "DrugPartialPayment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023060613001001",
                column: "DrugPartialPayment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023060613001002",
                column: "DrugPartialPayment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023062910001001",
                column: "DrugPartialPayment",
                value: null);

            migrationBuilder.UpdateData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET2023062920001002",
                column: "DrugPartialPayment",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrugPartialPayment",
                table: "Details");
        }
    }
}
