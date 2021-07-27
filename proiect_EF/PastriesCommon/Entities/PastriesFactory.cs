using System;
using System.Collections.Generic;
using System.Text;

namespace PastriesCommon.Entities
{
    public class PastriesFactory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Size Size { get; set; }
        public List<Product> Products { get; set; }
    }
}
