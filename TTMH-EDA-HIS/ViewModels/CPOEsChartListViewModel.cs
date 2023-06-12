using TTMH_EDA_HIS.Models;

namespace TTMH_EDA_HIS.ViewModels
{
    public class CPOEsChartListViewModel
    {
        public List<Patient?>? Patients = new List<Patient>();
        public string? content { get; set; }
    }
}
