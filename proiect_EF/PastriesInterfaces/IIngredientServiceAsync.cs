using PastriesCommon.DTOs;
using PastriesCommon.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PastriesInterfaces
{
    public interface IIngredientServiceAsync
    {
        //nume parram=cel din fct

        ///<summary>
        ///get the ingredient by id
        ///</summary>
        ///<param name="id"></param>
        ///<returns>task</returns>
        Task<IngredientDto> GetByIdAsync(int id);

        /// <summary>
        /// Get all ingredients, in a paged format.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<IngredientDto>> GetAsync(int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// Add the new ingredient to the collection.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<IngredientDto> AddAsync(IngredientDto entity);

        /// <summary>
        /// Update the existing ingredient with the new values.
        /// Throws KeyNotFoundException if the entity cannot be found.
        /// </summary>
        /// <param name="updatedEntity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(IngredientDto updatedEntity);

        /// <summary>
        /// Remove the Ingredient with the given id from the collection.
        /// Throws KeyNotFoundException if the entity cannot be found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(int id);
        Task<IEnumerable<IngredientDto>> GetByQuantityAsync(int quantity);
        Task<ProductDto> AddProductToIngredient(int ingredientId, ProductDto product);


    }
}
