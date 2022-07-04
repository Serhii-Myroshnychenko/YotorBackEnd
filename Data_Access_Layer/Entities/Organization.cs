using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Entities
{
    public partial class Organization
    {
        public Organization()
        {
            Car = new HashSet<Car>();
            Landlord = new HashSet<Landlord>();
        }

        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
        public string Taxes { get; set; }
        public string Address { get; set; }
        public string Founder { get; set; }
        public string Account { get; set; }

        public virtual ICollection<Car> Car { get; set; }
        public virtual ICollection<Landlord> Landlord { get; set; }
    }
}
