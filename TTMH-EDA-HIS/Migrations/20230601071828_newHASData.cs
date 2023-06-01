using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TTMH_EDA_HIS.Migrations
{
    /// <inheritdoc />
    public partial class newHASData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Charts",
                columns: new[] { "ChaID", "DepartmentName", "Object", "Subject", "VDate" },
                values: new object[,]
                {
                    { "CHA20230530001001", "心臟內科", "心臟病", "胸悶", new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CHA20230530001002", "心臟內科", "心臟病", "心跳好像一直不規律，呼吸不過來", new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CHA20230530001003", "心臟內科", "心臟病", "頭痛、噁心、冒冷汗", new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CHA20230530001004", "心臟內科", "心臟病", "呼吸急促、胸悶", new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "CHA20230530001005", "心臟內科", "心臟病", "心臟很痛", new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Dosages",
                columns: new[] { "DosID", "Direction" },
                values: new object[,]
                {
                    { "BIAH", "每天早,晚(飯前)及睡前1次" },
                    { "BIDP", "需要時早,晚(飯後)1次" },
                    { "ORDER", "依照醫師指示" },
                    { "PRN", "需要時使用" },
                    { "QN", "每晚1次" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "Account", "EmployeeName", "Password" },
                values: new object[,]
                {
                    { "C11201001", "C1004", "盧昱達", "C1004" },
                    { "C11201002", "C1006", "Althea", "C1006" },
                    { "D11201001", "D1001", "YuDaLu", "D1001" },
                    { "D11201002", "D1003", "連智健", "D1003" },
                    { "P11201001", "P1002", "鍾伊惠", "P1002" },
                    { "P11201002", "P1005", "林廣學", "P1005" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientID", "Address", "BirthDate", "Blood", "CaseHistory", "Gender", "Mobile", "NHICard", "PatientName" },
                values: new object[,]
                {
                    { "A118992634", "台北市", new DateTime(1999, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", "PAT20160610001001", "1", "0912345678", "000012345678", "水戶黃門" },
                    { "H255590997", "桃園市", new DateTime(1997, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "AB", "PAT20161225001003", "2", "0955664477", "123456789011", "晨曦" },
                    { "L198058112", "台中市", new DateTime(1992, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "O", "PAT20170526001004", "1", "0964973125", "647519785134", "野原新之助" },
                    { "O101929955", "新竹市", new DateTime(1997, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "A", "PAT20160610001002", "1", "0965478932", "080009699912", "海綿寶寶" },
                    { "S257920071", "高雄市", new DateTime(2000, 2, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "O", "PAT20170811001005", "2", "0997919395", "715687493157", "橘子" }
                });

            migrationBuilder.InsertData(
                table: "RoutesOfAdminstrations",
                columns: new[] { "ROAID", "BodyParts" },
                values: new object[] { "PO", "口服" });

            migrationBuilder.InsertData(
                table: "Cashiers",
                column: "CasID",
                values: new object[]
                {
                    "C11201001",
                    "C11201002"
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorID", "DepartmentName" },
                values: new object[,]
                {
                    { "D11201001", "心臟內科" },
                    { "D11201002", "心臟內科" }
                });

            migrationBuilder.InsertData(
                table: "Drugs",
                columns: new[] { "DrugID", "AdverseReactions", "Appearance", "ATCCode", "ClinicalUses", "DContent", "DrugName", "GenericName", "NHICode", "OtherInstructions", "PointOfHealthEducation", "ProcessingMethod", "ROAID", "StorageConditions", "SuggestedUsage", "UnitPrice", "Warning_Precautions" },
                values: new object[,]
                {
                    { "009554", "疲憊、頭痛、顏面神經痛、精神沮喪、幻覺、困倦、低血壓、視力障礙、厭食、嘔吐、腹瀉、發汗、吞嚥困難", "白色圓扁形錠, |D025", "C01AA05", "心臟衰竭、心律不整", "0.25mg/tab", "Digoxin", "Digoxin", "BC09554100", "無資料", "1.不要自行改變劑量或停藥。 2.定期量血壓和心跳。 3.服用後可能發生頭暈、低血壓，小心跌倒。 4.定期監測藥物血中濃度與血鉀濃度。 5.服藥期間若發生虛弱無力、心跳太慢，意識混亂等應即刻就醫。 6.避免高麩飲食。", "無資料", "PO", "室溫陰涼乾燥處保存", "對於輕度心衰竭的病人，應每天給藥 0.25~0.75mg，共一星期，接著給予一適當的維持劑量，達到毛地黃化的速度可和緩些", 240m, "無資料" },
                    { "021757", "便秘、腹瀉、頭痛、噁心、無力等", "橘 | 淺藍膠囊,CellCept 250,Roche", "L04AA06", "預防或緩解腎臟移植之急性器官排斥、預防心臟和肝臟移植之急性器官排斥", "250mg/cap", "Cellcept", "Mycophenolate Mofetil", "BC20999100", "空腹時服用。不應膠囊打開或弄碎", "1.服用本藥會使您抵抗力較弱，請經常洗手並避免接觸有傳染性疾病如感冒的親友。 2.接種任何疫苗之前請先諮詢你的醫生。 3.此藥可能使您皮膚對光較敏感，外出時請做好防曬工作。 4.告訴牙醫、外科醫生和其他醫生您在使用本藥物。 5.不要同時與含有鎂或鋁的制酸劑一起服用。 6.若有高血壓，每日定時測量血壓、脈搏並列入記錄，提供醫師參考。 7.如果膠囊已經打開或破裂，不要接觸內含的藥物。如果已經接觸到藥物或藥物跑進眼睛，請立即洗手或洗眼睛。 8.建議具有生育能力的女性在服用期間及停藥6周內採取有效避孕措施且請勿授乳。有性行為的男性在服用期間及停藥90天內採取有效避孕措施且不應捐獻精液。", "無資料", "PO", "儲存於30℃以下避光處", "口服給藥在腎臟、心臟和肝臟移植後應儘速給予 CellCept的起始劑量", 240m, "整粒吞服, 勿嚼碎；孕婦禁用；服藥後可能嗜睡或眩暈" },
                    { "026173", "呼吸道感染、頭痛、水腫、昏厥、低血壓、心悸、貧血等", "橘白色圓凸錠,62.5", "C02KX01", "治療因先天性心臟病續發WHO Class III 肺動脈高血壓", "62.5mg/tab", "Tracleer", "Bosentan Monohydrate", "BC26173100", "每日服用1-2次，請於每日固定時間服用", "1.除非醫師指示，請勿任意停藥。 2.漏服一劑，請在想起時立即服用，切勿依次服用 2劑藥物。", "無資料", "PO", "儲存於30℃以下避光處", "腎功能受損者不需要調整劑量。接受血液透析治療之病人，亦無需調整劑量", 240m, "無資料" },
                    { "026672", "低血壓、高血鉀、頭暈、腎衰竭、血管性水腫等", "淺黃橢圓形錠,NVR,L1", "C09DX04", "治療慢性心臟衰竭", "100mg/tab", "Entresto", "Sacubitril and Valsartan", "BC26671100", "可與食物併服或是空腹服用", "1.預備懷孕、已懷孕或授乳應先告訴醫師。 2.不要自行改變劑量或停藥。 3.定期量血壓和心跳。 4.服用後可能發生頭暈、低血壓，小心跌倒。 5.如長期使用含高鉀的飲食(如低鈉鹽、香蕉、柑橘類高鉀水果），須先請教醫師。", "無資料", "PO", "儲存於30℃以下避光處", "不建議重度肝功能不全患者使用此藥物", 240m, "孕婦禁用；服藥後可能有姿態性低血壓；" },
                    { "046404", "暈眩、頭痛，通常是輕微的且尤其於治療的初期", "白色圓形錠,Syntrend,S｜Y", "C07AG02", "高血壓、鬱血性心臟衰竭", "25mg/tab", "Syntrend", "Carvedilol", "AC46404100", "隨餐或餐後立即服用", "1.懷孕或計劃懷孕、氣喘或其他肺臟疾病、糖尿病、甲狀腺機能亢進，請先告訴醫師。 2.不要自行改變劑量或停藥。 3.請定時測量脈搏，低於醫師所訂標準請即就醫。 4.服用後可能發生頭暈、低血壓，小心跌倒。 5.糖尿病病人請注意，此藥可能會影響血糖值，掩蓋低血糖現象，應小心監測血糖。", "無資料", "PO", "室溫保存", "治療的期間︰服用 Carvedilol需長期性的治療。不應突然停止治療而應以星期為間隔逐漸減少治療，此對併有冠狀心臟疾病的病人尤其重要", 240m, "無資料" }
                });

            migrationBuilder.InsertData(
                table: "Pharmacists",
                column: "PhaID",
                values: new object[]
                {
                    "P11201001",
                    "P11201002"
                });

            migrationBuilder.InsertData(
                table: "Charts_Drugs_Dosages",
                columns: new[] { "ChaID", "DosID", "DrugID", "Days", "Remark", "Total" },
                values: new object[,]
                {
                    { "CHA20230530001001", "ORDER", "046404", 3, "無", 9 },
                    { "CHA20230530001002", "ORDER", "046404", 3, "無", 9 },
                    { "CHA20230530001003", "ORDER", "046404", 3, "無", 9 },
                    { "CHA20230530001004", "ORDER", "046404", 3, "無", 9 },
                    { "CHA20230530001005", "ORDER", "046404", 3, "無", 9 }
                });

            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "DetID", "CasID", "MedicalCost", "PatientID", "Payable", "Registration" },
                values: new object[,]
                {
                    { "DET20230530001001", "C11201001", 500m, "A118992634", 650m, 150m },
                    { "DET20230530001002", "C11201001", 500m, "O101929955", 650m, 150m },
                    { "DET20230530001003", "C11201001", 500m, "H255590997", 650m, 150m },
                    { "DET20230530001004", "C11201001", 500m, "L198058112", 650m, 150m },
                    { "DET20230530001005", "C11201001", 500m, "S257920071", 650m, 150m }
                });

            migrationBuilder.InsertData(
                table: "Doctors_Patients_Charts",
                columns: new[] { "ChaID", "DoctorID", "PatientID" },
                values: new object[,]
                {
                    { "CHA20230530001001", "D11201001", "A118992634" },
                    { "CHA20230530001002", "D11201001", "O101929955" },
                    { "CHA20230530001003", "D11201002", "H255590997" },
                    { "CHA20230530001004", "D11201002", "L198058112" },
                    { "CHA20230530001005", "D11201002", "S257920071" }
                });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "PresNo", "DrugDate", "PatientID", "PhaID" },
                values: new object[,]
                {
                    { "PRE20230530001001", new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "A118992634", "P11201001" },
                    { "PRE20230530001002", new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "O101929955", "P11201001" },
                    { "PRE20230530001003", new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "H255590997", "P11201001" },
                    { "PRE20230530001004", new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "L198058112", "P11201001" },
                    { "PRE20230530001005", new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "S257920071", "P11201001" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cashiers",
                keyColumn: "CasID",
                keyValue: "C11201002");

            migrationBuilder.DeleteData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DosID", "DrugID" },
                keyValues: new object[] { "CHA20230530001001", "ORDER", "046404" });

            migrationBuilder.DeleteData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DosID", "DrugID" },
                keyValues: new object[] { "CHA20230530001002", "ORDER", "046404" });

            migrationBuilder.DeleteData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DosID", "DrugID" },
                keyValues: new object[] { "CHA20230530001003", "ORDER", "046404" });

            migrationBuilder.DeleteData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DosID", "DrugID" },
                keyValues: new object[] { "CHA20230530001004", "ORDER", "046404" });

            migrationBuilder.DeleteData(
                table: "Charts_Drugs_Dosages",
                keyColumns: new[] { "ChaID", "DosID", "DrugID" },
                keyValues: new object[] { "CHA20230530001005", "ORDER", "046404" });

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET20230530001001");

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET20230530001002");

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET20230530001003");

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET20230530001004");

            migrationBuilder.DeleteData(
                table: "Details",
                keyColumn: "DetID",
                keyValue: "DET20230530001005");

            migrationBuilder.DeleteData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "DoctorID", "PatientID" },
                keyValues: new object[] { "CHA20230530001001", "D11201001", "A118992634" });

            migrationBuilder.DeleteData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "DoctorID", "PatientID" },
                keyValues: new object[] { "CHA20230530001002", "D11201001", "O101929955" });

            migrationBuilder.DeleteData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "DoctorID", "PatientID" },
                keyValues: new object[] { "CHA20230530001003", "D11201002", "H255590997" });

            migrationBuilder.DeleteData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "DoctorID", "PatientID" },
                keyValues: new object[] { "CHA20230530001004", "D11201002", "L198058112" });

            migrationBuilder.DeleteData(
                table: "Doctors_Patients_Charts",
                keyColumns: new[] { "ChaID", "DoctorID", "PatientID" },
                keyValues: new object[] { "CHA20230530001005", "D11201002", "S257920071" });

            migrationBuilder.DeleteData(
                table: "Dosages",
                keyColumn: "DosID",
                keyValue: "BIAH");

            migrationBuilder.DeleteData(
                table: "Dosages",
                keyColumn: "DosID",
                keyValue: "BIDP");

            migrationBuilder.DeleteData(
                table: "Dosages",
                keyColumn: "DosID",
                keyValue: "PRN");

            migrationBuilder.DeleteData(
                table: "Dosages",
                keyColumn: "DosID",
                keyValue: "QN");

            migrationBuilder.DeleteData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "009554");

            migrationBuilder.DeleteData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "021757");

            migrationBuilder.DeleteData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "026173");

            migrationBuilder.DeleteData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "026672");

            migrationBuilder.DeleteData(
                table: "Pharmacists",
                keyColumn: "PhaID",
                keyValue: "P11201002");

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE20230530001001");

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE20230530001002");

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE20230530001003");

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE20230530001004");

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "PresNo",
                keyValue: "PRE20230530001005");

            migrationBuilder.DeleteData(
                table: "Cashiers",
                keyColumn: "CasID",
                keyValue: "C11201001");

            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA20230530001001");

            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA20230530001002");

            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA20230530001003");

            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA20230530001004");

            migrationBuilder.DeleteData(
                table: "Charts",
                keyColumn: "ChaID",
                keyValue: "CHA20230530001005");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorID",
                keyValue: "D11201001");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "DoctorID",
                keyValue: "D11201002");

            migrationBuilder.DeleteData(
                table: "Dosages",
                keyColumn: "DosID",
                keyValue: "ORDER");

            migrationBuilder.DeleteData(
                table: "Drugs",
                keyColumn: "DrugID",
                keyValue: "046404");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "C11201002");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "P11201002");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "A118992634");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "H255590997");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "L198058112");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "O101929955");

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "PatientID",
                keyValue: "S257920071");

            migrationBuilder.DeleteData(
                table: "Pharmacists",
                keyColumn: "PhaID",
                keyValue: "P11201001");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "C11201001");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "D11201001");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "D11201002");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: "P11201001");

            migrationBuilder.DeleteData(
                table: "RoutesOfAdminstrations",
                keyColumn: "ROAID",
                keyValue: "PO");
        }
    }
}
