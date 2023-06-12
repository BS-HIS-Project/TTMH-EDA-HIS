using Confluent.Kafka;

namespace KafkaWebAPI.Services
{
    public class KafkaProducer
    {
        private readonly ProducerConfig config;
        private readonly IProducer<string, string> producer;

        public KafkaProducer(string brokerList)
        {
            config = new ProducerConfig { BootstrapServers = brokerList };
            producer = new ProducerBuilder<string, string>(config).Build();
        }
        public void Produce(string topic, string key, string message)
        {
            try
            {
                var result = producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = message }).GetAwaiter().GetResult();
                Console.WriteLine($"Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");
            }
            catch (ProduceException<string, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }

        public void Dispose()
        {
            producer?.Dispose();
        }
    }
}
