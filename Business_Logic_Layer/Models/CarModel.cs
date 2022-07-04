using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class CarModel
    {
        public Guid CarId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Year { get; set; }
        public string Transmission { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public bool Status { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
    }
}
