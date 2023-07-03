using System.ComponentModel;
using HISDB.Models;


namespace TTMH_EDA_HIS.ViewModels
{
    public class PresViewModel
    {
        public Patient? Patient { get; set; }
        public string? content { get; set; }

        [DisplayName("領藥號")]
        public string? PresNo { get; set; }
        [DisplayName("出生日期")]
        public string? birthday { get; set; }

        [DisplayName("年齡")]
        public int? age { get; set; }
        [DisplayName("醫生")]
        public string? docsName { get; set; }
        [DisplayName("就診日期")]
        public string? Vdate { get; set; }

        public int StatusCode { get; set; } = 0;
        public List<PresViewModel_Drug> Drugs { get; set; }

        //繳費時間
        public DateTime? PaymentTime { get; set; }

        //領藥時間
        public DateTime? DrugDate { get; set; }
    }
    public class PresViewModel_Drug 
    {
        [DisplayName("藥品編號")]
        public string? DrugID { get; set; }
        [DisplayName("藥品名稱")]
        public string? DrugName { get; set; }
        [DisplayName("用法")]
        public string? DosID { get; set; }
        [DisplayName("頻率")]
        public int? Freq { get; set; }
        [DisplayName("次量")]
        public double? Quantity { get; set; }
        [DisplayName("天數")]
        public int? Days { get; set; }
        [DisplayName("總量")]
        public double? Total { get; set; }
        [DisplayName("備註")]
        public string? Remark { get; set; }


    }
}
