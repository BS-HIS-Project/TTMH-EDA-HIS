namespace TTMH_EDA_HIS.ViewModels
{
    public class CPOEsPatientDetailsViewModel
    {
        public string? CaseHistory { get; set; }
        public string? PatientName { get; set; }
        public string? Gender { get; set; }
        public string? BirthDate { get; set; }
        public string? Age { get; set; }
        public string? DoctorName { get; set; }
        public string? VDate { get; set; }
        public string? Subject { get; set; }
        public string? Object { get; set; }
        public List<DrugTableTD> drugList { get; set; }
    }
    public class DrugTableTD
    {
        public string? DrugID { get; set; }
        public string? DrugName { get; set; }
        public string? DosID { get; set; }
        public string? Freq { get; set; }
        public string? BodyParts { get; set; }
        public string? Days { get; set; }
        public string? Total { get; set; }
        public string? Remark { get; set; }
    }
}
