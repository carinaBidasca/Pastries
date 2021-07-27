using PastriesCommon.DTOs;
using PastriesCommon.Util;
using PastriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastriesServices
{
    public class IngredientServiceAsync : IIngredientServiceAsync
    {
        
        private readonly IIngredientRepositoryAsync _ingredientRepositoryAsync;
        public IngredientServiceAsync(IIngredientRepositoryAsync ingredientRepositoryAsync)
        {
            _ingredientRepositoryAsync = ingredientRepositoryAsync;
        }

        /// <summary>
        /// Add the new ingredient to the collection.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IngredientDto> AddAsync(IngredientDto entity)
        {
            var result = _ingredientRepositoryAsync.AddAsync(entity.ToEntity());
            return result.Result.ToDto();
        }

        public async Task<ProductDto> AddProductToIngredient(int ingredientId, ProductDto product)
        {
            var result = _ingredientRepositoryAsync.AddProductToIngredient(ingredientId, product.ToEntity());
            return result.Result.ToDto();
        }

        /// <summary>
        /// Get all ingredients, in a paged format.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IngredientDto>> GetAsync(int pageNumber = 1, int pageSize = 10)
        {
            var results = _ingredientRepositoryAsync.GetAsync(pageNumber, pageSize).Result.ToList();
            return results.Any() ? results.Select(x => x.ToDto()) : new List<IngredientDto>();
        }

        /// <summary>
        /// Get the ingredient by  id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<IngredientDto> GetByIdAsync(int id)
        {
            return _ingredientRepositoryAsync.GetByIdAsync(id)?.Result.ToDto();
        }

        public async Task<IEnumerable<IngredientDto>> GetByQuantityAsync(int quantity)
        {
            var results= _ingredientRepositoryAsync.GetByQuantityAsync(quantity).Result.ToList();
            return results.Any() ? results.Select(x => x.ToDto()) : new List<IngredientDto>();

        }

        /// <summary>
        /// Remove the ingredient with the given id from the collection.
        /// Throws KeyNotFoundException if the entity cannot be found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(int id)
        {
            return _ingredientRepositoryAsync.RemoveAsync(id).Result;
        }

        /// <summary>
        /// Update the existing ingredient with the new values.
        /// Throws KeyNotFoundException if the entity cannot be found.
        /// </summary>
        /// <param name="updatedEntity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(IngredientDto updatedEntity)
        {
            return _ingredientRepositoryAsync.UpdateAsync(updatedEntity.ToEntity()).Result;
        }
    }
}
