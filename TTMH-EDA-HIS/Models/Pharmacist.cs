using System;
using System.Collections.Generic;

namespace TTMH_EDA_HIS.Models;

public partial class Pharmacist
{
    public string PhaId { get; set; } = null!;

    public virtual Employee Pha { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
