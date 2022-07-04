using System;
using System.Collections.Generic;

namespace Data_Access_Layer.Entities
{
    public partial class Backup
    {
        public Guid BackupId { get; set; }
        public string Path { get; set; }
    }
}
