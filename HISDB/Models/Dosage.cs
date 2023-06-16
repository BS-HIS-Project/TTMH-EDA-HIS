using System;
using System.Collections.Generic;

namespace HISDB.Models;

public partial class Dosage
{
    public string DosId { get; set; } = null!;

    public string Direction { get; set; } = null!;
    public int? Freq { get; set; }
    public virtual ChartsDrugsDosage ChartsDrugsDosage { get; set; } = null!;
}
