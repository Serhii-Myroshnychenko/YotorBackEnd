using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Constructors
{
    public class RestrictionConstructor
    {
        [Required(ErrorMessage = "Введите id пользователя")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Введите название машини")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите описание машини")]
        public string Description { get; set; }
    }
}
