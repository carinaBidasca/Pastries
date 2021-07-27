using System;
using System.ComponentModel.DataAnnotations;

namespace tema3.Models
{
    public class IngredientModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Length must be at least 2 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Quantity is required")]
        public int Quantity { get; set; }

        public string Details { get; set; }//nu are si lista de produse

    }
}
