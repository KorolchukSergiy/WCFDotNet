namespace DAL
{
    public class MotherBoardFromProvider:ItemFromProvider
    {
        public string MBSocket { get; set; }
        public string ChipSet { get; set; }
        public string PciE { get; set; }
        public string RAM { get; set; }
        public string USB { get; set; }
    }
}