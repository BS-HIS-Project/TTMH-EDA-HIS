using System;
using System.Collections.Generic;

namespace HISDB.Models;

public partial class DoctorsPatientsChart
{
    public string DoctorId { get; set; } = null!;

    public string PatientId { get; set; } = null!;

    public string ChaId { get; set; } = null!;

    public virtual Chart Cha { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
