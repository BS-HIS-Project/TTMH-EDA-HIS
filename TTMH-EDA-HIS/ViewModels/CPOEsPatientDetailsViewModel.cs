using HISDB.Models;
using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace TTMH_EDA_HIS.ViewModels
{
    public class CPOEsPatientDetailsViewModel
    {
        public string? CaseHistory { get; set; } //病歷號碼
        public string? PatientName { get; set; } //病患姓名
        public string? Age { get; set; } //年齡
        public string? Gender { get; set; } //病患性別
        public string? BirthDate { get; set; } //出生年月日
        public string? DoctorID { get; set; }
        public string? DoctorName { get; set; }

        //就診紀錄
        public Chart? chart { get; set; } //就診號 + 看診日期 + Object + Subject + History
        public List<CPOEsPatientDetailsViewModel_DrugTableTD> Drugs { get; set; }

        //Navigation for other Charts
        public List<string> RecordsOfChaID { get; set; }
        public List<string> RecordsOfvdate { get; set; }
        public string? FirstChart { get; set; }
        public string? LastChart { get; set; }
        public string? PreviousChart { get; set; }
        public string? NextChart { get; set; }
    }

    public class CPOEsPatientDetailsViewModel_DrugTableTD
    {
        public string? DrugID { get; set; }
        public string? DrugName { get; set; }
        public string? DosID { get; set; }
        [DataType("int")]
        public int? Freq { get; set; }
        [DataType("float")]
        public double? Quantity { get; set; }
        [DataType("int")]
        public int? Days { get; set; }
        [DataType("int")]
        public int? Total { get; set; }
        public string? Remark { get; set; }
        public string? BodyParts { get; set; }
    }
}
