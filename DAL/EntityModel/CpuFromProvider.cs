namespace DAL
{
    public class CpuFromProvider:ItemFromProvider
    {
        public string CpuSocket { get; set; }
        public int Frequency { get; set; }
        public int Core { get; set; }
        public int Threads { get; set; }
        public string Cash { get; set; }
        public string Video { get; set; }
    }
}