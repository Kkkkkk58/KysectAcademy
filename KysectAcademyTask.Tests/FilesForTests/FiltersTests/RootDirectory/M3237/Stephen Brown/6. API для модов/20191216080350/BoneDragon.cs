namespace Laba_v2
{
    public class BoneDragon : Unit
    {
        public BoneDragon() : base("BoneDragon", 150, 27, 28, (15, 30), 11)
        {
            Spells.Add(Spell.Curse);
            Spells.Add(Spell.Arcon);
            Features.Add(Feature.Cosset);
            Features.Add(Feature.Sage);
            Features.Add(Feature.Fiery);
        }
    }
}