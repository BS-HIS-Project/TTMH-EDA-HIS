using System;
using System.Collections.Generic;

namespace HISDB.Models;

public partial class Doctor
{
    public string DoctorId { get; set; } = null!;

    public string DepartmentName { get; set; } = null!;

    public virtual Employee DoctorNavigation { get; set; } = null!;

    public virtual ICollection<DoctorsPatientsChart> DoctorsPatientsCharts { get; set; } = new List<DoctorsPatientsChart>();
}
