using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Constructors
{
    public class OrganizationConstructor
    {
        [Required(ErrorMessage = "Введите название организации")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите почту организации")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите телефон организации")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Введите код организации")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Введите налоги организации")]
        public string Taxes { get; set; }
        [Required(ErrorMessage = "Введите адресс организации")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Введите создателя организации")]
        public string Founder { get; set; }
        [Required(ErrorMessage = "Введите счёт организации")]
        public string Account { get; set; }
    }
}
