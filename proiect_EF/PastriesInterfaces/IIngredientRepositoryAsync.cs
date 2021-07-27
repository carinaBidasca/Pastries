using PastriesCommon.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PastriesInterfaces
{
    public interface IIngredientRepositoryAsync
    {
        /// <summary>
        /// Get the ingredient by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Ingredient> GetByIdAsync(int id);

        /// <summary>
        /// Get all ingredients, in a paged format.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<Ingredient>> GetAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Get a collection of ingredients with the same given quantity
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<IEnumerable<Ingredient>> GetByQuantityAsync(int quantity);

       
        /// <summary>
        /// Add the new ingredient to the collection.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Ingredient> AddAsync(Ingredient entity);

        /// <summary>
        /// Update the existing ingredient with the new values.
        /// Throws KeyNotFoundException if the entity cannot be found.
        /// </summary>
        /// <param name="updatedEntity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Ingredient updatedEntity);

        /// <summary>
        /// Remove the ingredient with the given id.
        /// Throws KeyNotFoundException if the entity cannot be found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(int id);
        Task<Product> AddProductToIngredient(int  ingredientId, Product product);
    }
}
