using System;
using System.Collections.Generic;

namespace HISDB.Models;

public partial class ChartsDrugsDosage
{
    public string ChaId { get; set; } = null!;

    public string DrugId { get; set; } = null!;

    public string DosId { get; set; } = null!;

    //次量
    public double Quantity { get; set; }
    public int Days { get; set; }
    public double Total { get; set; }

    public string? Remark { get; set; }

    public virtual Chart Cha { get; set; } = null!;

    public virtual Dosage Dos { get; set; } = null!;

    public virtual Drug Drug { get; set; } = null!;
}
