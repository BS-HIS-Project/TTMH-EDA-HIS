using System;
using System.Collections.Generic;
namespace HISDB.Models;

public partial class Detail
{
    public string DetId { get; set; } = null!;

    public decimal Registration { get; set; }

    public decimal MedicalCost { get; set; }

    public decimal Payable { get; set; }

    public string CasId { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    //繳費時間
    public DateTime? PaymentTime { get; set; }
    // 部分負擔
    public decimal? PartialPayment { get; set; }
    //診察費
    public decimal? Diagnostic { get; set; }
    //藥品部分負擔
    public decimal? DrugPartialPayment { get; set; }

    public virtual Cashier Cas { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
