using Confluent.Kafka;
using static Confluent.Kafka.ConfigPropertyNames;

namespace KafkaWebAPI.Services
{
    public class KafkaConsumer
    {
        private readonly ConsumerConfig config = new ConsumerConfig
        {
            BootstrapServers = "server.nicklu89.com:9092",
            GroupId = "test-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        public void GetMsg()
        {
            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe("my-topic");

                while (true)
                {
                    var consumeResult = consumer.Consume();

                    Console.WriteLine($"Received message: {consumeResult.Message.Value}");
                }
            }
        }
        
    }
}
