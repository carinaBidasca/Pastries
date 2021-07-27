using PastriesCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastriesInterfaces
{
    public interface IProductRepositoryAsync
    {
        Task<Product> GetByIdAsync(int id);

        Task<IEnumerable<Product>> GetAsync(int pageNumber, int pageSize);


        Task<Product> AddAsync(Product entity);


        Task<bool> UpdateAsync(Product updatedEntity);


        Task<bool> RemoveAsync(int id);
        Task<Ingredient> AddIngredientToProduct(int productId, Ingredient ingredient);
    }
}
