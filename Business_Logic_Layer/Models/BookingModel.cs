using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class BookingModel
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
    }
}
