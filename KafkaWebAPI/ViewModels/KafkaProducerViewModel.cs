namespace KafkaWebAPI.ViewModels
{
    public class KafkaProducerViewModel
    {
        public string? Topic { get; set; }
        public string? Partition { get; set; }
        public string? Key { get; set; }
        public string? Message { get; set; }
    }
}
