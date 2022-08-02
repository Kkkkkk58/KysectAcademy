using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_v2
{
    public class Unit
    {
        private string Name;
        private int Hp;
        private double Attack;
        private double Defence;
        private (int, int) Damage;
        private double Init;

        protected List<Spell> Spells = new List<Spell>();
        protected List<Feature> Features = new List<Feature>();
        
        public IList<Spell> spells { get { return Spells.ToArray(); } }
        public IList<Feature> features { get { return Features.ToArray(); } }
        public string name { get { return Name; } }
        public int hp { get { return Hp; } }
        public double attack { get { return Attack; } }
        public double defence { get { return Defence; } }
        public (int, int) damage { get { return Damage; } }
        public double init { get { return Init; } }

        public Unit(string Name, int Hp, int Attack, int Defence, (int, int) Damage, double Init)
        {
            this.Name = Name;
            this.Hp = Hp;
            this.Attack = Attack;
            this.Defence = Defence;
            this.Damage.Item1 = Damage.Item1;
            this.Damage.Item2 = Damage.Item2;
            this.Init = Init;
        }

        public Unit(Unit x)
        {
            Name = x.name;
            Hp = x.hp;
            Attack = x.attack;
            Defence = x.defence;
            Damage.Item1 = x.damage.Item1;
            Damage.Item2 = x.damage.Item2;
            Init = x.init;

            Spells = new List<Spell>(x.spells);
            Features = new List<Feature>(x.features);
        }

        public Unit()
        { }

        public void Get_Spells()
        {
            for (int i = 0; i < Spells.Count; i++)
                Console.WriteLine(Spells[i]);
        }

        public void Get_Features()
        {
            for (int i = 0; i < Features.Count; i++)
                Console.WriteLine(Features[i]);
        }
    }
}
