using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SaleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeSale { get; set; }
        public virtual ItemFromShop ItemFromShop { get; set; }
    }
}
