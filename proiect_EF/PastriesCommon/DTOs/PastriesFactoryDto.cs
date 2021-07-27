using PastriesCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastriesCommon.DTOs
{
    public class PastriesFactoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Size Size { get; set; }
    }
}
