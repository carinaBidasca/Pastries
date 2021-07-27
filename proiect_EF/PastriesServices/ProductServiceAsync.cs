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
    public class ProductServiceAsync:IProductServiceAsync
    {

        private readonly IProductRepositoryAsync _productRepositoryAsync;
        public ProductServiceAsync(IProductRepositoryAsync productRepositoryAsync)
        {
            _productRepositoryAsync = productRepositoryAsync;
        }

        public async Task<ProductDto> AddAsync(ProductDto entity)
        {
            var result = _productRepositoryAsync.AddAsync(entity.ToEntity());
            return result.Result.ToDto();
        }

        public async Task<IngredientDto> AddIngredientToProduct(int productId, IngredientDto ingredient)
        {
            var result = _productRepositoryAsync.AddIngredientToProduct(productId, ingredient.ToEntity());
            return result.Result.ToDto();
        }

        public async Task<IEnumerable<ProductDto>> GetAsync(int pageNumber, int pageSize)
        {
            var results = _productRepositoryAsync.GetAsync(pageNumber, pageSize).Result.ToList();
            return results.Any() ? results.Select(x => x.ToDto()) : new List<ProductDto>();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return _productRepositoryAsync.GetByIdAsync(id)?.Result.ToDto();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return _productRepositoryAsync.RemoveAsync(id).Result;
        }

        public async Task<bool> UpdateAsync(ProductDto updatedEntity)
        {
            return _productRepositoryAsync.UpdateAsync(updatedEntity.ToEntity()).Result;
        }
    }
}
