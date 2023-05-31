using System;
using System.Collections.Generic;

namespace TTMH_EDA_HIS.Models;

public partial class Detail
{
    public string DetId { get; set; } = null!;

    public decimal Registration { get; set; }

    public decimal MedicalCost { get; set; }

    public decimal Payable { get; set; }

    public string CasId { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public virtual Cashier Cas { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
