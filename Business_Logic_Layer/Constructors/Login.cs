using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Constructors
{
    public class Login
    {
        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
    }
}
