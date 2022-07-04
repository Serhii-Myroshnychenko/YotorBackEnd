using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class FeedbackModel
    {
        public Guid FeedbackId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Text { get; set; }
    }
}
