namespace Laba_v2
{
    public class Devil : Unit
    {
        public Devil() : base("Devil", 166, 27, 25, (36, 66), 11)
        {
            Spells.Add(Spell.Weakening);
            Spells.Add(Spell.Persuasion);
            Spells.Add(Spell.Curse);
            Spells.Add(Spell.Treacherous);
            Features.Add(Feature.Sage);
        }
    }
}