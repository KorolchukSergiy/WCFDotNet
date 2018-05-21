using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DAL.DataModel
{
    public class ItemFromShop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Producer Producer { get; set; }
        public decimal SalaryPrice { get; set; }
        public BitmapImage Image { get; set; }
    }
}
