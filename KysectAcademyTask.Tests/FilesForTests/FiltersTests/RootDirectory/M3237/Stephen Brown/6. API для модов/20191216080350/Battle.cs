using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_v2
{
    public class Battle
    {
        private int round = 0;

        private BattleArmy Player1;
        private BattleArmy Player2;

        public BattleArmy player1
        { get { return Player1; } }
        public BattleArmy player2
        { get { return Player2; } }

        public Battle(BattleArmy player1, BattleArmy player2)
        {
            Player1 = new BattleArmy(player1);
            Player2 = new BattleArmy(player2);
        }
        public Battle(Battle x)
        {
            Player1 = new BattleArmy(x.player1);
            Player2 = new BattleArmy(x.player2);
        }

        private My_Queue Queue = new My_Queue();

        public void Queue_Created()
        {
            Queue.Clear();

            for (int i = 0; i < Player1.arm.Count; i++)
                Queue.Add(new Zhdun(Player1.arm[i].global_index, 1, Player1.arm[i].init));

            for (int i = 0; i < Player2.arm.Count; i++)
                Queue.Add(new Zhdun(Player2.arm[i].global_index, 2, Player2.arm[i].init));

            Queue.Sort();
        }

        public void ShowStats()
        {
            Console.WriteLine("Player 1 stats:");
            Console.WriteLine();
            Player1.Show();
            Console.WriteLine("Player 2 stats:");
            Console.WriteLine();
            Player2.Show();
            Console.ReadKey();
            Console.Clear();
        }

        public (int, int) Info_Global_Index(int Global_Index)
        {
            for (int i = 0; i < Player1.arm.Count; i++)
                if (Player1.arm[i].global_index == Global_Index)
                    return (1, i);

            for (int i = 0; i < Player2.arm.Count; i++)
                if (Player2.arm[i].global_index == Global_Index)
                    return (2, i);

            return (0, 0);
        }

        public void Attack(int Global_Index1, int Global_Index2)
        {
            (int, int) x1 = Info_Global_Index(Global_Index1);
            (int, int) x2 = Info_Global_Index(Global_Index2);

            if (x1.Item1 == 1)
            {
                double x1_Attack = Player1.arm[x1.Item2].attack;
                double x1_Damage = Player1.arm[x1.Item2].damage;
                double x2_Defence = Player2.arm[x2.Item2].defence;

                if (Player1.arm[x1.Item2].Have_Features(Feature.Precise_shot))
                    x2_Defence = 0;

                double Final_Damage;
                if (x1_Attack > x2_Defence)
                    Final_Damage = Player1.arm[x1.Item2].count * x1_Damage * (1 + 0.05 * (x1_Attack - x2_Defence));
                else
                    Final_Damage = Player1.arm[x1.Item2].count * x1_Damage / (1 + 0.05 * (x2_Defence - x1_Attack));

                Console.WriteLine($"Юниту {Player2.arm[x2.Item2].name} второй команды было нанесено {Math.Min(Player2.arm[x2.Item2].hp, Final_Damage)} урона");
                
                if (Player1.arm[x1.Item2].Have_Features(Feature.Vampirism))
                {
                    Console.WriteLine($"Юнит {Player1.arm[x1.Item2].name} первой команды, благодаря вампиризму, восстановил {Math.Min(Math.Min(Player2.arm[x2.Item2].hp, Final_Damage) / 10, Player1.arm[x1.Item2].maxhp - Player1.arm[x1.Item2].hp)} хп");
                    Player1.arm[x1.Item2].hp = Player1.arm[x1.Item2].hp + Math.Min(Player2.arm[x2.Item2].hp, Final_Damage) / 10;
                }

                Player2.arm[x2.Item2].hp = Player2.arm[x2.Item2].hp - Final_Damage;
                Console.WriteLine($"У {Player2.arm[x2.Item2].name} второй команды осталось {Player2.arm[x2.Item2].hp} жизней");

                if (Player2.arm[x2.Item2].hp == 0)
                {
                    Console.WriteLine("Юнит умер");
                    Player2.Del(x2.Item2);
                    Queue.Del(Queue.Convert_Global_Index_to_Local_Index(Global_Index2));
                }
                else if ((Player2.arm[x2.Item2].Have_Features(Feature.Shooter) == false) &&
                         (Player1.arm[x1.Item2].Have_Features(Feature.Oppression) == false) &&
                         (Player1.arm[x1.Item2].Have_Features(Feature.Shooter) == false) &&
                        ((Queue.Revenge(Queue.Convert_Global_Index_to_Local_Index(Global_Index2)) == false) ||
                         (Player2.arm[x2.Item2].Have_Features(Feature.Persistence))))
                {
                    Queue.Revenge(Queue.Convert_Global_Index_to_Local_Index(Global_Index2), true);
                    double x2_Attack = Player2.arm[x2.Item2].attack;
                    double x2_Damage = Player2.arm[x2.Item2].damage;
                    double x1_Defence = Player1.arm[x1.Item2].defence;

                    if (Player2.arm[x2.Item2].Have_Features(Feature.Precise_shot))
                        x1_Defence = 0;

                    double Final_Damage_2;
                    if (x2_Attack > x1_Defence)
                        Final_Damage_2 = Player2.arm[x2.Item2].count * x2_Damage * (1 + 0.05 * (x2_Attack - x1_Defence));
                    else
                        Final_Damage_2 = Player2.arm[x2.Item2].count * x2_Damage / (1 + 0.05 * (x1_Defence - x2_Attack));

                    Console.WriteLine($"Юнит {Player2.arm[x2.Item2].name} совершает ответную атаку");
                    Console.WriteLine($"Юниту {Player1.arm[x1.Item2].name} первой команды было нанесено {Math.Min(Player1.arm[x1.Item2].hp, Final_Damage_2)} урона");
                    
                    if (Player2.arm[x2.Item2].Have_Features(Feature.Vampirism))
                    {
                        Console.WriteLine($"Юнит {Player2.arm[x2.Item2].name} второй команды, благодаря вампиризму, восстановил {Math.Min(Math.Min(Player1.arm[x1.Item2].hp, Final_Damage_2) / 10, Player2.arm[x2.Item2].maxhp - Player2.arm[x2.Item2].hp)} хп");
                        Player2.arm[x2.Item2].hp = Player2.arm[x2.Item2].hp + Math.Min(Player1.arm[x1.Item2].hp, Final_Damage_2) / 10;
                    }

                    Player1.arm[x1.Item2].hp = Player1.arm[x1.Item2].hp - Final_Damage_2;

                    Console.WriteLine($"У {Player1.arm[x1.Item2].name} первой команды осталось {Player1.arm[x1.Item2].hp} жизней");

                    if (Player1.arm[x1.Item2].hp == 0)
                    {
                        Console.WriteLine("Юнит умер");
                        Player1.Del(x1.Item2);
                        Queue.Del(Queue.Convert_Global_Index_to_Local_Index(Global_Index1));
                    }
                }
            }
            else if (x1.Item1 == 2)
            {
                double x1_Attack = Player2.arm[x1.Item2].attack;
                double x1_Damage = Player2.arm[x1.Item2].damage;
                double x2_Defence = Player1.arm[x2.Item2].defence;

                if (Player2.arm[x1.Item2].Have_Features(Feature.Precise_shot))
                    x2_Defence = 0;

                double Final_Damage;
                if (x1_Attack > x2_Defence)
                    Final_Damage = Player2.arm[x1.Item2].count * x1_Damage * (1 + 0.05 * (x1_Attack - x2_Defence));
                else
                    Final_Damage = Player2.arm[x1.Item2].count * x1_Damage / (1 + 0.05 * (x2_Defence - x1_Attack));

                Console.WriteLine($"Юниту {Player1.arm[x2.Item2].name} первой команды было нанесено {Math.Min(Player1.arm[x2.Item2].hp, Final_Damage)} урона");
                
                if (Player2.arm[x1.Item2].Have_Features(Feature.Vampirism))
                {
                    Console.WriteLine($"Юнит {Player2.arm[x1.Item2].name} второй команды, благодаря вампиризму, восстановил {Math.Min(Math.Min(Player1.arm[x2.Item2].hp, Final_Damage) / 10, Player2.arm[x1.Item2].maxhp - Player2.arm[x1.Item2].hp)} хп");
                    Player2.arm[x1.Item2].hp = Player2.arm[x1.Item2].hp + Math.Min(Player1.arm[x2.Item2].hp, Final_Damage) / 10;
                }

                Player1.arm[x2.Item2].hp = Player1.arm[x2.Item2].hp - Final_Damage;
                Console.WriteLine($"У {Player1.arm[x2.Item2].name} первой команды осталось {Player1.arm[x2.Item2].hp} жизней");

                if (Player1.arm[x2.Item2].hp == 0)
                {
                    Console.WriteLine("Юнит умер");
                    Player1.Del(x2.Item2);
                    Queue.Del(Queue.Convert_Global_Index_to_Local_Index(Global_Index2));
                }
                else if ((Player1.arm[x2.Item2].Have_Features(Feature.Shooter) == false) &&
                         (Player2.arm[x1.Item2].Have_Features(Feature.Oppression) == false) &&
                         (Player2.arm[x1.Item2].Have_Features(Feature.Shooter) == false) &&
                        ((Queue.Revenge(Queue.Convert_Global_Index_to_Local_Index(Global_Index2)) == false) ||
                         (Player1.arm[x2.Item2].Have_Features(Feature.Persistence))))
                {
                    Queue.Revenge(Queue.Convert_Global_Index_to_Local_Index(Global_Index2), true);
                    double x2_Attack = Player1.arm[x2.Item2].attack;
                    double x2_Damage = Player1.arm[x2.Item2].damage;
                    double x1_Defence = Player2.arm[x1.Item2].defence;

                    if (Player1.arm[x2.Item2].Have_Features(Feature.Precise_shot))
                        x1_Defence = 0;

                    double Final_Damage_2;
                    if (x2_Attack > x1_Defence)
                        Final_Damage_2 = Player1.arm[x2.Item2].count * x2_Damage * (1 + 0.05 * (x2_Attack - x1_Defence));
                    else
                        Final_Damage_2 = Player1.arm[x2.Item2].count * x2_Damage / (1 + 0.05 * (x1_Defence - x2_Attack));

                    Console.WriteLine($"Юнит {Player1.arm[x2.Item2].name} совершает ответную атаку");
                    Console.WriteLine($"Юниту {Player2.arm[x1.Item2].name} первой команды было нанесено {Math.Min(Player2.arm[x1.Item2].hp, Final_Damage_2)} урона");
                    
                    if (Player1.arm[x2.Item2].Have_Features(Feature.Vampirism))
                    {
                        Console.WriteLine($"Юнит {Player1.arm[x2.Item2].name} второй команды, благодаря вампиризму, восстановил {Math.Min(Math.Min(Player2.arm[x1.Item2].hp, Final_Damage_2) / 10, Player1.arm[x2.Item2].maxhp - Player1.arm[x2.Item2].hp)} хп");
                        Player1.arm[x2.Item2].hp = Player1.arm[x2.Item2].hp + Math.Min(Player2.arm[x1.Item2].hp, Final_Damage_2) / 10;
                    }

                    Player2.arm[x1.Item2].hp = Player2.arm[x1.Item2].hp - Final_Damage_2;
                    Console.WriteLine($"У {Player2.arm[x1.Item2].name} второй команды осталось {Player2.arm[x1.Item2].hp} жизней");

                    if (Player2.arm[x1.Item2].hp == 0)
                    {
                        Console.WriteLine("Юнит умер");
                        Player2.Del(x1.Item2);
                        Queue.Del(Queue.Convert_Global_Index_to_Local_Index(Global_Index1));
                    }
                }
            }
            else
                Console.WriteLine("Could not find such a unit");
        }

        // заклинание на другого юнита
        public void Cast_Spell(Spell num, int Global_Index1, int Global_Index2)
        {
            (int, int) x1 = Info_Global_Index(Global_Index1);
            (int, int) x2 = Info_Global_Index(Global_Index2);

            switch (num)
            {
                case Spell.Punishing_blow:
                    if (x2.Item1 == 1)
                        Player1.arm[x2.Item2].Add_Effect(Effect.Buff_Attack);
                    else
                        Player2.arm[x2.Item2].Add_Effect(Effect.Buff_Attack);
                    break;
                case Spell.Curse:
                    if (x2.Item1 == 1)
                        Player1.arm[x2.Item2].Add_Effect(Effect.Debuff_Attack);
                    else
                        Player2.arm[x2.Item2].Add_Effect(Effect.Debuff_Attack);
                    break;
                case Spell.Weakening:
                    if (x2.Item1 == 1)
                        Player1.arm[x2.Item2].Add_Effect(Effect.Debuff_Defence);
                    else
                        Player2.arm[x2.Item2].Add_Effect(Effect.Debuff_Defence);
                    break;
                case Spell.Acceleration:
                    if (x2.Item1 == 1)
                    {
                        Player1.arm[x2.Item2].Add_Effect(Effect.Buff_Init);
                        Queue.Change_Init(Queue.Convert_Global_Index_to_Local_Index(Global_Index2), Player1.arm[x2.Item2].init);
                    }
                    else
                    {
                        Player2.arm[x2.Item2].Add_Effect(Effect.Buff_Init);
                        Queue.Change_Init(Queue.Convert_Global_Index_to_Local_Index(Global_Index2), Player2.arm[x2.Item2].init);
                    }
                    break;
                case Spell.Resurrection:
                    if (x1.Item1 == 1)
                        Player1.arm[x2.Item2].hp = Player1.arm[x2.Item2].hp + Player1.arm[x1.Item2].count * 100;
                    else
                        Player2.arm[x2.Item2].hp = Player2.arm[x2.Item2].hp + Player2.arm[x1.Item2].count * 100;
                    break;
                case Spell.Poisoning:
                    if (x2.Item1 == 1)
                        Player1.arm[x2.Item2].Add_Effect(Effect.Poison);
                    else
                        Player2.arm[x2.Item2].Add_Effect(Effect.Poison);
                    break;
                case Spell.Arcon:
                    if (x2.Item1 == 1)
                        Player1.arm[x2.Item2].Add_Effect(Effect.Hotly);
                    else
                        Player2.arm[x2.Item2].Add_Effect(Effect.Hotly);
                    break;
                case Spell.Freezing:
                    if (x2.Item1 == 1)
                        Player1.arm[x2.Item2].Add_Effect(Effect.Cold);
                    else
                        Player2.arm[x2.Item2].Add_Effect(Effect.Cold);
                    break;
                case Spell.Stun:
                    if (x2.Item1 == 1)
                        Player1.arm[x2.Item2].Add_Effect(Effect.Stupor);
                    else
                        Player2.arm[x2.Item2].Add_Effect(Effect.Stupor);
                    break;
                case Spell.Persuasion: // желательно изначально перед возможностью вызова чекать переполненность армии
                    if (x2.Item1 == 1)
                    {
                        Queue.Chenge_Player(Queue.Convert_Global_Index_to_Local_Index(Global_Index2), 2);
                        Player2.Add(Player1.arm[x2.Item2]);
                        Player1.Del(x2.Item2);
                    }
                    else
                    {
                        Queue.Chenge_Player(Queue.Convert_Global_Index_to_Local_Index(Global_Index2), 1);
                        Player1.Add(Player2.arm[x2.Item2]);
                        Player2.Del(x2.Item2);
                    }
                    break;
                case Spell.Treacherous:
                    if (x2.Item1 == 1)
                    {
                        double Damage = Math.Min(Math.Min(Player1.arm[x1.Item2].count * 10, Player1.arm[x1.Item2].maxhp - Player1.arm[x1.Item2].hp), Player1.arm[x2.Item2].hp);
                        Player1.arm[x1.Item2].hp = Player1.arm[x1.Item2].hp + Damage;
                        Player1.arm[x2.Item2].hp = Player1.arm[x2.Item2].hp - Damage;
                        Console.WriteLine($"Юнит {Player1.arm[x1.Item2].name} излечился на {Damage} хп");
                        Console.WriteLine($"Юниту {Player1.arm[x2.Item2].name} было нанесено {Damage} урона");
                        if (Player1.arm[x2.Item2].hp == 0)
                        {
                            Console.WriteLine("Юнит умер");
                            Player1.Del(x2.Item2);
                            Queue.Del(Queue.Convert_Global_Index_to_Local_Index(Global_Index2));
                        }
                    }
                    else
                    {
                        double Damage = Math.Min(Math.Min(Player2.arm[x1.Item2].count * 10, Player2.arm[x1.Item2].maxhp - Player2.arm[x1.Item2].hp), Player2.arm[x2.Item2].hp);
                        Player2.arm[x1.Item2].hp = Player2.arm[x1.Item2].hp + Damage;
                        Player2.arm[x2.Item2].hp = Player2.arm[x2.Item2].hp - Damage;
                        Console.WriteLine($"Юнит {Player2.arm[x1.Item2].name} излечился на {Damage} хп");
                        Console.WriteLine($"Юниту {Player2.arm[x2.Item2].name} было нанесено {Damage} урона");
                        if (Player2.arm[x2.Item2].hp == 0)
                        {
                            Console.WriteLine("Юнит умер");
                            Player2.Del(x2.Item2);
                            Queue.Del(Queue.Convert_Global_Index_to_Local_Index(Global_Index2));
                        }
                    }
                    break;
                case Spell.Suicide:
                    if (x2.Item1 == 1)
                    {
                        double Damage = Math.Min(Player1.arm[x2.Item2].hp, Player2.arm[x1.Item2].hp);
                        Player1.arm[x2.Item2].hp = Player1.arm[x2.Item2].hp - Damage;
                     
                        Console.WriteLine($"Юниту {Player1.arm[x2.Item2].name} первой команды было нанесено {Damage} урона");
                        Console.WriteLine($"Юниту {Player2.arm[x1.Item2].name} второй команды было нанесено {Player2.arm[x1.Item2].hp} урона");

                        Player2.arm[x1.Item2].hp = 0;

                        if (Player1.arm[x2.Item2].hp == 0)
                        {
                            Console.WriteLine($"Юнит {Player1.arm[x2.Item2].name} первой команды умер");
                            Player1.Del(x2.Item2);
                            Queue.Del(Queue.Convert_Global_Index_to_Local_Index(Global_Index2));
                        }

                        if (Player2.arm[x1.Item2].hp == 0)
                        {
                            Console.WriteLine($"Юнит {Player2.arm[x1.Item2].name} второй команды умер");
                            Player2.Del(x1.Item2);
                            Queue.Del(Queue.Convert_Global_Index_to_Local_Index(Global_Index1));
                        }

                    }
                    else
                    {
                        double Damage = Math.Min(Player2.arm[x2.Item2].hp, Player1.arm[x1.Item2].hp);
                        Player2.arm[x2.Item2].hp = Player2.arm[x2.Item2].hp - Damage;

                        Console.WriteLine($"Юниту {Player2.arm[x2.Item2].name} второй команды было нанесено {Damage} урона");
                        Console.WriteLine($"Юниту {Player1.arm[x1.Item2].name} первой команды было нанесено {Player1.arm[x1.Item2].hp} урона");

                        Player1.arm[x1.Item2].hp = 0;

                        if (Player2.arm[x2.Item2].hp == 0)
                        {
                            Console.WriteLine($"Юнит {Player2.arm[x2.Item2].name} второй команды умер");
                            Player2.Del(x2.Item2);
                            Queue.Del(Queue.Convert_Global_Index_to_Local_Index(Global_Index2));
                        }

                        if (Player1.arm[x1.Item2].hp == 0)
                        {
                            Console.WriteLine($"Юнит {Player1.arm[x1.Item2].name} первой команды умер");
                            Player1.Del(x1.Item2);
                            Queue.Del(Queue.Convert_Global_Index_to_Local_Index(Global_Index1));
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }



        }

        // заклинание на себя
        public void Cast_Spell(Spell num, int Global_Index) 
        {
            (int, int) x = Info_Global_Index(Global_Index);

            switch (num)
            {
                case Spell.Convenience:
                    if (x.Item1 == 1)
                        Player1.arm[x.Item2].Add_Effect(Effect.Naked);
                    else
                        Player2.arm[x.Item2].Add_Effect(Effect.Naked);
                    break;
                case Spell.Invisibility:
                    if (x.Item1 == 1)
                        Player1.arm[x.Item2].Add_Effect(Effect.Invisible_man);
                    else
                        Player2.arm[x.Item2].Add_Effect(Effect.Invisible_man);
                    break;
                case Spell.Call:  // желательно изначально перед возможностью вызова чекать переполненность армии 
                    Console.WriteLine("Введите кол-во духов");
                    int z = Convert.ToInt32(Console.ReadLine());
                    if (x.Item1 == 1)
                    {
                        Player1.Add(new BattleUnitsStack(new UnitsStack(new Spirit(), z)));
                        Queue.Add(new Zhdun(Player1.arm[Player1.arm.Count - 1].global_index, 1, Player1.arm[Player1.arm.Count - 1].init, false, true, false));
                    }               
                    else
                    {
                        Player2.Add(new BattleUnitsStack(new UnitsStack(new Spirit(), z)));
                        Queue.Add(new Zhdun(Player2.arm[Player2.arm.Count - 1].global_index, 2, Player2.arm[Player2.arm.Count - 1].init, false, true, false));
                    }
                    break;
            }
        }  

        public string Get_Name(int Global_Index)
        {
            foreach (BattleUnitsStack num in Player1.arm)
                if (num.global_index == Global_Index)
                    return num.name;

            foreach (BattleUnitsStack num in Player2.arm)
                if (num.global_index == Global_Index)
                    return num.name;

            return "";
        }

        public void Show_Queue()
        {
            for (int i = 0; i < Queue.line.Count; i++)
                Console.WriteLine($"{Queue.line[i].init} {Get_Name(Queue.line[i].global_index)} {Queue.line[i].global_index} {Queue.line[i].player} {Queue.line[i].wait} {Queue.line[i].move}");
        }

        public void Start()
        {
            try
            {
                Console.WriteLine("Битва началась");
                Console.WriteLine("");
                Console.WriteLine("Список юнитов первой команды:");
                Player1.Show();
                Console.WriteLine("Список юнитов второй команды:");
                Player2.Show();
                Console.ReadKey();
                Console.Clear();

                while (true)
                {
                    round++;

                    Queue_Created();

                    int Num, Num1, Num2;

                    while (true)
                    {
                        (int, int) x = Info_Global_Index(Queue.line[0].global_index);

                        if (x.Item1 == 1)
                        {
                        start1:
                            Console.Clear();
                            Show_Queue();
                            Console.WriteLine("");
                            Console.WriteLine($"Раунд {round}");
                            Console.WriteLine($"Ходит юнит {Player1.arm[x.Item2].name} первой команды");
                            Console.WriteLine("Выберите действие:");
                            Console.WriteLine("1.Атаковать");
                            Console.WriteLine("2.Использовать заклинание/способность");
                            Console.WriteLine("3.Ожидать");
                            Console.WriteLine("4.Обороняться");
                            Console.WriteLine("5.Сдаться");
                            Console.WriteLine("6.Статистика");

                            string s = Console.ReadLine();
                            if (s != "")
                                Num = Convert.ToInt32(s);
                            else
                            {
                                Console.WriteLine("Incorrect input, try again");
                                Console.ReadKey();
                                goto start1;
                            }
                            Console.Clear();
                            switch (Num)
                            {
                                case 1:
                                    Console.WriteLine("Выберите юнита, которого хотите атаковать:");
                                    Player2.Show();
                                    Console.WriteLine("Если хотите вернуться, введите -1");

                                    Num1 = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();

                                    if ((Num1 == -1) || (Num1 >= Player2.arm.Count))
                                        goto start1;

                                    Attack(Player1.arm[x.Item2].global_index, Player2.arm[Num1].global_index);
                                    Console.ReadKey();
                                    Queue.Moving(0);
                                    break;
                                case 2:
                                    Console.WriteLine("Выберите заклинание, которое хотите использовать:");
                                    Player1.arm[x.Item2].Get_Spells();
                                    if (Player1.arm[x.Item2].spells.Count == 0)
                                        Console.WriteLine("Похоже у данного юнита нет заклинаний :c");
                                    Console.WriteLine("Если хотите вернуться, введите -1");
                                    Console.WriteLine("Если вам нужен мануал, то введите -2");

                                    Num1 = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();

                                    if (Num1 == -2)
                                    {
                                        Console.Clear();
                                        Manual();
                                        Console.ReadKey();
                                        Console.Clear();
                                        goto start1;
                                    }


                                    if ((Num1 == -1) || (Num1 >= Player1.arm[x.Item2].spells.Count))
                                        goto start1;

                                    Console.Clear();
                                    switch (Player1.arm[x.Item2].spells[Num1])
                                    {
                                        case Spell.Punishing_blow:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player1.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index, Player1.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Curse:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player2.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index, Player2.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Weakening:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player2.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index, Player2.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Acceleration:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player1.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index, Player1.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Resurrection:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player1.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index, Player1.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Poisoning:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player2.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index, Player2.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Arcon:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player2.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index, Player2.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Freezing:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player2.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index, Player2.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Stun:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player2.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index, Player2.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Persuasion:
                                            if (Player1.Check_Full())
                                            {
                                                Console.WriteLine("К сожалению вы не можете использовать это заклинание т.к. ваша армия переполнена");
                                                Console.ReadKey();
                                                goto start1;
                                            }
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player2.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index, Player2.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Suicide:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player2.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index, Player2.arm[Num2].global_index);
                                            Console.ReadKey();
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Treacherous:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player1.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index, Player1.arm[Num2].global_index);
                                            Console.ReadKey();
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Convenience:
                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Invisibility:
                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Call:
                                            if (Player1.Check_Full())
                                            {
                                                Console.WriteLine("К сожалению вы не можете использовать это заклинание т.к. ваша армия переполнена");
                                                Console.ReadKey();
                                                goto start1;
                                            }
                                            Cast_Spell(Player1.arm[x.Item2].spells[Num1], Player1.arm[x.Item2].global_index);
                                            Queue.Moving(0);
                                            break;
                                    }
                                    break;
                                case 3:
                                    Queue.Waiting(0);
                                    break;
                                case 4:
                                    Player1.arm[x.Item2].Add_Effect(Effect.Protection);
                                    Queue.Moving(0);
                                    break;
                                case 5:
                                    Console.WriteLine("Первая команда сдалась");
                                    Console.WriteLine("Вторая команда одержала победу");
                                    if (round == 1)
                                        Console.WriteLine($"Сражение длилось {round} ранд");
                                    if ((round > 1) || (round < 5))
                                        Console.WriteLine($"Сражение длилось {round} ранда");
                                    if (round > 4)
                                        Console.WriteLine($"Сражение длилось {round} рандов");
                                    Console.ReadKey();
                                    Environment.Exit(0);
                                    break;
                                case 6:
                                    Console.Clear();
                                    Console.WriteLine("Список юнитов первой команды:");
                                    Player1.Show();
                                    Console.WriteLine("Список юнитов второй команды:");
                                    Player2.Show();
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                default:
                                    Console.WriteLine("Ошибочный ввод");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        else /////////////////////////////////////////////////////////////////////////////////
                        {
                        start2:
                            Console.Clear();
                            Show_Queue();
                            Console.WriteLine("");
                            Console.WriteLine($"Раунд {round}");
                            Console.WriteLine($"Ходит юнит {Player2.arm[x.Item2].name} второй команды");
                            Console.WriteLine("Выберите действие:");
                            Console.WriteLine("1.Атаковать");
                            Console.WriteLine("2.Использовать заклинание/способность");
                            Console.WriteLine("3.Ожидать");
                            Console.WriteLine("4.Обороняться");
                            Console.WriteLine("5.Сдаться");
                            Console.WriteLine("6.Статистика");

                            Num = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();

                            switch (Num)
                            {
                                case 1:
                                    Console.WriteLine("Выберите юнита, которого хотите атаковать:");
                                    Player1.Show();
                                    Console.WriteLine("Если хотите вернуться, введите -1");

                                    Num1 = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();

                                    if ((Num1 == -1) || (Num1 >= Player1.arm.Count))
                                        goto start2;

                                    Attack(Player2.arm[x.Item2].global_index, Player1.arm[Num1].global_index);
                                    Console.ReadKey();
                                    Queue.Moving(0);
                                    break;
                                case 2:
                                    Console.WriteLine("Выберите заклинание, которое хотите использовать:");
                                    Player2.arm[x.Item2].Get_Spells();
                                    if (Player2.arm[x.Item2].spells.Count == 0)
                                        Console.WriteLine("Похоже у данного юнита нет заклинаний :c");
                                    Console.WriteLine("Если хотите вернуться, введите -1");
                                    Console.WriteLine("Если вам нужен мануал, то введите -2");

                                    Num1 = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();

                                    if (Num1 == -2)
                                    {
                                        Console.Clear();
                                        Manual();
                                        Console.ReadKey();
                                        Console.Clear();
                                        goto start2;
                                    }

                                    if ((Num1 == -1) || (Num1 >= Player2.arm[x.Item2].spells.Count))
                                        goto start2;

                                    Console.Clear();
                                    switch (Player2.arm[x.Item2].spells[Num1])
                                    {
                                        case Spell.Punishing_blow:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player2.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index, Player2.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Curse:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player1.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index, Player1.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Weakening:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player1.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index, Player1.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Acceleration:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player2.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index, Player2.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Resurrection:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player2.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index, Player2.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Poisoning:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player1.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index, Player1.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Arcon:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player1.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index, Player1.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Freezing:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player1.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index, Player1.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Stun:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player1.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index, Player1.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Persuasion:
                                            if (Player2.Check_Full())
                                            {
                                                Console.WriteLine("К сожалению вы не можете использовать это заклинание т.к. ваша армия переполнена");
                                                Console.ReadKey();
                                                goto start2;
                                            }
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player1.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index, Player1.arm[Num2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Suicide:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player1.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index, Player1.arm[Num2].global_index);
                                            Console.ReadKey();
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Treacherous:
                                            Console.WriteLine("Выберите вашу цель:");
                                            Player2.Show();

                                            Num2 = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();

                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index, Player2.arm[Num2].global_index);
                                            Console.ReadKey();
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Convenience:
                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Invisibility:
                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index);
                                            Queue.Moving(0);
                                            break;
                                        case Spell.Call:
                                            if (Player2.Check_Full())
                                            {
                                                Console.WriteLine("К сожалению вы не можете использовать это заклинание т.к. ваша армия переполнена");
                                                Console.ReadKey();
                                                goto start2;
                                            }
                                            Cast_Spell(Player2.arm[x.Item2].spells[Num1], Player2.arm[x.Item2].global_index);
                                            Queue.Moving(0);
                                            break;
                                    }
                                    break;
                                case 3:
                                    Queue.Waiting(0);
                                    break;
                                case 4:
                                    Player2.arm[x.Item2].Add_Effect(Effect.Protection);
                                    Queue.Moving(0);
                                    break;
                                case 5:
                                    Console.WriteLine("Вторая команда сдалась");
                                    Console.WriteLine("Первая команда одержала победу");
                                    if (round == 1)
                                        Console.WriteLine($"Сражение длилось {round} ранд");
                                    if ((round > 1) || (round < 5))
                                        Console.WriteLine($"Сражение длилось {round} ранда");
                                    if (round > 4)
                                        Console.WriteLine($"Сражение длилось {round} рандов");
                                    Console.ReadKey();
                                    Environment.Exit(0);
                                    break;
                                case 6:
                                    Console.Clear();
                                    Console.WriteLine("Список юнитов первой команды:");
                                    Player1.Show();
                                    Console.WriteLine("Список юнитов второй команды:");
                                    Player2.Show();
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                default:
                                    Console.WriteLine("Ошибочный ввод");
                                    Console.ReadKey();
                                    break;
                            }
                        }

                        bool flag = false;
                        for (int i = 0; i < Queue.line.Count; i++)
                            if (Queue.line[i].move == false)
                                flag = true;
                        if (flag == false)
                            break;

                        if ((Player1.arm.Count == 0) || (Player2.arm.Count == 0))
                            break;
                    }


                    Console.WriteLine("Конец раунда");
                    Console.WriteLine("Действие отложенных заклинаний на первую армию:");

                    for (int i = 0; i < Player1.arm.Count; i++)
                        for (int j = 0; j < Player1.arm[i].effects.Count; j++)
                        {
                            if ((Player1.arm[i].effects[j].name == Effect.Poison) ||
                                (Player1.arm[i].effects[j].name == Effect.Hotly) ||
                                (Player1.arm[i].effects[j].name == Effect.Cold))
                            {
                                Console.WriteLine($"На юнита {Player1.arm[i].name} действует эффект {Player1.arm[i].effects[j].name} и отнимает 10 хп");
                                Player1.arm[i].hp = Player1.arm[i].hp - 10;
                                if (Player1.arm[i].hp == 0)
                                {
                                    Console.WriteLine("Юнит умер");
                                    Player1.Del(i);
                                }
                            }

                            Player1.arm[i].effects[j].Dec_Time();
                            if (Player1.arm[i].effects[j].time == 0)
                                Player1.arm[i].Del_Effect(Player1.arm[i].effects[j].name);
                        }

                    Console.WriteLine("Действие отложенных заклинаний на вторую армию:");

                    for (int i = 0; i < Player2.arm.Count; i++)
                        for (int j = 0; j < Player2.arm[i].effects.Count; j++)
                        {
                            if ((Player2.arm[i].effects[j].name == Effect.Poison) ||
                                (Player2.arm[i].effects[j].name == Effect.Hotly) ||
                                (Player2.arm[i].effects[j].name == Effect.Cold))
                            {
                                Console.WriteLine($"На юнита {Player2.arm[i].name} действует эффект {Player2.arm[i].effects[j].name} и отнимает 10 хп");
                                Player2.arm[i].hp = Player2.arm[i].hp - 10;
                                if (Player2.arm[i].hp == 0)
                                {
                                    Console.WriteLine("Юнит умер");
                                    Player2.Del(i);
                                }
                            }

                            Player2.arm[i].effects[j].Dec_Time();
                            if (Player2.arm[i].effects[j].time == 0)
                                Player2.arm[i].Del_Effect(Player2.arm[i].effects[j].name);
                        }

                    Console.ReadKey();
                    Console.Clear();

                    if ((Player1.arm.Count == 0) && (Player2.arm.Count == 0))
                    {
                        Console.WriteLine("Ничья");
                        Console.WriteLine("Обе армии уничтожены");

                        Console.ReadKey();
                        Console.Clear();

                        Environment.Exit(0);
                    }
                    else
                    if (Player1.arm.Count == 0)
                    {
                        Console.WriteLine("Победил второй игрок");
                        Console.WriteLine("Остаточная армия:");

                        Player2.Show();
                        Console.ReadKey();
                        Console.Clear();

                        Environment.Exit(0);
                    }
                    else
                    if (Player2.arm.Count == 0)
                    {
                        Console.WriteLine("Победил первый игрок");
                        Console.WriteLine("Остаточная армия:");

                        Player1.Show();
                        Console.ReadKey();
                        Console.Clear();

                        Environment.Exit(0);
                    }

                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Incorrect input");
            }
        }

        public void Manual()
        {
            Console.WriteLine($"{Spell.Punishing_blow} накладывает на цель эффект {Effect.Buff_Attack}, который увеличивает атаку цели на 12 единиц в течении 3 раундов");
            Console.WriteLine($"{Spell.Curse} накладывает на цель эффект {Effect.Debuff_Attack}, который уменьшает атаку цели на 12 единиц в течении 3 раундов");
            Console.WriteLine($"{Spell.Weakening} накладывает на цель эффект {Effect.Debuff_Defence}, который уменьшает защиту цели на 12 единиц в течении 3 раундов");
            Console.WriteLine($"{Spell.Acceleration} накладывает на цель эффект {Effect.Buff_Init}, который увеличивает инициативу цели на 40% в течении 1 раунда");
            Console.WriteLine($"{Spell.Resurrection} воскрешает 100 единиц здоровья за каждое колдующее существо");
            Console.WriteLine($"{Spell.Poisoning} накладывает на цель эффект {Effect.Poison}, который отнимает по 10 жизней в течении 5 раундов и временно уменьшает инициативу на 5");
            Console.WriteLine($"{Spell.Arcon} накладывает на цель эффект {Effect.Hotly}, который отнимает по 10 жизней в течении 5 раундов и временно уменьшает защиту на 5");
            Console.WriteLine($"{Spell.Freezing} накладывает на цель эффект {Effect.Cold}, который отнимает по 10 жизней в течении 5 раундов и временно уменьшает атаку на 5");
            Console.WriteLine($"{Spell.Stun} накладывает на цель эффект {Effect.Stupor}, бездействие в течении 3 раундов");
            Console.WriteLine($"{Spell.Persuasion} переманивает противника на свою сторону, если команда не заполнена");
            Console.WriteLine($"{Spell.Suicide} жертвует собой, нанося урон, пропорциональный имеющимся жизням");
            Console.WriteLine($"{Spell.Treacherous} высасывание жизней у союзника по формуле (Кол-во юнитов) * 10");
            Console.WriteLine($"{Spell.Convenience} накладывает на себя {Effect.Naked}, который уменьшает броню до 0, при увеличенном уроне на 10 в течении 3 раундов");
            Console.WriteLine($"{Spell.Invisibility} накладывает на себя {Effect.Invisible_man} - юнит не может атаковать и быть атакованным в течении 3 раундов");
            Console.WriteLine($"{Spell.Call} призывает в свою команду духа, если команда не заполнена");
        }

    }
}
