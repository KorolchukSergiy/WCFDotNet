using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BuyItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeBuy { get; set; }
        public virtual ItemFromProvider ItemFromProvider { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
