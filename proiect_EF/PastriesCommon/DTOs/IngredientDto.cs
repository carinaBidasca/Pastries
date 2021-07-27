using System;
using System.Collections.Generic;
using System.Text;

namespace PastriesCommon.DTOs
{
    public class IngredientDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Details { get; set; }
        public int? Id_user { get; set; }//fara lista produse
       
    }
}
