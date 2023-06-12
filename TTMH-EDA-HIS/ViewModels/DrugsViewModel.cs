using System.ComponentModel;

namespace TTMH_EDA_HIS.ViewModels
{
    public class DrugsViewModel
    {
        [DisplayName("藥品編號")]
        public string DrugId { get; set; }
        [DisplayName("藥品名稱")]
        public string DrugName { get; set; }
        [DisplayName("頻率代號")]
        public string dosId { get; set; }
        //[DisplayName("次數")]
        //public int freq { get; set; }
        [DisplayName("用法")]
        public string bp { get; set; }
        [DisplayName("天數")]
        public int days { get; set; }
        [DisplayName("總量")]
        public int total { get; set; }
        [DisplayName("備註")]
        public string remark { get; set; }
    }
}
