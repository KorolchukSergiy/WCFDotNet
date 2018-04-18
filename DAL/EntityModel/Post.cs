using System.Collections.Generic;
namespace DAL
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Salary { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}