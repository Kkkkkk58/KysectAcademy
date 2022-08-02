using System;
using System.Collections.Generic;
using System.Text;

namespace Laba
{
    public class Unit
    {
        private string Name;
        private int Hp;
        private double Attack;
        private double Defence;
        private (int, int) Damage;
        private double Init;
        private bool DedInside;

        public string name { get { return Name; } }
        public int hp { get { return Hp; } }
        public double attack { get { return Attack; } }
        public double defence { get { return Defence; } }
        public (int, int) damage { get { return Damage; } }
        public double init { get { return Init; } }
        public bool dedinside { get { return DedInside; } }

        public Unit(string Name, int Hp, int Attack, int Defence, (int, int) Damage, double Init, bool DedInside)
        {
            this.Name = Name;
            this.Hp = Hp;
            this.Attack = Attack;
            this.Defence = Defence;
            this.Damage.Item1 = Damage.Item1;
            this.Damage.Item2 = Damage.Item2;
            this.Init = Init;
            this.DedInside = DedInside;
        }
        public Unit(Unit x)
        {
            this.Name = x.Name;
            this.Hp = x.Hp;
            this.Attack = x.Attack;
            this.Defence = x.Defence;
            this.Damage.Item1 = x.Damage.Item1;
            this.Damage.Item2 = x.Damage.Item2;
            this.Init = x.Init;
        }
        public Unit()
        { }
    }
}
