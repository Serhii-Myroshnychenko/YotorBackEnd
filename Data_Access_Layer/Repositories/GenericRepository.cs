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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected YotorDatabaseContext _yotorDatabase;
        internal DbSet<T> dbSet;

        public GenericRepository()
        {
            _yotorDatabase = new YotorDatabaseContext();
            dbSet = _yotorDatabase.Set<T>();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            dbSet.Remove(await dbSet.FindAsync(id));
            await _yotorDatabase.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await dbSet.FindAsync(id);
        }
    }
}
