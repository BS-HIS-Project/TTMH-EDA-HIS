using Confluent.Kafka;
using System.Text.Json;
using ConsumerTerminal.ViewModels;
using HISDB.Models;
using HISDB.Data;
using HISDB;
using ConsumerTerminal.Services.BillingSystem;
using ConsumerTerminal.Services.PrintSystem;

var _context = new HisdbContext();

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
    var PhamacistId = "P11201001";
    var ClinicNumber = 1;
    var DrugNumber = 1;
    var PaymentBarcode = 1;
    string PresNo;

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
                DrugNumber = CreatePrescription(PhamacistId, DrugNumber, PaymentBarcode, JsonData, out PresNo);

                PaymentBarcode = CreateDetall(CashierId, ClinicNumber, PaymentBarcode, JsonData);

                CreateCDDs(JsonData);


                var BagControlSer = new BagControlServices(JsonData, PresNo, JsonData.ChaId);
                BagControlSer.run();

                // 藥袋內頁列印
                //foreach(var data in JsonData.ChartsDrugsDosages)
                //{
                //    var _MBSer = new MedicineBagServices(JsonData.ChaId, data.DrugId, JsonData.PatientId, PresNo);
                //}
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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

                Console.WriteLine($"ChartsDrugsDosage: {_ChartsDrugsDosage.ChaId}, {_ChartsDrugsDosage.DrugId} 成功新增");
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
        // 部分負擔
        PartialPayment = _billing.PartialPayment(),
        // 診察費
        Diagnostic = _billing.DiagnosticFee(),
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

int CreatePrescription( string PhamacistId, int DrugNumber, int PaymentBarcode, DoctorMessage? JsonData, out string PresNo)
{
    var _Prescription = new Prescription()
    {
        // PRE + NOWDATE + 小時 + 診間號 + (領藥號)序號
        PresNo = $"PRE{DateTime.Now.ToString("yyyyMMddHH")}{DrugNumber.ToString().PadLeft(3, '0')}{PaymentBarcode.ToString().PadLeft(3, '0')}",
        PhaId = PhamacistId,
        PatientId = JsonData.PatientId
    };

    DrugNumber++;

    while (_context.Prescriptions.Find(_Prescription.PresNo) != null)
    {
        Console.WriteLine($"Prescription: {_Prescription.PresNo} 新增已存在");
        _Prescription.PresNo = $"PRE{DateTime.Now.ToString("yyyyMMddHH")}{DrugNumber.ToString().PadLeft(3, '0')}{PaymentBarcode.ToString().PadLeft(3, '0')}";
        DrugNumber++;
    }

    _context.Prescriptions.Add(_Prescription);
    _context.SaveChanges();

    Console.WriteLine($"Prescription: {_Prescription.PresNo} 成功新增");

    PresNo = _Prescription.PresNo;
    return DrugNumber;
}