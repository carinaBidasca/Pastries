using PastriesCommon.Entities.InterfacesEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PastriesCommon.Entities
{
    public class Product:IRestrictedCascadeDelete,ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PastriesFactoryId { get; set; }
        public PastriesFactory PastriesFactory { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
