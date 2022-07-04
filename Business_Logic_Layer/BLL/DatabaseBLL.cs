using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.BLL
{
    public class DatabaseBLL
    {
        private readonly Data_Access_Layer.Configuration.IUnitOfWork _iUnitOfWorkDAL;

        public DatabaseBLL()
        {
            _iUnitOfWorkDAL = new Data_Access_Layer.Configuration.UnitOfWork();
        }

        public async Task<string> CreateBackupAsync(Guid userId)
        {
            try
            {
                Random rnd = new Random();
                int value = rnd.Next(0, 100000);
                string path = $"C:\\Program Files\\Microsoft SQL Server\\MSSQL14.SQLEXPRESS\\MSSQL\\Backup\\YotorDatabase{value}.bak";
                bool isAdmin = await _iUnitOfWorkDAL.Customers.IsAdminAsync(userId);
                if (isAdmin == true)
                {
                    await _iUnitOfWorkDAL.Backups.CreateBackupAsync(path);
                    await _iUnitOfWorkDAL.Backups.InsertBackupToDbAsync(path);
                    return "Ok";
                }
                return "Недостаточно прав";
            }
            catch (Exception ex) { return ex.Message; }

        }
        public async Task<string> RestoreDatabaseByLastBackupAsync()
        {
            try
            {
                var lastBackup = await _iUnitOfWorkDAL.Backups.GetLastBackupAsync();
                if (lastBackup != null)
                {
                    await _iUnitOfWorkDAL.Backups.RestoreDatabaseBySomeBackupAsync(lastBackup.FirstOrDefault());
                    return "Ok";
                }
                return "Что-то пошло не так";
            }
            catch (Exception ex) { return ex.Message; }

        }
        public async Task<string> RestoreDatabaseBySomeBackupAsync(Guid id)
        {
            try
            {
                var backup = await _iUnitOfWorkDAL.Backups.GetByIdAsync(id);
                if (backup != null)
                {
                    await _iUnitOfWorkDAL.Backups.RestoreDatabaseBySomeBackupAsync(backup);
                    return "Ok";
                }
                return "Что-то пошло не так";
            }
            catch (Exception ex) { return ex.Message; }
        }

    }
}
