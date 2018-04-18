using System.Collections.Generic;
namespace DAL
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ItemFromProvider> ItemFromProviders { get; set; }
        public virtual ICollection<ItemFromShop> ItemFromShops { get; set; }
    }
}