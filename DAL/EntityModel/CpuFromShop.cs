using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CpuFromShop : ItemFromShop
    {
        public string CpuSocket { get; set; }
        public int Frequency { get; set; }
        public int Core { get; set; }
        public int Threads { get; set; }
        public string Cash { get; set; }
        public string Video { get; set; }
    }
}
