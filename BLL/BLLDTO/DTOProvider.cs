using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BllDTO
{
    public class DTOProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DTOUser DTOUser { get; set; }
        public List<DTOItemFromProvider> DTOItemFromProviders { get; set; }
    }
}
