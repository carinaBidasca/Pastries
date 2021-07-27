using PastriesCommon.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastriesInterfaces
{
    public interface IPastriesFactoryService
    {
        Task<PastriesFactoryDto> GetByIdAsync(int id);

        Task<IEnumerable<PastriesFactoryDto>> GetAsync(int pageNumber, int pageSize);


        Task<PastriesFactoryDto> AddAsync(PastriesFactoryDto entity);


        Task<bool> UpdateAsync(PastriesFactoryDto updatedEntity);


        Task<bool> RemoveAsync(int id);
        Task<ProductDto> AddProductToFactory(int factoryId, ProductDto product);
    }
}
