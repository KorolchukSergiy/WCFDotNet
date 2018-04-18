using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MotherBoardFromShop : ItemFromShop
    {
        public string MBSocket { get; set; }
        public string ChipSet { get; set; }
        public string PciE { get; set; }
        public string RAM { get; set; }
        public string USB { get; set; }
    }
}
