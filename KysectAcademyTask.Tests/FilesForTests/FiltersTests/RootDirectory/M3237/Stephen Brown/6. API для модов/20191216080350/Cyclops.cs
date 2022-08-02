namespace Laba_v2
{
    public class Cyclop : Unit
    {
        public Cyclop() : base("Cyclop", 85, 20, 15, (18, 26), 10)
        {
            Features.Add(Feature.Shooter);
        }
    }
}