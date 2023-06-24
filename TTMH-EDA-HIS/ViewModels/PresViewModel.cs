using System.ComponentModel;
using TTMH_EDA_HIS.Controllers;
using HISDB.Models;

namespace TTMH_EDA_HIS.ViewModels
{
    public class PresViewModel
    {
        //public IEnumerable<Prescription> Prescriptions { get; set; }
        //public IEnumerable<Patient> Patients { get; set; }
        //public IEnumerable<DoctorsPatientsChart> DPCs { get; set; }
        //public IEnumerable<Chart> Charts { get; set; }
        //public IEnumerable<Doctor> Doctors { get; set; }
        //public IEnumerable <Employee> Employees { get; set; }

        [DisplayName("領藥號")]
        public string PresId { get; set; }
        [DisplayName("病歷編號")]
        public string chId { get; set; }
        [DisplayName("病患姓名")]
        public string PatsName { get; set; }
        [DisplayName("性別")]
        public int gender { get; set; }
        [DisplayName("出生日期")]
        public DateTime birthday { get; set; }
        //[DisplayName("年齡")]
        //public DateTime age { get; set; }
        [DisplayName("醫生")]
        public string docsName { get; set; }
        [DisplayName("就診日期")]
        public DateTime Vdate { get; set; }

    }
}
