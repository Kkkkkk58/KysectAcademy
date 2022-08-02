namespace Laba_v2
{
    public class Hydra : Unit
    {
        public Hydra() : base("Hydra", 80, 15, 12, (7, 14), 7)
        {
            Features.Add(Feature.Splash);
            Features.Add(Feature.Oppression);
        }
    }
}