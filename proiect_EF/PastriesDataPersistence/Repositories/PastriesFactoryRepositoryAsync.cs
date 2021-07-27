using Microsoft.EntityFrameworkCore;
using PastriesCommon.Entities;
using PastriesData;
using PastriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastriesDataPersistence.Repositories
{
    public class PastriesFactoryRepositoryAsync : IPastriesFactoryRepository
    {
        private readonly PastriesDbContext _context;
        public PastriesFactoryRepositoryAsync(PastriesDbContext pastriesDbContext)
        {
            _context = pastriesDbContext;
        }
        public async Task<PastriesFactory> AddAsync(PastriesFactory entity)
        {
            await _context.AddAsync(entity);

            var entities = _context.ChangeTracker.Entries();

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Product> AddProductToFactory(int factoryId, Product product)
        {
            var existingFactory = await _context.PastriesFactories
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == factoryId);

            existingFactory.Products.Add(product);

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<PastriesFactory>> GetAsync(int pageNumber, int pageSize)
        {
            var result = await _context.PastriesFactories
                .IgnoreQueryFilters().Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
                

            var entities = _context.ChangeTracker.Entries();

            return result;
        }

        public async Task<PastriesFactory> GetByIdAsync(int id)
        {
            var result = await _context.PastriesFactories
               .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var existingItem = await _context.PastriesFactories
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingItem == null)
            {
                return false;
            }

            _context.Remove(existingItem);

            var entities = _context.ChangeTracker.Entries();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(PastriesFactory updatedEntity)
        {
            var existingItem = await _context.PastriesFactories
                .FirstOrDefaultAsync(x => x.Id == updatedEntity.Id);

            if (existingItem == null)
            {
                return false;
            }

           // _context.Entry(existingItem).CurrentValues.SetValues(updatedEntity);
            existingItem.Name = updatedEntity.Name;
            existingItem.Address = updatedEntity.Address;
            //existingItem.Size = updatedEntity.Size;

            var entities = _context.ChangeTracker.Entries();

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
