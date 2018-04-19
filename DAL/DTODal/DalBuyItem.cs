using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTODal
{
    public class DalBuyItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DalProducer Producer { get; set; }
        public int Quantity { get; set; }
        public DateTime TimeBuy { get; set; }
        public DalItemFromProvider DalItemFromProvider { get; set; }
        public DalProvider DalProvider { get; set; }
    }
}
