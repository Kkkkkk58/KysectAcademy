namespace Laba_v2
{
    public class Arbalester : Unit
    {
        public Arbalester() : base("Arbalester", 10, 4, 4, (2, 8), 8)
        {
            Features.Add(Feature.Shooter);
            Features.Add(Feature.Precise_shot);
        }
    }
}