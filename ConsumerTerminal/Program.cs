using HISDB.Data;
using HISDB.Models;
using Confluent.Kafka;
using System.Text.Json;
using NuGet.Versioning;
using ConsumerTerminal.ViewModel;

var _context = new HisdbContext();

ConsumerConfig config = new ConsumerConfig
{
    BootstrapServers = "server.nicklu89.com:9092",
    GroupId = "Group001",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
{
    consumer.Subscribe("my-topic");

    var ClinicNumber = 1;
    var PaymentBarcode = 1;
    var CashierId = "C11201001";

    while (true)
    {
        var consumeResult = consumer.Consume();
        DoctorMessage JsonData;
        try
        {
            JsonData = JsonSerializer.Deserialize<DoctorMessage>(consumeResult.Message.Value) ?? throw new Exception("Deserialize failed");
            Console.WriteLine($"DoctorId: {JsonData.DoctorId ?? "NoDoctorData"}");

            if(JsonData.DoctorId != null)
            {
                Detail _detail = new Detail()
                {
                    // DET + NOWDATE + 診間號 + (繳費條碼)序號
                    DetId = $"DET{DateTime.Now.ToString("yyyyMMddHHmmss")}{ClinicNumber.ToString().PadLeft(3, '0')}{PaymentBarcode.ToString().PadLeft(3, '0')}",
                    // 掛號費
                    Registration = Convert.ToDecimal(150),
                    // 藥費
                    MedicalCost = Convert.ToDecimal(500),
                    // 應繳金額
                    Payable = Convert.ToDecimal(650),

                    CasId = CashierId,
                    PatientId = JsonData.PatientID ?? throw new Exception("PatientID is null"),

                    //Cas = _context.Cashiers.Find(CashierId) ?? throw new Exception("CashierId not found"),
                    //Patient = _context.Patients.Find(JsonData.PatientID) ?? throw new Exception("PatientID not found")
                };
                
                Console.WriteLine($"DetId: {_detail.DetId}");
                Console.WriteLine($"PatientId: {_detail.PatientId}");
                Console.WriteLine($"CasId: {_detail.CasId}");
                Console.WriteLine($"Registration: {_detail.Registration}");
                Console.WriteLine($"MedicalCost: {_detail.MedicalCost}");
                Console.WriteLine($"Payable: {_detail.Payable}");
                //Console.WriteLine($"Cas: {_detail.Cas.CasId}");
                //Console.WriteLine($"Patient: {_detail.Patient.PatientId}\n\n");

                _context.Details.Add(_detail);
                _context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine($"Msg: {consumeResult.Message.Value}");
    }
}
