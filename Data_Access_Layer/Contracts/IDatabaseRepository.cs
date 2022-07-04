using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Contracts
{
    public interface IDatabaseRepository : IGenericRepository<Backup>
    {
        Task CreateBackupAsync(string path);
        Task InsertBackupToDbAsync(string path);
        Task RestoreDatabaseBySomeBackupAsync(Backup backup);
        Task<IEnumerable<Backup>> GetLastBackupAsync();
    }
}
