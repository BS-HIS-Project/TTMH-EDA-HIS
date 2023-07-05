using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HISDB.Migrations
{
    /// <inheritdoc />
    public partial class HashPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Employees",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "C11201001",
                column: "Password",
                value: "55ACA56A258F8CA90B01594BC4C613B5F5B44F5FB6DAAF5C14DF842D851D2E78324FC4BD1883B821E0B5C21358DFBCDBB30905E80239EDE900B9964934199862");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "C11201002",
                column: "Password",
                value: "42F4197EBEF050AD15E15195C53663FCE72C070E069F98F98743D362479618BA5BFB5E10EB72C31DC74AAC003553490D51A6669605520016C81CDD483DA1CD92");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "D11201001",
                column: "Password",
                value: "C7E21D81B12CE45023ED244C4E3C8DCF8F15A3A3C12A94E04B5F139A67AEC9A0534FC5A1387A5B5C63956752B6677FCF98B524FA378277BCA2241FC7E7807FA0");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "D11201002",
                column: "Password",
                value: "894D9AE1387F7FAE80C852727BF4C122E906BC84A00E9F7AA098F81A5114687DE6A1E746A7769B5EA0C5FFF3E14C4D27A562CC2591B8FD811050F6993535F472");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "P11201001",
                column: "Password",
                value: "C620C100EE6E040376B0367514B74660499ECBAB0EFB18DBCA49DFD6A1DC19A41FD6F14AE500F9607BF0F0428412AD18F704B4CEE74B636E42272F6E32CCF449");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "P11201002",
                column: "Password",
                value: "FB8D1C53588E7E238EC43CEDBFC1387FB8CEF974C3ABE50D10E081E4E8B8D9C214FBD54DDD204CDA87E93D309201B4558DF15FF363970E1AFCD8154571FFF97C");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Employees",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldUnicode: false,
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "C11201001",
                column: "Password",
                value: "C1004");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "C11201002",
                column: "Password",
                value: "C1006");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "D11201001",
                column: "Password",
                value: "D1001");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "D11201002",
                column: "Password",
                value: "D1003");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "P11201001",
                column: "Password",
                value: "P1002");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "P11201002",
                column: "Password",
                value: "P1005");
        }
    }
}
