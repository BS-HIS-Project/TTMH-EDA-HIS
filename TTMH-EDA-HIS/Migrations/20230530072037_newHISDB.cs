using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTMH_EDA_HIS.Migrations
{
    /// <inheritdoc />
    public partial class newHISDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charts",
                columns: table => new
                {
                    ChaID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Object = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Charts__97A59C75CBFB1D4C", x => x.ChaID);
                });

            migrationBuilder.CreateTable(
                name: "Dosages",
                columns: table => new
                {
                    DosID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dosages__4DFE76ACB6EE094D", x => x.DosID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Account = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__7AD04FF1CB77EC3F", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    NHICard = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CaseHistory = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Gender = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    Blood = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patients__970EC346C8B25736", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "RoutesOfAdminstrations",
                columns: table => new
                {
                    ROAID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    BodyParts = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RoutesOf__84E1649C98E0E0F3", x => x.ROAID);
                });

            migrationBuilder.CreateTable(
                name: "Cashiers",
                columns: table => new
                {
                    CasID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cashiers__6B6EF2C78E8D5CE5", x => x.CasID);
                    table.ForeignKey(
                        name: "FK_Cashiers_Employees",
                        column: x => x.CasID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Doctors__2DC00EDF9BB9663C", x => x.DoctorID);
                    table.ForeignKey(
                        name: "FK_Doctors_Employees",
                        column: x => x.DoctorID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacists",
                columns: table => new
                {
                    PhaID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pharmaci__5D18076B51E8930B", x => x.PhaID);
                    table.ForeignKey(
                        name: "FK_Pharmacists_Employees",
                        column: x => x.PhaID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    DrugID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ATCCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    NHICode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    DrugName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    GenericName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DContent = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Appearance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicalUses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuggestedUsage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseReactions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Warning_Precautions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointOfHealthEducation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherInstructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessingMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ROAID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Drugs__908D66F6408F425E", x => x.DrugID);
                    table.ForeignKey(
                        name: "FK_Drugs_RoutesOfAdminstrations",
                        column: x => x.ROAID,
                        principalTable: "RoutesOfAdminstrations",
                        principalColumn: "ROAID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    DetID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Registration = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MedicalCost = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Payable = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CasID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    PatientID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Details__D8957AFCE2D73766", x => x.DetID);
                    table.ForeignKey(
                        name: "FK_Details_Cashiers",
                        column: x => x.CasID,
                        principalTable: "Cashiers",
                        principalColumn: "CasID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Details_Patients",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors_Patients_Charts",
                columns: table => new
                {
                    DoctorID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    PatientID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ChaID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Doctors___20274777D9BDA64F", x => new { x.DoctorID, x.PatientID, x.ChaID });
                    table.ForeignKey(
                        name: "FK_Doctors_Patients_Charts_Charts",
                        column: x => x.ChaID,
                        principalTable: "Charts",
                        principalColumn: "ChaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Patients_Charts_Doctors",
                        column: x => x.DoctorID,
                        principalTable: "Doctors",
                        principalColumn: "DoctorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Patients_Charts_Patients",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PresNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DrugDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PhaID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    PatientID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prescrip__1401F7AF47170057", x => x.PresNo);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Pharmacists",
                        column: x => x.PhaID,
                        principalTable: "Pharmacists",
                        principalColumn: "PhaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Charts_Drugs_Dosages",
                columns: table => new
                {
                    ChaID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DrugID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DosID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Charts_D__43E0B46C2967DE43", x => new { x.ChaID, x.DrugID, x.DosID });
                    table.ForeignKey(
                        name: "FK_Charts_Drugs_Dosages_Charts",
                        column: x => x.ChaID,
                        principalTable: "Charts",
                        principalColumn: "ChaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Charts_Drugs_Dosages_Dosages",
                        column: x => x.DosID,
                        principalTable: "Dosages",
                        principalColumn: "DosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Charts_Drugs_Dosages_Drugs",
                        column: x => x.DrugID,
                        principalTable: "Drugs",
                        principalColumn: "DrugID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Charts_Drugs_Dosages_DosID",
                table: "Charts_Drugs_Dosages",
                column: "DosID");

            migrationBuilder.CreateIndex(
                name: "IX_Charts_Drugs_Dosages_DrugID",
                table: "Charts_Drugs_Dosages",
                column: "DrugID");

            migrationBuilder.CreateIndex(
                name: "IX_Details_CasID",
                table: "Details",
                column: "CasID");

            migrationBuilder.CreateIndex(
                name: "IX_Details_PatientID",
                table: "Details",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Patients_Charts_ChaID",
                table: "Doctors_Patients_Charts",
                column: "ChaID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Patients_Charts_PatientID",
                table: "Doctors_Patients_Charts",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_ROAID",
                table: "Drugs",
                column: "ROAID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientID",
                table: "Prescriptions",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PhaID",
                table: "Prescriptions",
                column: "PhaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charts_Drugs_Dosages");

            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "Doctors_Patients_Charts");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Dosages");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "Cashiers");

            migrationBuilder.DropTable(
                name: "Charts");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Pharmacists");

            migrationBuilder.DropTable(
                name: "RoutesOfAdminstrations");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
