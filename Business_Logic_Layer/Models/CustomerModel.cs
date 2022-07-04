using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class CustomerModel
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public byte[] Photo { get; set; }
        public byte[] Passport { get; set; }
        public byte[] DriversLicense { get; set; }
        public bool IsAdmin { get; set; }
    }
}
