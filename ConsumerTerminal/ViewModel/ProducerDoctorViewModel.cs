using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerTerminal.ViewModel
{
    public class DoctorMessage
    {
        public string? DoctorId { get; set; }
        public string? PatientID { get; set; }
        public string? ChaID { get; set; }

        public List<ChartsDrugsDosages>? ChartsDrugsDosages { get; set; }
    }

    public class ChartsDrugsDosages
    {

        public string DrugId { get; set; } = null!;

        public string DosId { get; set; } = null!;

        public int Days { get; set; }

        public string? Remark { get; set; }
    }
}
