using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerTerminal.Services
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

        public void Produce(string topic, string message)
        {
            try
            {
                var result = producer.ProduceAsync(topic, new Message<string, string> { Key = null, Value = message }).GetAwaiter().GetResult();
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
