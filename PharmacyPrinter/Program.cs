// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;

ConsumerConfig config = new ConsumerConfig
{
    BootstrapServers = "server.nicklu89.com:9092",
    GroupId = "test-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
{
    consumer.Subscribe("my-topic");
    //consumer.OffsetsForTimes();
    while (true)
    {
        var consumeResult = consumer.Consume();

        Console.WriteLine($"Received message: {consumeResult.Message.Value}");
    }
}
