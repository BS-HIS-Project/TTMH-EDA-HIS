using System;
using System.Collections.Generic;

namespace HISDB.Models;

public partial class Prescription
{
    public string PresNo { get; set; } = null!;
    //領藥時間
    public DateTime? DrugDate { get; set; }

    public string PhaId { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual Pharmacist Pha { get; set; } = null!;
}
