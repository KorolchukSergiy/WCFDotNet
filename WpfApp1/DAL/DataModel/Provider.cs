using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataModel
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ItemFromProvider> ItemFromProviders { get; set; }
        public virtual User User { get; set; }
    }
}
