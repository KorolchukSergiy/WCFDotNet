using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataModel
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeSale { get; set; }
        public ItemFromShop ItemFromShop { get; set; }
    }
}
