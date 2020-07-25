using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required, 
            DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Required, 
            DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
