namespace Laba_v2
{
    public class Angel : Unit
    {
        public Angel() : base("Angel", 180, 27, 27, (45, 45) , 11)
        {
            Spells.Add(Spell.Punishing_blow);
            Spells.Add(Spell.Persuasion);
            Features.Add(Feature.Sage);
        }
    }
}