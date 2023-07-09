namespace KafkaWebAPI.ViewModels
{
    public class KafkaProducerDetailViewModel
    {
        public string? Topic { get; set; }
        public string? Key { get; set; }
        public Receipt? Message { get; set; }
    }

    public class Receipt
    {
        public string DetId { get; set; } = null!;
        public string? PatientId { get; set; }
        public string? Vdate { get; set; }
    }
}
