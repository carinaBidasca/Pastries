
using PastriesCommon.DTOs;
using System;
using tema3.Models;

namespace tema3.Utils
{
    public static class MapperUtil
    {//mapari dto to model
        
        public static IngredientModel ToModel(this IngredientDto dto)
        {
            return new IngredientModel
            {
                Id = (int)(dto.Id ?? null),
                Name = dto.Name,
                Quantity = dto.Quantity,
                Details = dto.Details,
               
            };
        }

        public static ProductModel ToModel(this ProductDto dto)
        {
            //dto are mereu id,poate fi 0
            return new ProductModel
            {
                Id = dto.Id,
                Name = dto.Name,
                PastriesFactoryId=dto.PastriesFactoryId

            };
        }
        public static PastriesFactoryModel ToModel(this PastriesFactoryDto dto)
        {
            return new PastriesFactoryModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Address=dto.Address,
                Size=dto.Size

            };
        }


        public static IngredientDto ToDto(this IngredientModel model,int? id)
        {
            
            return new IngredientDto
            {
                Id = id,
                Name = model.Name,
                Quantity = model.Quantity,
                Details = model.Details
            };
        }
        public static IngredientDto ToDto(this IngredientModel model, int? id,int id_user)
        {//verif pt add
            if (!id.HasValue)
            {
                return new IngredientDto
                {
                    Id = 0,
                    Name = model.Name,
                    Quantity = model.Quantity,
                    Details = model.Details,
                    Id_user = id_user
                };
            }
            
            return new IngredientDto
            {
                Id = id,
                Name = model.Name,
                Quantity = model.Quantity,
                Details = model.Details,
                Id_user=id_user
            };
        }
        public static ProductDto ToDto(this ProductModel model, int? id)
        {//verif pt add
            if (!id.HasValue)
            {
                return new ProductDto
                {
                    Id = 0,
                    Name = model.Name,
                    PastriesFactoryId=model.PastriesFactoryId
                };
            }

            return new ProductDto
            {
                Id = (int)id,
                Name = model.Name,
                PastriesFactoryId=model.PastriesFactoryId
            };
        }

        public static PastriesFactoryDto ToDto(this PastriesFactoryModel model, int? id)
        {//verif pt add
            if (!id.HasValue)
            {
                return new PastriesFactoryDto
                {
                    Id = 0,
                    Name = model.Name,
                    Address=model.Address,
                    Size=model.Size
                };
            }

            return new PastriesFactoryDto
            {
                Id = (int)id,
                Name = model.Name,
                Address=model.Address,
                Size=model.Size
            };
        }


    }
}
