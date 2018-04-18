using System.Collections.Generic;

namespace DAL
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual Post Post { get; set; }
        public bool Online { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}