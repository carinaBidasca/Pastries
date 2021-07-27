using PastriesCommon.DTOs;
using PastriesCommon.Entities;
using System;
using System.Collections.Generic;

namespace PastriesCommon.Util
{
    public static class MapperUtil
    {//mapari dto-entitate
        public static Ingredient ToEntity(this IngredientDto dto)
        {
            if (dto.Id == 0)
            {
                return new Ingredient
                {
                    Id=0,
                    //Id = (int)(dto.Id ?? null),
                    Name = dto.Name,
                    Details = dto.Details,
                    Quantity = dto.Quantity
                };
            }
            return new Ingredient
            {
                //nu ajunge null
                Id = (int)(dto.Id ?? null),
                Name = dto.Name,
                Details = dto.Details,
                Quantity = dto.Quantity
            };
        }
        public static Product ToEntity(this ProductDto dto)
        {
            if (dto.Id == 0)
            {
                return new Product
                {
                    Id = 0,
                    Name = dto.Name,
                    PastriesFactoryId = dto.PastriesFactoryId,
                    Ingredients = new List<Ingredient>(),
                    PastriesFactory=null
                };
            }
            return new Product
            {
                //nu ajunge null
                Id = (int)(dto.Id),
                Name = dto.Name,
                PastriesFactoryId = dto.PastriesFactoryId,
                Ingredients = new List<Ingredient>(),
                PastriesFactory = null
            };
        }
        public static PastriesFactory ToEntity(this PastriesFactoryDto dto)
        {
            if (dto.Id == 0)
            {
                return new PastriesFactory
                {
                    Id = 0,
                    //Id = (int)(dto.Id ?? null),
                    Name = dto.Name,
                    Address=dto.Address,
                    Size=dto.Size,
                    Products=new List<Product>()
                };
            }
            return new PastriesFactory
            {
                //nu ajunge null
                Id = (int)(dto.Id),
                Name = dto.Name,
                Address = dto.Address,
                Size = dto.Size,
                Products = new List<Product>()
            };
        }

        public static IngredientDto ToDto(this Ingredient entity)
        {
            return new IngredientDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Quantity = entity.Quantity,
                Details = entity.Details
            };
        }
        public static ProductDto ToDto(this Product entity)
        {
            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                PastriesFactoryId=entity.PastriesFactoryId
            };
        }
        public static PastriesFactoryDto ToDto(this PastriesFactory entity)
        {
            return new PastriesFactoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
               Address=entity.Address,
               Size=entity.Size
            };
        }
    }
}
