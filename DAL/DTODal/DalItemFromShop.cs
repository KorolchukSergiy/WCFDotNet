using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTODal
{
    public class DalItemFromShop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Producer { get; set; }
        public decimal SalaryPrice { get; set; }
        public byte[] Image { get; set; }
    }
}
