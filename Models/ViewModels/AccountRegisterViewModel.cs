using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Required, 
            DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required, 
            DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Required, 
            DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password),
            Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
