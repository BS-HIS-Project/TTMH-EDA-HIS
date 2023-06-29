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

        //[DisplayName("病歷編號")]
        //public string CaseHistory { get; set; }
        //[DisplayName("病患姓名")]
        //public string PatsName { get; set; }
        //[DisplayName("性別")]
        //public int gender { get; set; }
        //[DisplayName("出生日期")]
        //public DateTime birthday { get; set; }

        [DisplayName("年齡")]
        public int? age { get; set; }
        [DisplayName("醫生")]
        public string? docsName { get; set; }
        [DisplayName("就診日期")]
        public DateTime? Vdate { get; set; }

    }
}
