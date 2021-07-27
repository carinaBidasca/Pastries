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
    public class IngredientRepositoryAsync : IIngredientRepositoryAsync
    {
        // private IList<Ingredient> _ingredients;
        private readonly PastriesDbContext _context;
        public IngredientRepositoryAsync(PastriesDbContext pastriesDbContext)
        {
            _context = pastriesDbContext;
            //_ingredients = new List<Ingredient>();
           /* Ingredient i1 = new Ingredient { Id = 1, Name = "sugar", Details = "brown sugar-less sweet", Quantity = 2 };
            Ingredient i2 = new Ingredient { Id = 2, Name = "chocolate", Details = "dark-min 70% cacao", Quantity = 2 };
            Ingredient i3 = new Ingredient { Id = 3, Name = "vanilla", Details = "strong flavour", Quantity = 2 };
            Ingredient i4 = new Ingredient { Id = 4, Name = "oil", Details = "olive oil", Quantity = 2 };
            Ingredient i5 = new Ingredient { Id = 5, Name = "fruits", Details = "fresh forest fruits for cakes", Quantity = 2 };
            _ingredients.Add(i1);
            _ingredients.Add(i2);
            _ingredients.Add(i3);
            _ingredients.Add(i4);
            _ingredients.Add(i5);*/
        }

        /// <summary>
        /// Add the new ingredient to the collection.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Ingredient> AddAsync(Ingredient entity)
        {//ajunge cu id=0
            /*List<int> ids = new List<int>();
            foreach (var elem in _ingredients)
                ids.Add(elem.Id);
            entity.Id = ids.Max() + 1;
            
            _ingredients.Add(entity);

            return entity;*/
            await _context.AddAsync(entity);

            var entities = _context.ChangeTracker.Entries();

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Product> AddProductToIngredient(int ingredientId, Product product)
        {
            var existingIngredient = await _context.Ingredients
                .Include(x =>x.Products )
                .FirstOrDefaultAsync(x => x.Id == ingredientId);

            existingIngredient.Products.Add(product);

            await _context.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Get all ingredients, in a paged format.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Ingredient>> GetAsync(int pageNumber, int pageSize)
        {//puteam sa pun direct list in loc de ienumerable:)
            /*return _ingredients
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);*/
            var result = await _context.Ingredients
              .IgnoreQueryFilters().Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            //.AsNoTracking()
            //.Include(x => x.Products)


            //await _context.Products.LoadAsync();

            var entities = _context.ChangeTracker.Entries();

            return result;
        }

        /// <summary>
        /// Get the ingredient by  id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Ingredient> GetByIdAsync(int id)
        {
            //return _ingredients.FirstOrDefault(x => x.Id == id);
            var result = await _context.Ingredients
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        /// <summary>
        /// Get a collection of ingredients with the same given quantity.
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Ingredient>> GetByQuantityAsync(int quantity)
        {
            //return _ingredients.Where(x => x.Quantity == quantity);
            var result = await _context.Ingredients
                .Where(x => x.Quantity == quantity)
                .ToListAsync();
            return result;
        }

        /// <summary>
        /// Remove the ingredient with the given id from the collection.
        /// Throws KeyNotFoundException if the entity cannot be found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(int id)
        {
            /* var existingEntity = _ingredients.FirstOrDefault(x => x.Id == id);
             if (existingEntity == null)
                 throw new KeyNotFoundException($"Item with given id does not exist");

             return _ingredients.Remove(existingEntity);*/
            var existingItem = await _context.Ingredients
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

        /// <summary>
        /// Update the existing ingredient with the new values.
        /// Throws KeyNotFoundException if the entity cannot be found.
        /// </summary>
        /// <param name="updatedEntity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Ingredient updatedEntity)
        {//ingredient vine cu id setat si verificat

            /*var existingEntity = _ingredients.FirstOrDefault(x => x.Id == updatedEntity.Id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Item with given id does not exist");

            existingEntity.Name = updatedEntity.Name;
            existingEntity.Quantity = updatedEntity.Quantity;
            existingEntity.Details = updatedEntity.Details;

            return true;*/
            var existingItem = await _context.Ingredients
               .FirstOrDefaultAsync(x => x.Id == updatedEntity.Id);

            if (existingItem == null)
            {
                return false;
            }

           // _context.Entry(existingItem).CurrentValues.SetValues(updatedEntity);
            existingItem.Name = updatedEntity.Name;
            existingItem.Quantity = updatedEntity.Quantity;
            existingItem.Details = updatedEntity.Details;

            var entities = _context.ChangeTracker.Entries();

            await _context.SaveChangesAsync();

            return true;

        }
    }
}
