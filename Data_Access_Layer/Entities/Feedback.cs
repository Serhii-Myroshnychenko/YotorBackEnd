using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Entities
{
    public partial class Feedback
    {
        public Feedback()
        {
            Booking = new HashSet<Booking>();
        }

        public Guid FeedbackId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Text { get; set; }

        public virtual Customer User { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
    }
}
