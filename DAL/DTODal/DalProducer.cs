using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTODal
{
    public class DalProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<DalItemFromProvider> ItemFromProviders { get; set; }
        public virtual List<DalItemFromShop> ItemFromShops { get; set; }
    }
}
