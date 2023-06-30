using Confluent.Kafka;
using System.Text.Json;
using ConsumerTerminal.ViewModels;
using HISDB.Models;
using HISDB.Data;
using HISDB;
using ConsumerTerminal.Services.BillingSystem;

var _context = new HisdbContext();

//Console.WriteLine("請輸入GroupId");
//var inputGroupId = Console.ReadLine();
var inputGroupId = "G001";

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
                PaymentBarcode = CreateDetall(CashierId, ClinicNumber, PaymentBarcode, JsonData);
                CreateCDDs(JsonData);
                
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //Console.WriteLine($"Received message: {consumeResult.Message.Value} NOT Doctor VM");
        }

        Console.WriteLine($"Received message: {consumeResult.Message.Value}");
    }
}

void CreateCDDs(DoctorMessage? JsonData)
{
    if (JsonData.ChartsDrugsDosages != null)
    {
        foreach (var data in JsonData.ChartsDrugsDosages)
        {
            var _ChartsDrugsDosage = new HISDB.Models.ChartsDrugsDosage()
            {
                ChaId = JsonData.ChaId ?? throw new Exception("ChaId is NULL"),
                DrugId = data.DrugId,
                DosId = data.DosId,
                Quantity = data.Quantity,
                Days = data.Days,
                Remark = data.Remark
            };

            var _chart = _context.Charts.Where(x => x.ChaId == JsonData.ChaId).FirstOrDefault();
            var _dosage = _context.Dosages.Where(x => x.DosId == data.DosId).FirstOrDefault();
            var _drug = _context.Drugs.Where(x => x.DrugId == data.DrugId).FirstOrDefault();

            if (_chart == null)
            {
                throw new Exception("Chart is NULL");
            }
            if (_dosage == null)
            {
                throw new Exception("Dosage is NULL");
            }
            if (_drug == null)
            {
                throw new Exception("Drug is NULL");
            }

            _ChartsDrugsDosage.Cha = _chart;
            _ChartsDrugsDosage.Dos = _dosage;
            _ChartsDrugsDosage.Drug = _drug;

            var _freg = _dosage.Freq;
            if (_freg == 0)
            {
                throw new Exception("Freq is 0");
            }
            else if (_freg == null)
            {
                throw new Exception("Freg is NULL");
            }

            _ChartsDrugsDosage.Total = (int)((double)_freg * (double)_ChartsDrugsDosage.Quantity * (double)_ChartsDrugsDosage.Days);

            if (_context.ChartsDrugsDosages.Find(_ChartsDrugsDosage.ChaId, _ChartsDrugsDosage.DrugId) != null)
            {
                Console.WriteLine($"ChartsDrugsDosage: {_ChartsDrugsDosage.ChaId}, {_ChartsDrugsDosage.DrugId} 新增已存在");
            }
            else
            {
                _context.ChartsDrugsDosages.Add(_ChartsDrugsDosage);
                _context.SaveChanges();

                Console.WriteLine($"ChartsDrugsDosage: {_ChartsDrugsDosage.ChaId} 成功新增");
            }
        }
    }
}

int CreateDetall(string CashierId, int ClinicNumber, int PaymentBarcode, DoctorMessage? JsonData)
{
    ConnectServices connectServices = new ConnectServices();

    var _billing = connectServices.GetBilling(JsonData?.PatientId ?? throw new Exception("PatientId is NULL"));

    Detail _detail = new Detail()
    {
        // DET + NOWDATE + 診間號 + (繳費條碼)序號
        DetId = $"DET{DateTime.Now.ToString("yyyyMMddHH")}{ClinicNumber.ToString().PadLeft(3, '0')}{PaymentBarcode.ToString().PadLeft(3, '0')}",
        // 掛號費
        Registration = _billing.RegistrationFee(),
        // 藥費
        MedicalCost = _billing.DrugFee(),
        // 應繳金額
        Payable = _billing.Total(),

        CasId = CashierId,
        PatientId = JsonData.PatientId ?? throw new Exception("PatientId is NULL")
    };

    PaymentBarcode++;

    while (_context.Details.Find(_detail.DetId) != null)
    {
        Console.WriteLine($"Detail: {_detail.DetId} 新增已存在");
        _detail.DetId = $"DET{DateTime.Now.ToString("yyyyMMddHH")}{ClinicNumber.ToString().PadLeft(3, '0')}{PaymentBarcode.ToString().PadLeft(3, '0')}";
        PaymentBarcode++;
    }

    _context.Details.Add(_detail);
    _context.SaveChanges();

    Console.WriteLine($"Detail: {_detail.DetId} 成功新增");
    return PaymentBarcode;
}