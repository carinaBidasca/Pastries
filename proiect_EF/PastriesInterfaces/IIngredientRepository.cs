using PastriesCommon.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PastriesInterfaces
{
    public interface IIngredientRepository
    {
        
        Ingredient GetById(int id);

        //un findAll paginat
        IEnumerable<Ingredient> Get(int pageNumber, int pageSize);


       //get by cantitate
        IEnumerable<Ingredient> GetByQuantity(int quantity);

       
        Ingredient Add(Ingredient entity);

        
        // Update the existing ingredient  with the new values.
        // Throws KeyNotFoundException if the entity cannot be found.
        
        bool Update(Ingredient updatedEntity);

        
        // Remove the ingredient with the given id from the collection.
        // Throws KeyNotFoundException if the entity cannot be found.
        
        bool Remove(int id);
    }
}
