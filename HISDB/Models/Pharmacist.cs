using System;
using System.Collections.Generic;

namespace HISDB;

public partial class Pharmacist
{
    // ID
    public string PhaId { get; set; } = null!;
    public virtual Employee Pha { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
