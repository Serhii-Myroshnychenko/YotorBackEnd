using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class BackupModel
    {
        public Guid BackupId { get; set; }
        public string Path { get; set; }
    }
}
