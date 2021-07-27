using PastriesCommon.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastriesInterfaces
{
    public interface IProductServiceAsync
    {
        Task<ProductDto> GetByIdAsync(int id);

        Task<IEnumerable<ProductDto>> GetAsync(int pageNumber, int pageSize);


        Task<ProductDto> AddAsync(ProductDto entity);


        Task<bool> UpdateAsync(ProductDto updatedEntity);


        Task<bool> RemoveAsync(int id);
        Task<IngredientDto> AddIngredientToProduct(int productId, IngredientDto ingredient);
    }
}
