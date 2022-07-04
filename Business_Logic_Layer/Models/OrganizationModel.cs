using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class OrganizationModel
    {
      
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
        public string Taxes { get; set; }
        public string Address { get; set; }
        public string Founder { get; set; }
        public string Account { get; set; }
    }
}
