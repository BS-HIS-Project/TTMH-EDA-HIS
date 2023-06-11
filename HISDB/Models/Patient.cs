using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HISDB;

public partial class Patient
{
    // ID
    public string PatientId { get; set; } = null!;
    // 健保卡號
    public string? Nhicard { get; set; }
    // 病例編號
    public string CaseHistory { get; set; } = null!;
    // 姓名
    public string PatientName { get; set; } = null!;
    // 出生年月日
    public DateTime BirthDate { get; set; }
    // 性別
    public string Gender { get; set; } = null!;
    // 血型
    public string? Blood { get; set; }
    // 居住地址
    public string Address { get; set; } = null!;
    // 電話
    public string Mobile { get; set; } = null!;

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual ICollection<DoctorsPatientsChart> DoctorsPatientsCharts { get; set; } = new List<DoctorsPatientsChart>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
