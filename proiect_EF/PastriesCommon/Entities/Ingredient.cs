using System;
using System.Collections.Generic;
using System.Text;

namespace PastriesCommon.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Quantity { get; set; }
        public int Id_user { get; set; }
        public List<Product> Products { get; set; }
       
    }
}
