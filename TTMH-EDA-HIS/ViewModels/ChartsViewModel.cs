using HISDB.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TTMH_EDA_HIS.ViewModels
{
    public class ChartsViewModel
    {

        public Patient? Patient { get; set; }
        //public Detail? Detail { get; set; }
        public string? content { get; set; }

        [DisplayName("繳費條號")]
        public string? DetId { get; set; }

        [DisplayName("出生日期")]
        public string? birthday { get; set; }
        
        [DisplayName("年齡")]
        public int? age { get; set; }
        
        [DisplayName("醫生")]
        public string? docsName { get; set; }
        
        [DisplayName("就診日期")]
        public string? Vdate { get; set; }

        [DisplayName("繳費時間")]
        public DateTime? PaymentTime { get; set; }
        [DisplayName("掛號費")]
        public decimal? Registration { get; set; }

        [DisplayName("部分負擔")]
        public decimal? PartialPayment { get; set; }

        [DisplayName("藥品部分負擔")]
        public decimal? DrugPartialPayment { get; set;}

        [DisplayName("診察費")]
        public decimal? Diagnostic { get; set; }

        [DisplayName("藥費")]
        public decimal? MedicalCost { get; set; }

        [DisplayName("應繳金額")]
        public decimal? Payable {  get; set; }

        //確認id狀態
        public int StatusCode { get; set; } = 0;

        public List<ChartsViewModel_Drug> Drugs { get; set; }

        public class ChartsViewModel_Drug
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

            [DisplayName("藥價")]
            public int? UnitPrice { get; set; }
        }
    }
}
