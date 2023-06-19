using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HISDB.Models;

namespace ConsumerTerminal.ViewModels
{
    public class DoctorMessage
    {
        public string? DoctorId { get; set; }
        public string? PatientId { get; set; }
        public string? ChaId { get; set; }

        public List<ChartsDrugsDosage>? ChartsDrugsDosages { get; set; }
    }

    public class ChartsDrugsDosage
    {
        //public string ChaId { get; set; } = null!;

        public string DrugId { get; set; } = null!;

        public string DosId { get; set; } = null!;

        //次量
        public double Quantity { get; set; }

        public int Days { get; set; }

        public int Total { get; set; }

        public string? Remark { get; set; }
    }
}
