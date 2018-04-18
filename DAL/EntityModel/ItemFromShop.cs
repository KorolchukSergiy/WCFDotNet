using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ItemFromShop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public virtual Producer Producer { get; set; }
        public decimal SalaryPrice { get; set; }
        public byte[] Image { get; set; }
    }
}
