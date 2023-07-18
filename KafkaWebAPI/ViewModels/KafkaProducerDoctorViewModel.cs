using HISDB.Models;
namespace KafkaWebAPI.ViewModels
{
    public class KafkaProducerDoctorViewModel
    {

        public string? Topic { get; set; }
        public string? Key { get; set; }
        public DoctorMessage? Message { get; set; }
    }


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

        public double Total { get; set; }

        public string? Remark { get; set; }
    }
}
