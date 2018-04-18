namespace DAL
{
    public class Log
    {
        public int Id { get; set; }
        public string Logs { get; set; }
        public virtual  User User { get; set; }
    }
}