using System;
using System.Collections.Generic;
using System.Text;

namespace Laba
{
    public class BattleUnitsStack
    {
        private string Name;
        private int Count;
        private int firstHp;
        private double Hp;
        private int maxHp;
        private double Attack;
        private double Defence;
        private (int, int) Damage;
        private double Init;
        private int Status = 0;
        private bool DedInside;

        private List<(int id, Time t, Value v)> Buff = new List<(int, Time, Value)>();
       // private List<(int id, int time, double value)> Debuff = new List<(int, int, double)>();

        public IList<(int id, Time t, Value v)> buff { get { return Buff.ToArray(); } }
        //public IList<(int id, int time, double value)> debuff { get { return Debuff.ToArray(); } }

        public void Add_Buff(int id, int count)
        {
            Console.Clear();
            Console.WriteLine($"На ютина {Name} были наложены чары");
            Console.ReadKey();
            Console.Clear();

            if (id == 1)
            {

                Buff.Add((1, new Time(-1), new Value(12)));
                attack += 12;
            }
            else if (id == 2)
            {
                Buff.Add((2, new Time(-1), new Value(Math.Min(12, Attack))));
                attack -= 12;
            }
            else if (id == 3)
            {
                Buff.Add((3, new Time(-1), new Value(Math.Min(12, Defence))));
                defence -= 12;
            }
            else if (id == 4)
            {
                Buff.Add((4, new Time(-1), new Value(Init * 0.4)));
                Init *= 1.4;
            }
            else if (id == 5)
            {
                hp += 100 * count;
            }
            else if (id == 6)
            {
                Buff.Add((6, new Time(1), new Value(Defence * 0.3)));
                Defence *= 1.3;
            }
            else if (id == 7)
            {
                Buff.Add((7, new Time(10), new Value(0)));
            }
        }

        public void Del_Buff(int ind)
        {
            if (Buff[ind].id == 1)
            {
                attack -= Buff[ind].v.alue;
                Buff.RemoveAt(ind);
            }
            else if (Buff[ind].id == 2)
            {
                attack += Buff[ind].v.alue;
                Buff.RemoveAt(ind);
            }
            else if (Buff[ind].id == 3)
            {
                defence += Buff[ind].v.alue;
                Buff.RemoveAt(ind);
            }
            else if (Buff[ind].id == 4)
            {
                init -= Buff[ind].v.alue;
                Buff.RemoveAt(ind);
            }
            else if (Buff[ind].id == 5)
            {
            }
            else if (Buff[ind].id == 6)
            {
                defence -= Buff[ind].v.alue;
                Buff.RemoveAt(ind);
            } 
            else if (Buff[ind].id == 7)
            {
                Buff.RemoveAt(ind);
            }
        }

        public void Check(int ind)
        {
            if (Buff[ind].id == 7)
            {
                Buff[ind].v.alue += Math.Min(10, Hp);

                hp -= 10;
                Console.WriteLine($"На юнита {Name} действует яд и отнимает 10 hp");
            }
        }

        public string name { get { return Name; } }
        public bool dedinside { get { return DedInside; } }

        public int count
        {
            get { return Count; }
            set
            {
                if (value >= 0)
                    Count = value;
                else
                    Console.WriteLine("Stack is empty");
            }
        }

        public int status { get { return Status; } set { Status = value; } }
        // 0 - жив
        // 1 - ждет
        // 2 - умер

        public double hp
        {
            get { return Hp; }
            set
            {
                Hp = Math.Min(Math.Max(0, value), maxHp);

                if ((status == 2) && (Hp > 0))
                    status = 0;

                Count = (int)Math.Ceiling(Hp / firstHp);
            }
        }

        public double attack

        {
            get { return Attack; }
            set { Attack = Math.Max(0, value); }
        }
        public int damage
        {
            get
            {
                Random rnd = new Random();
                return rnd.Next(Damage.Item1, Damage.Item2);
            }
        }
        public double defence
        {
            get { return Defence; }
            set { Defence = Math.Max(0, value); }
        }

        public double init
        {
            get { return Init; }
            set { Init = Math.Max(0, value); }
        }

        public BattleUnitsStack(UnitsStack x)
        {
            Name = x.heroes.name;
            Count = x.count;
            firstHp = x.heroes.hp;
            Hp = x.heroes.hp * x.count;
            maxHp = x.heroes.hp * x.count;
            Attack = x.heroes.attack;
            Defence = x.heroes.defence;
            Damage = x.heroes.damage;
            Init = x.heroes.init;
            DedInside = x.heroes.dedinside;
        }
    }
}
