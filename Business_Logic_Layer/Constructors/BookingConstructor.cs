using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Constructors
{
    public class BookingConstructor
    {
        [Required(ErrorMessage = "Введите начальную дату")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Введите кончную дату")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Введите начальный адресс")]

        public string StartAddress { get; set; }
        [Required(ErrorMessage = "Введите конечный адресс")]
        public string EndAddress { get; set; }
        [Required(ErrorMessage = "Введите название машини")]
        public string CarName { get; set; }
        [Required(ErrorMessage = "Введите цену")]
        public int FullPrice { get; set; }
    }
}
