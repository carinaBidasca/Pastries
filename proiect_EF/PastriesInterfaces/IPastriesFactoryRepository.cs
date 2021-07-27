using PastriesCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastriesInterfaces
{
    public interface IPastriesFactoryRepository
    {
        
        Task<PastriesFactory> GetByIdAsync(int id);

        Task<IEnumerable<PastriesFactory>> GetAsync(int pageNumber, int pageSize);


        Task<PastriesFactory> AddAsync(PastriesFactory entity);

       
        Task<bool> UpdateAsync(PastriesFactory updatedEntity);

      
        Task<bool> RemoveAsync(int id);
        Task<Product> AddProductToFactory(int factoryId, Product product);
    }
}
