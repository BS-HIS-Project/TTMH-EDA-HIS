namespace TTMH_EDA_HIS.ViewModels
{
    public class ChartsViewDetailsPostRequestAPIViewModel
    {
        public string topic { set; get; }
        public string key { set; get; }
        public ChartsViewDetailsPostRequestAPIViewModel_message message { set; get; }

    }
    public class ChartsViewDetailsPostRequestAPIViewModel_message
    {
        public string detId { set; get; }
        public string patientId { set; get; }
        public string vdate { set; get; }
        public string doctorName { set; get; }
    }
}
