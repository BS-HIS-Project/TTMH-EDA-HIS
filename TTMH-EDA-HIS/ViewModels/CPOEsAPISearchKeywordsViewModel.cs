namespace TTMH_EDA_HIS.ViewModels
{
    public class CPOEsAPISearchKeywordsViewModel
    {
        public string SearchKey { get; set; }
    }
    public class CPOEsAPISearchKeywordsViewModel_Response
    {
        public string SearchKeyRequested { get; set; }
        public string[] Results { get; set; }
        public string[] Relatives { get; set; }
    }
}
