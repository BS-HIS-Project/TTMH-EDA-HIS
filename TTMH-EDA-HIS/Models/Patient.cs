using System;
using System.Collections.Generic;

namespace TTMH_EDA_HIS.Models;

public partial class Patient
{
    public string PatientId { get; set; } = null!;

    public string? Nhicard { get; set; }

    public string CaseHistory { get; set; } = null!;

    public string PatientName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string Gender { get; set; } = null!;

    public string? Blood { get; set; }

    public string Address { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual ICollection<DoctorsPatientsChart> DoctorsPatientsCharts { get; set; } = new List<DoctorsPatientsChart>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
