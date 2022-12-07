using EcommerceApi.DAL.DataContext;
using EcommerceApi.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceApi.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private EcommerceContext context;
        protected DbSet<T> table;
        public GenericRepository(EcommerceContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public async Task<bool> DeleteAsync(object id)
        {
            T existing = await table.FindAsync(id);
            if (existing == null)
            {
                return false;
            }
            table.Remove(existing);
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await table.ToListAsync();

        public async Task<T> GetByIdAsync(object id) => await table.FindAsync(id);

        public async Task InsertAsync(T obj)
        {
            await table.AddAsync(obj);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }
    }
}
