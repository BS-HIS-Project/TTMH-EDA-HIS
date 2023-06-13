using Confluent.Kafka;
using System.Text.Json;
using ConsumerTerminal.ViewModels;
using HISDB.Models;
using HISDB.Data;
using HISDB;

var _context = new HisdbContext();

Console.WriteLine("請輸入GroupId");
var inputGroupId = Console.ReadLine();

ConsumerConfig config = new ConsumerConfig
{
    BootstrapServers = "server.nicklu89.com:9092",
    GroupId = inputGroupId,
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
{
    consumer.Subscribe("my-topic");
    
    var CashierId = "C11201001";
    var ClinicNumber = 1;
    var PaymentBarcode = 1;

    while (true)
    {
        var consumeResult = consumer.Consume();

        try
        {
            var JsonData = JsonSerializer.Deserialize<DoctorMessage>(consumeResult.Message.Value);

            if (JsonData == null)
            {
                Console.WriteLine("Msg not DoctorMessage or JsonData is NULL");
            } else
            {
                Detail _detail = new Detail()
                {
                    // DET + NOWDATE + 診間號 + (繳費條碼)序號
                    DetId = $"DET{DateTime.Now.ToString("yyyyMMddHH")}{ClinicNumber.ToString().PadLeft(3, '0')}{PaymentBarcode.ToString().PadLeft(3, '0')}",
                    // 掛號費
                    Registration = Convert.ToDecimal(150),
                    // 藥費
                    MedicalCost = Convert.ToDecimal(500),
                    // 應繳金額
                    Payable = Convert.ToDecimal(650),

                    CasId = CashierId,
                    PatientId = JsonData.PatientID ?? throw new Exception("PatientId is NULL")
                };
                PaymentBarcode++;
                _context.Details.Add(_detail);
                _context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Console.WriteLine($"Received message: {consumeResult.Message.Value}");
    }
}