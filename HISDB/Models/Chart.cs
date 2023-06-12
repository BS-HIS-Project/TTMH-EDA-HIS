using System;
using System.Collections.Generic;

namespace HISDB.Models;

public partial class Chart
{
    public string ChaId { get; set; } = null!;

    public string DepartmentName { get; set; } = null!;

    public DateTime Vdate { get; set; }

    public string Subject { get; set; } = null!;

    public string Object { get; set; } = null!;

    public virtual ICollection<ChartsDrugsDosage> ChartsDrugsDosages { get; set; } = new List<ChartsDrugsDosage>();

    public virtual ICollection<DoctorsPatientsChart> DoctorsPatientsCharts { get; set; } = new List<DoctorsPatientsChart>();
}
