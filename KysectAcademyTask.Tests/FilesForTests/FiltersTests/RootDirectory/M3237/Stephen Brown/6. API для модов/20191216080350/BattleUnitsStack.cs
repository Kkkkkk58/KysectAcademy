using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_v2
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
        private int Global_Index;

        private List<Spell> Spells;
        private List<Feature> Features;
        private List<Info_Effect> Effects = new List<Info_Effect>();

        public IList<Info_Effect> effects { get { return Effects.ToArray(); } }
        public IList<Spell> spells { get { return Spells.ToArray(); } }
        public IList<Feature> features { get { return Features.ToArray(); } }
        
        public int global_index
        { get { return Global_Index; } }

        public string name 
        { get { return Name; } }

        public int count
        {
            get { return Count; }
            set
            {
                Count = Math.Max(value, 0);
            }
        }

        public double hp
        {
            get { return Hp; }
            set
            {
                Hp = Math.Min(Math.Max(0, value), maxHp);
                count = (int)Math.Ceiling(Hp / firstHp);
            }
        }

        public double maxhp
        {
            get
            {
                return maxHp;
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

        public (int, int) iidamage
        {
            get
            {
                return Damage;
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

            Global_Index = Global.index;

            Spells = new List<Spell>(x.heroes.spells);
            Features = new List<Feature>(x.heroes.features);
        }

        public BattleUnitsStack(BattleUnitsStack x)
        {
            Name = x.name;
            Count = x.count;
            firstHp = x.firstHp;
            Hp = x.hp;
            maxHp = x.maxHp;
            Attack = x.attack;
            Defence = x.defence;
            Damage = x.iidamage;
            Init = x.init;

            Global_Index = Global.index;

            Effects = new List<Info_Effect>(x.Effects);
            Spells = new List<Spell>(x.spells);
            Features = new List<Feature>(x.features);
        }

        public void Get_Spells()
        {
            for (int i = 0; i < Spells.Count; i++)
                Console.WriteLine($"{i}. {Spells[i]}");
        }

        public void Get_Features()
        {
            if (Features.Count != 0)
                Console.WriteLine("Features:");
            for (int i = 0; i < Features.Count; i++)
                Console.WriteLine(Features[i]);
        }

        public void Get_Effects()
        {
            for (int i = 0; i < Effects.Count; i++)
                Console.WriteLine($"Эффект: {Effects[i].name} Время : {Effects[i].time}");
        }

        public void Add_Effect(Effect num)
        {
            foreach (Info_Effect elem in Effects)
                if (elem.name == num)
                {
                    Console.WriteLine("This effect is already imposed");
                    return;
                }

            switch (num)
            {
                case Effect.Buff_Attack:
                    Effects.Add(new Info_Effect(Effect.Buff_Attack, 3, 12));
                    attack = attack + 12;
                    break;
                case Effect.Debuff_Attack:
                    Effects.Add(new Info_Effect(Effect.Debuff_Attack, 3, Math.Min(attack, 12)));
                    attack = attack - 12;
                    break;
                case Effect.Debuff_Defence:
                    Effects.Add(new Info_Effect(Effect.Debuff_Defence, 3, Math.Min(defence, 12)));
                    defence = defence - 12;
                    break;
                case Effect.Buff_Init:
                    Effects.Add(new Info_Effect(Effect.Buff_Init, 1, init * 0.4));
                    init = init * 1.4;
                    break;
                case Effect.Poison:
                    Effects.Add(new Info_Effect(Effect.Poison, 5, Math.Min(init, 5)));
                    init = init - 5;
                    break;
                case Effect.Hotly:
                    Effects.Add(new Info_Effect(Effect.Hotly, 5, Math.Min(defence, 5)));
                    defence = defence - 5;
                    break;
                case Effect.Cold:
                    Effects.Add(new Info_Effect(Effect.Cold, 5, Math.Min(attack, 5)));
                    attack = attack - 5;
                    break;
                case Effect.Stupor:
                    Effects.Add(new Info_Effect(Effect.Stupor, 3, 0));
                    break;
                case Effect.Naked:
                    Effects.Add(new Info_Effect(Effect.Naked, 3, defence));
                    defence = 0;
                    attack = attack + 10;
                    break;
                case Effect.Invisible_man:
                    Effects.Add(new Info_Effect(Effect.Invisible_man, 3, 0));
                    break;
                case Effect.Protection:
                    Effects.Add(new Info_Effect(Effect.Protection, 1, defence * 0.3));
                    defence = defence * 1.3;
                    break;
                default:
                    Console.WriteLine("Error while trying to add effect");
                    break;
            }
        }

        public void Del_Effect(Effect num)
        {
            int index = -1;

            for (int i = 0; i < Effects.Count; i++)
                if (Effects[i].name == num)
                    index = i;

            if (index == -1)
            {
                Console.WriteLine("This effect is not imposed on the unit");
                return;
            }

            switch (num)
            {
                case Effect.Buff_Attack:
                    attack = attack - 12;
                    Effects.RemoveAt(index);
                    break;
                case Effect.Debuff_Attack:
                    attack = attack + Effects[index].value;
                    Effects.RemoveAt(index);
                    break;
                case Effect.Debuff_Defence:
                    defence = defence + Effects[index].value;
                    Effects.RemoveAt(index);
                    break;
                case Effect.Buff_Init:
                    init = init - Effects[index].value;
                    Effects.RemoveAt(index);
                    break;
                case Effect.Poison:
                    init = init + Effects[index].value;
                    Effects.RemoveAt(index);
                    break;
                case Effect.Hotly:
                    defence = defence + Effects[index].value;
                    Effects.RemoveAt(index);
                    break;
                case Effect.Cold:
                    attack = attack + Effects[index].value;
                    Effects.RemoveAt(index);
                    break;
                case Effect.Stupor:
                    Effects.RemoveAt(index);
                    break;
                case Effect.Naked:
                    defence = defence + Effects[index].value;
                    attack = attack - 10;
                    Effects.RemoveAt(index);
                    break;
                case Effect.Invisible_man:
                    Effects.RemoveAt(index);
                    break;
                case Effect.Protection:
                    defence = defence - Effects[index].value;
                    Effects.RemoveAt(index);
                    break;
                default:
                    Console.WriteLine("Error while trying to add effect");
                    break;
            }
        }

        public bool Have_Features(Feature num)
        {
            foreach (Feature ind in Features)
                if (ind == num)
                    return true;

            return false;
        }
    }
}
