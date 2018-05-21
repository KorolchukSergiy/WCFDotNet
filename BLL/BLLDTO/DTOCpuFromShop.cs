using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BllDTO
{
    public class DTOCpuFromShop : DTOItemFromShop
    {
        public string CpuSocket { get; set; }
        public int Frequency { get; set; }
        public int Core { get; set; }
        public int Threads { get; set; }
        public string Cash { get; set; }
        public string Video { get; set; }
    }
}
