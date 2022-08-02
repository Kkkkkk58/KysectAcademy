namespace Laba_v2
{
    public class Fury : Unit
    {
        public Fury() : base("Fury", 16, 5, 3, (5, 7), 16)
        {
            Features.Add(Feature.Oppression);
        }
    }
}