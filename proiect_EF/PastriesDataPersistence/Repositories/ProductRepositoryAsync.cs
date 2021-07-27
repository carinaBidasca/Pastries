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
    public class ProductRepositoryAsync : IProductRepositoryAsync
    {
        private readonly PastriesDbContext _context;
        public ProductRepositoryAsync(PastriesDbContext pastriesDbContext)
        {
            _context = pastriesDbContext;
        }
        public async Task<Product> AddAsync(Product entity)
        {
            await _context.AddAsync(entity);

            var entities = _context.ChangeTracker.Entries();

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Ingredient> AddIngredientToProduct(int productId, Ingredient ingredient)
        {
            var existingProduct = await _context.Products
                .Include(x => x.Ingredients)//.Include(x=>x.PastriesFactory)
                .FirstOrDefaultAsync(x => x.Id == productId);

            existingProduct.Ingredients.Add(ingredient);

            await _context.SaveChangesAsync();
            return ingredient;
        }

        public async Task<IEnumerable<Product>> GetAsync(int pageNumber, int pageSize)
        {
            var result = await _context.Products
               .IgnoreQueryFilters().Skip((pageNumber - 1) * pageSize)
               .Take(pageSize).ToListAsync();


            var entities = _context.ChangeTracker.Entries();

            return result;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var result = await _context.Products
              .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async  Task<bool> RemoveAsync(int id)
        {
            var existingItem = await _context.Products
               .Include(x => x.Ingredients)
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

        public async Task<bool> UpdateAsync(Product updatedEntity)
        {
            var existingItem = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == updatedEntity.Id);

            if (existingItem == null)
            {
                return false;
            }

            // _context.Entry(existingItem).CurrentValues.SetValues(updatedEntity);
            existingItem.Name = updatedEntity.Name;
          //  existingItem.PastriesFactoryId = updatedEntity.PastriesFactoryId;
             var entities = _context.ChangeTracker.Entries();

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
