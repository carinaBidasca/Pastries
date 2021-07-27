
using PastriesCommon.Entities;

namespace tema3.Models
{
    public class PastriesFactoryModel
    {//id se da obligat,poate fi 0
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Size Size { get; set; }
        //nu am lista produse,am size,pe care in swagger il dau ca int

    }
}
