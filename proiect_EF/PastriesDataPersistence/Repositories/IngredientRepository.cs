using PastriesCommon.Entities;
using PastriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PastriesDataPersistence.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private IList<Ingredient> _ingredients;

        public IngredientRepository()
        {
            _ingredients = new List<Ingredient>();
            Ingredient i1 = new Ingredient {Id= 1, Name="sugar", Details="brown sugar-less sweet", Quantity=2 };
            Ingredient i2 = new Ingredient {Id= 2, Name="chocolate", Details="dark-min 70% cacao", Quantity=2 };
            Ingredient i3 = new Ingredient {Id= 3, Name="vanilla", Details="strong flavour", Quantity=2 };
            Ingredient i4 = new Ingredient {Id= 4, Name="oil", Details="olive oil", Quantity=2 };
            Ingredient i5 = new Ingredient {Id= 5, Name="fruits", Details="fresh forest fruits for cakes", Quantity=2 };
            _ingredients.Add(i1);
            _ingredients.Add(i2);
            _ingredients.Add(i3);
            _ingredients.Add(i4);
            _ingredients.Add(i5);
        }

        
        public Ingredient GetById(int id)
        {
            return _ingredients.FirstOrDefault(x => x.Id == id);
        }

       
        public IEnumerable<Ingredient> Get(int pageNumber, int pageSize)
        {
            return _ingredients
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public IEnumerable<Ingredient> GetByQuantity(int quantity)
        {
            return _ingredients
                .Where(x => x.Quantity==quantity);
        }


        public Ingredient Add(Ingredient entity)
        {
            List<int> ids = new List<int>();
            foreach (var elem in _ingredients)
                ids.Add(elem.Id);
            entity.Id = ids.Max() + 1;
            _ingredients.Add(entity);

            return entity;
        }

       
        public bool Update(Ingredient updatedEntity)
        {
            var existingEntity = _ingredients.FirstOrDefault(x => x.Id == updatedEntity.Id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Item with given id does not exist");

            existingEntity.Name = updatedEntity.Name;
            existingEntity.Quantity = updatedEntity.Quantity;
            existingEntity.Details = updatedEntity.Details;

            return true;
        }

       
        public bool Remove(int id)
        {
            var existingEntity = _ingredients.FirstOrDefault(x => x.Id == id);
            if (existingEntity == null)
                throw new KeyNotFoundException($"Item with given id does not exist");

            return _ingredients.Remove(existingEntity);
        }

    }
}
