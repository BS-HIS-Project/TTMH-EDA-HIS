namespace TTMH_EDA_HIS.ViewModels
{
    public class CPOEsCommitAPIPrescriptionViewModel
    {
        public string? Topic { get; set; }
        public string? Key { get; set; }
        public CPOEsCommitAPIPrescriptionViewModel_DoctorMessage? Message { get; set; }
    }
    public class CPOEsCommitAPIPrescriptionViewModel_DoctorMessage
    {
        public string? DoctorId { get; set; }
        public string? PatientId { get; set; }
        public string? ChaId { get; set; }

        public List<CPOEsCommitAPIPrescriptionViewModel_ChartsDrugsDosage>? ChartsDrugsDosages { get; set; }
    }

    public class CPOEsCommitAPIPrescriptionViewModel_ChartsDrugsDosage
    {
        public string DrugId { get; set; } = null!;
        public string DosId { get; set; } = null!;
        public double Quantity { get; set; }
        public int Days { get; set; }
        public int Total { get; set; }
        public string? Remark { get; set; }
    }
}
