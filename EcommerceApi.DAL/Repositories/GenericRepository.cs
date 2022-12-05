using EcommerceApi.DAL.DataContext;
using EcommerceApi.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApi.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private EcommerceContext context;
        private DbSet<T> table;
        public GenericRepository(EcommerceContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public async Task DeleteAsync(object id)
        {
            T existing = await table.FindAsync(id);
            table.Remove(existing);
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await table.ToListAsync();

        public async Task<T> GetByIdAsync(object id) => await table.FindAsync(id);

        public virtual async Task InsertAsync(T obj)
        {
            await table.AddAsync(obj);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(object id, T obj)
        {
            //Check if entry 
            var entry = await table.FindAsync(id);
            if (entry == null)
            {
                return;
            }
            entry = obj;
        }
    }
}
