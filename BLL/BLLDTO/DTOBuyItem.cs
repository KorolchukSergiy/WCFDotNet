using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BllDTO
{
    public class DTOBuyItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeBuy { get; set; }
        public DTOItemFromProvider ItemFromProvider { get; set; }
    }
}
