using Data_Access_Layer.Contracts;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories
{
    public class DatabaseRepository : GenericRepository<Backup>, IDatabaseRepository
    {
        public DatabaseRepository() : base()
        {

        }

        public async Task CreateBackupAsync(string path)
        {
            await dbSet.FromSqlRaw($"exec CreateBackup @Path = '{path}'").ToListAsync();
        }

        public async Task<IEnumerable<Backup>> GetLastBackupAsync()
        {
            return await dbSet.FromSqlRaw("exec GetLastBackup").ToListAsync();
        }

        public async Task InsertBackupToDbAsync(string path)
        {
            var backup = new Backup();
            backup.BackupId = Guid.NewGuid();
            backup.Path = path;
            await dbSet.AddAsync(backup);
            await _yotorDatabase.SaveChangesAsync();
        }

        public async Task RestoreDatabaseBySomeBackupAsync(Backup backup)
        {
            await dbSet.FromSqlRaw($"use YotorDatabase alter database YotorDatabase set single_user with rollback immediate use master RESTORE DATABASE YotorDatabase FROM DISK = 'C:\\Program Files\\Microsoft SQL Server\\MSSQL14.SQLEXPRESS\\MSSQL\\Backup\\YotorDatabase18921.bak' WITH REPLACE,RECOVERY").ToListAsync();
        }
    }
}   
