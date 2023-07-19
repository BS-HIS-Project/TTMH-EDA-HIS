using KafkaWebAPI.ViewModels;
namespace KafkaWebAPI.ViewModels
{
    public class KafkaProducerBagControlViewModel
    {
        public string? Topic { get; set; }
        public string? Key { get; set; }
        public BagControlMsg? Message { get; set; }
    }

    public class BagControlMsg
    {
        public DoctorMessage? DoctorMessage { get; set; }
        public string? PresNo { get; set; }
        public string? DetId { get; set; }
    }
}
