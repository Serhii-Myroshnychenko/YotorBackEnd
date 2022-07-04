using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Entities
{
    public partial class Restriction
    {
        public Restriction()
        {
            Booking = new HashSet<Booking>();
        }

        public Guid RestrictionId { get; set; }
        public Guid LandlordId { get; set; }
        public string CarName { get; set; }
        public string Description { get; set; }

        public virtual Landlord Landlord { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
    }
}
