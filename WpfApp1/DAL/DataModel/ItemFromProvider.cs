using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DAL.DataModel
{
    public class ItemFromProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Producer Producer { get; set; }
        public virtual Provider Provider { get; set; }
        public decimal BuyPrice { get; set; }
        public BitmapImage Image { get; set; }
    }
}
