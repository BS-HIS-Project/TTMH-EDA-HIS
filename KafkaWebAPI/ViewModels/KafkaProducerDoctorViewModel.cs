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
