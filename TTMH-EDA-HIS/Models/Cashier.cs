using System;
using System.Collections.Generic;

namespace TTMH_EDA_HIS.Models;

public partial class Cashier
{
    public string CasId { get; set; } = null!;

    public virtual Employee Cas { get; set; } = null!;

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();
}
