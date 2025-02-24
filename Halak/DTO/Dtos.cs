namespace Halak.Dtos
{
    public class HalakWithTavakDto
    {
        public int Id { get; set; }
        public string? Nev { get; set; }
        public string? Faj { get; set; }
        public string? TavakNev { get; set; }
    }

    public class HorgaszokFogasokDto
    {
        public string? HorgaszNev { get; set; }
        public string? HalNev { get; set; }
        public DateTime Datum { get; set; }
    }

    public class LegnagyobbHalDto
    {
        public string? Nev { get; set; }
        public int MeretCm { get; set; }
    }
}