using System;
using System.Collections.Generic;

namespace TTMH_EDA_HIS.Models;

public partial class Dosage
{
    public string DosId { get; set; } = null!;

    public string Direction { get; set; } = null!;

    public virtual ICollection<ChartsDrugsDosage> ChartsDrugsDosages { get; set; } = new List<ChartsDrugsDosage>();
}
