using HISDB.Models;
using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace TTMH_EDA_HIS.ViewModels
{
    public class CPOEsPatientDetailsViewModel
    {
        [DisplayName("病歷號碼")]
        public string? CaseHistory { get; set; } 
        [DisplayName("病患姓名")]
        public string? PatientName { get; set; } 
        [DisplayName("年齡")]
        public string? Age { get; set; } 
        [DisplayName("病患性別")]
        public string? Gender { get; set; } 
        [DisplayName("出生年月日")]
        public string? BirthDate { get; set; } 
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
        [DisplayName("藥品編號")]
        public string? DrugID { get; set; }
        [DisplayName("藥品名稱")]
        public string? DrugName { get; set; }
        [DisplayName("用法")]
        public string? DosID { get; set; }
        [DisplayName("頻率")]
        [DataType("int")]
        public int? Freq { get; set; }
        [DisplayName("次量")]
        [DataType("float")]
        public double? Quantity { get; set; }
        [DisplayName("天數")]
        [DataType("int")]
        public int? Days { get; set; }
        [DisplayName("總量")]
        [DataType("int")]
        public int? Total { get; set; }
        [DisplayName("備註")]
        public string? Remark { get; set; }

        public string? BodyParts { get; set; }
    }
}
