namespace Laba_v2
{
    public class Griffin : Unit
    {
        public Griffin() : base("Griffin", 30, 7, 5, (5, 10), 15)
        {
            Features.Add(Feature.Persistence);
        }
    }
}