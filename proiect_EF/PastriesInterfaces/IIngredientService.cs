using PastriesCommon.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PastriesInterfaces
{
    public interface IIngredientService
    {
       
        IngredientDto GetById(int id);

      
        IEnumerable<IngredientDto> Get(int pageNumber = 1, int pageSize = 10);

       
        IngredientDto Add(IngredientDto entity);

        
        bool Update(IngredientDto updatedEntity);

       
        bool Remove(int id);
    }
}
