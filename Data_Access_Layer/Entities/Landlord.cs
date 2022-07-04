using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Entities
{
    public partial class Landlord
    {
        public Landlord()
        {
            Restriction = new HashSet<Restriction>();
        }

        public Guid LandlordId { get; set; }
        public Guid UserId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual ICollection<Restriction> Restriction { get; set; }
    }
}
