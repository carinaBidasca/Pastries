using PastriesCommon.DTOs;
using PastriesCommon.Util;
using PastriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PastriesServices
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

       
        public IngredientDto GetById(int id)
        {
            return _ingredientRepository.GetById(id)?.ToDto();
        }

       
        public IEnumerable<IngredientDto> Get(int pageNumber = 1, int pageSize = 100)
        {
            var results = _ingredientRepository.Get(pageNumber, pageSize).ToList();
            return results.Any() ? results.Select(x => x.ToDto()) : new List<IngredientDto>();
        }

        
        public IngredientDto Add(IngredientDto entity)
        {
            var result = _ingredientRepository.Add(entity.ToEntity());
            return result.ToDto();
        }

        
        public bool Update(IngredientDto updatedEntity)
        {
            return _ingredientRepository.Update(updatedEntity.ToEntity());
        }

       
        public bool Remove(int id)
        {
            return _ingredientRepository.Remove(id);
        }

      
    }
}
