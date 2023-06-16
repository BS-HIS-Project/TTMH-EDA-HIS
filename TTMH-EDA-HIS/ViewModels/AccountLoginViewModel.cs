using System.ComponentModel.DataAnnotations;

namespace TTMH_EDA_HIS.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required]
        public string Account { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
