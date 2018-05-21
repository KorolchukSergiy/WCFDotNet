using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BllDTO
{
    public class DTOSaleItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeSale { get; set; }
        public DTOItemFromShop ItemFromShop { get; set; }
    }
}
