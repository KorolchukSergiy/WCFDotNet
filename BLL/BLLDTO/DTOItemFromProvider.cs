using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BllDTO
{
    public class DTOItemFromProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BuyPrice { get; set; }
        public byte[] Image { get; set; }
        public DTOProvider Provider { get; set; }
        public DTOProducer Producer { get; set; }
    }
}
