namespace Laba_v2
{
    public class Spirit : Unit
    {
        public Spirit() : base("Spirit", 100, 10, 0, (30, 50), 1)
        {
            Spells.Add(Spell.Treacherous);
            Spells.Add(Spell.Invisibility);
            Spells.Add(Spell.Suicide);
            Features.Add(Feature.Fiery);
            Features.Add(Feature.Icy);
            Features.Add(Feature.Antidote);
        }
    }
}