using Confluent.Kafka;
using HISDB.Models;
using HISDB.Data;
using HISDB;

var _context = new HisdbContext();

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