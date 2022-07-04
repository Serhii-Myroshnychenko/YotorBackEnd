using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class RestrictionModel
    {
        public Guid RestrictionId { get; set; }
        public Guid LandlordId { get; set; }
        public string CarName { get; set; }
        public string Description { get; set; }
    }
}
