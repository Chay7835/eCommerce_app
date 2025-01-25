using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Dal
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        private readonly EcommerceDbContext _context;
        private DbSet<T> _dbSet;    

        // Injection of the DBContext happens here
        public CommonRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return 0;
            }
            // Set refers to the DbSet
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetDetailAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> InsertAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            // Hover on the function and check its functionality
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return await _context.SaveChangesAsync();   
        }
    }
}
