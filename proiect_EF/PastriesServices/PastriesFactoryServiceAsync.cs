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
    public class PastriesFactoryServiceAsync:IPastriesFactoryService
    {

        private readonly IPastriesFactoryRepository _pastriesFactoryRepository;
        public PastriesFactoryServiceAsync(IPastriesFactoryRepository pastriesFactoryRepository)
        {
            _pastriesFactoryRepository = pastriesFactoryRepository;
        }

        public async Task<PastriesFactoryDto> AddAsync(PastriesFactoryDto entity)
        {
            var result = _pastriesFactoryRepository.AddAsync(entity.ToEntity());
            return result.Result.ToDto();
        }

        public async Task<ProductDto> AddProductToFactory(int factoryId, ProductDto product)
        {
            var result = _pastriesFactoryRepository.AddProductToFactory(factoryId, product.ToEntity());
            return result.Result.ToDto();
        }

        public async Task<IEnumerable<PastriesFactoryDto>> GetAsync(int pageNumber, int pageSize)
        {
            var results = _pastriesFactoryRepository.GetAsync(pageNumber, pageSize).Result.ToList();
            return results.Any() ? results.Select(x => x.ToDto()) : new List<PastriesFactoryDto>();
        }

        public async Task<PastriesFactoryDto> GetByIdAsync(int id)
        {
            return _pastriesFactoryRepository.GetByIdAsync(id)?.Result.ToDto();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return _pastriesFactoryRepository.RemoveAsync(id).Result;
        }

        public async Task<bool> UpdateAsync(PastriesFactoryDto updatedEntity)
        {
            return _pastriesFactoryRepository.UpdateAsync(updatedEntity.ToEntity()).Result;
        }
    }
}
