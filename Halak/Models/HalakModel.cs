    namespace Halak.Models
{
    public class HalakModel
    {
        public required int id { get; set; }
        public required string nev { get; set; }
        public required string faj { get; set; }
        public required int to_id { get; set; }
        public required byte[] kep { get; set; }
        
    }
}