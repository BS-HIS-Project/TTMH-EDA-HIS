using HISDB.Models;

namespace TTMH_EDA_HIS.ViewModels
{
    public class CPOEsChartListViewModel
    {
        public List<Patient> Patients { get; set; }
        public string? content { get; set; }

        public bool UseButtonGp { get; set; }
        public int next_page { get; set; }
        public int previous_page { get; set; }
        public int page1 { get; set; }
        public int page2 { get; set; }
        public int page3 { get; set; }
    }
}
