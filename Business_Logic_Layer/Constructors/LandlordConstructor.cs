using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Constructors
{
    public class LandlordConstructor
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Введите название организации")]
        public string OrganizationName { get; set; }
    }
}
