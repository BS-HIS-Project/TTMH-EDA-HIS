namespace TTMH_EDA_HIS.ViewModels
{
    public class CPOEsCommitAPICommitDrugsViewModel
    {
        public string DoctorID { get; set; }
        public string PatientID { get; set; }
        public string Subject { get; set; }
        public string Object { get; set; }
        public List<CPOEsCommitAPICommitDrugsViewModel_Drugs> Drugs { get; set; }
    }
    public class CPOEsCommitAPICommitDrugsViewModel_Drugs
    {
        public string DrugID { get; set; }
        public string DosID { get; set; }
        public string Freq { get; set; }
        public string Quantity { get; set; }
        public string Days { get; set; }
        public string Total { get; set; }
        public string Remark { get; set; }
    }
}

