using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Entities
{
    public partial class Booking
    {
        public Guid BookingId { get; set; }
        public Guid? RestrictionId { get; set; }
        public Guid UserId { get; set; }
        public Guid CarId { get; set; }
        public Guid? FeedbackId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public int FullPrice { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }

        public virtual Car Car { get; set; }
        public virtual Feedback Feedback { get; set; }
        public virtual Restriction Restriction { get; set; }
        public virtual Customer User { get; set; }
    }
}
