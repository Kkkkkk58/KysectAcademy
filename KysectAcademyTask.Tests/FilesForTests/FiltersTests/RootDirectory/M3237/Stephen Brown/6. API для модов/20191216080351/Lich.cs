namespace Laba_v2
{
    public class Lich : Unit
    {
        public Lich() : base("Lich", 50, 15, 15, (12, 17), 10)
        {
            Spells.Add(Spell.Resurrection);
            Features.Add(Feature.Cosset);
            Features.Add(Feature.Shooter);
        }
    }
}