namespace Laba_v2
{
    public class Shaman : Unit
    {
        public Shaman() : base("Shaman", 40, 12, 10, (7, 12), 10.5)
        {
            Spells.Add(Spell.Acceleration);
            Spells.Add(Spell.Call);
        }
    }
}