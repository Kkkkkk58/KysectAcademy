using System;

namespace Laba
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Army Ar1 = new Army();
                Army Ar2 = new Army();
                Battle Btl;
                start:
                BattleArmy BArmy1 = new BattleArmy();
                BattleArmy BArmy2 = new BattleArmy();

                while (true)
                {
                    Console.WriteLine("Выберите действие");
                    Console.WriteLine("1. Создать армию №1");
                    Console.WriteLine("2. Создать BattleArmy из армии №1");
                    Console.WriteLine("3. Создать армию №2");
                    Console.WriteLine("4. Создать BattleArmy из армии №2");
                    Console.WriteLine("5. Начало боя");
                    Console.WriteLine("6. Выход");
                    int z = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    if (z == 1)
                    {
                        while (true)
                        {
                            Console.WriteLine("Выберите юнита и его количество");
                            Console.WriteLine("1. Ангел");
                            Console.WriteLine("2. Арбалетчик");
                            Console.WriteLine("3. Костяной дракон");
                            Console.WriteLine("4. Циклоп");
                            Console.WriteLine("5. Дьявол");
                            Console.WriteLine("6. Фурия");
                            Console.WriteLine("7. Грифон");
                            Console.WriteLine("8. Гидра"); //Say no to drugs
                            Console.WriteLine("9. Лич");
                            Console.WriteLine("10. Шаман");
                            Console.WriteLine("11. Скелет");
                            Console.WriteLine("Enter 0 0 to exit");
                            int u = Convert.ToInt32(Console.ReadLine());
                            int a = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            if (u == 1)
                            {
                                Ar1.Add(new UnitsStack(new Angel(), a));
                            }
                            else if (u == 2)
                            {
                                Ar1.Add(new UnitsStack(new Arbalester(), a));
                            }
                            else if (u == 3)
                            {
                                Ar1.Add(new UnitsStack(new BoneDragon(), a));
                            }
                            else if (u == 4)
                            {
                                Ar1.Add(new UnitsStack(new Cyclop(), a));
                            }
                            else if (u == 5)
                            {
                                Ar1.Add(new UnitsStack(new Devil(), a));
                            }
                            else if (u == 6)
                            {
                                Ar1.Add(new UnitsStack(new Fury(), a));
                            }
                            else if (u == 7)
                            {
                                Ar1.Add(new UnitsStack(new Griffin(), a));
                            }
                            else if (u == 8)
                            {
                                Ar1.Add(new UnitsStack(new Hydra(), a));
                            }
                            else if (u == 9)
                            {
                                Ar1.Add(new UnitsStack(new Lich(), a));
                            }
                            else if (u == 10)
                            {
                                Ar1.Add(new UnitsStack(new Shaman(), a));
                            }
                            else if (u == 11)
                            {
                                Ar1.Add(new UnitsStack(new Skeleton(), a));
                            }
                            else if (u == 0)
                            {
                                break;
                            }
                        }
                    }
                    else if (z == 2)
                    {
                        BArmy1 = new BattleArmy(Ar1);
                    }
                    else if (z == 3)
                    {
                        while (true)
                        {
                            Console.WriteLine("Выберите юнита и его количество");
                            Console.WriteLine("1. Ангел");
                            Console.WriteLine("2. Арбалетчик");
                            Console.WriteLine("3. Костяной дракон");
                            Console.WriteLine("4. Циклоп");
                            Console.WriteLine("5. Дьявол");
                            Console.WriteLine("6. Фурия");
                            Console.WriteLine("7. Грифон");
                            Console.WriteLine("8. Гидра"); //Say no to drugs
                            Console.WriteLine("9. Лич");
                            Console.WriteLine("10. Шаман");
                            Console.WriteLine("11. Скелет");
                            Console.WriteLine("Enter 0 0 to exit");
                            int u = Convert.ToInt32(Console.ReadLine());
                            int a = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            if (u == 1)
                            {
                                Ar2.Add(new UnitsStack(new Angel(), a));
                            }
                            else if (u == 2)
                            {
                                Ar2.Add(new UnitsStack(new Arbalester(), a));
                            }
                            else if (u == 3)
                            {
                                Ar2.Add(new UnitsStack(new BoneDragon(), a));
                            }
                            else if (u == 4)
                            {
                                Ar2.Add(new UnitsStack(new Cyclop(), a));
                            }
                            else if (u == 5)
                            {
                                Ar2.Add(new UnitsStack(new Devil(), a));
                            }
                            else if (u == 6)
                            {
                                Ar2.Add(new UnitsStack(new Fury(), a));
                            }
                            else if (u == 7)
                            {
                                Ar2.Add(new UnitsStack(new Griffin(), a));
                            }
                            else if (u == 8)
                            {
                                Ar2.Add(new UnitsStack(new Hydra(), a));
                            }
                            else if (u == 9)
                            {
                                Ar2.Add(new UnitsStack(new Lich(), a));
                            }
                            else if (u == 10)
                            {
                                Ar2.Add(new UnitsStack(new Shaman(), a));
                            }
                            else if (u == 11)
                            {
                                Ar2.Add(new UnitsStack(new Skeleton(), a));
                            }
                            else if (u == 0)
                            {
                                break;
                            }
                        }
                    }
                    else if (z == 4)
                    {
                        BArmy2 = new BattleArmy(Ar2);
                    }
                    else if (z == 5)
                    {
                        Btl = new Battle(BArmy1, BArmy2);

                        while (true)
                        {
                            Btl.CrQ();

                            while (true)
                            {
                                for (int i = 0; i < Btl.queue.Count; i++)
                                    Console.WriteLine($"{Btl.queue[i].Item1.name} {Btl.queue[i].Item1.init} {Btl.queue[i].Item1.status} {Btl.queue[i].Item2} ");

                                bool flag = false;

                                for (int i = 0; i < Btl.queue.Count; i++)
                                    if (Btl.queue[i].A.ctive)
                                        flag = true;

                                if (flag == false)
                                    goto end;
                                
                                if (Btl.queue[0].BattleUS.hp == 0)
                                {
                                    Btl.queue[0].BattleUS.status = 2;
                                    Btl.queue[0].A.ctive = false;
                                    Btl.Sort1(0);
                                    continue;
                                }

                                if (Btl.queue[0].Player == 1)
                                    Console.WriteLine("Ходит первый игрок");
                                else
                                    Console.WriteLine("Ходит второй игрок");

                                Console.WriteLine($"Юнит {Btl.queue[0].BattleUS.name} ждет приказа");

                                Console.WriteLine("1.Атаковать");
                                Console.WriteLine("2.Использовать заклинание/способность");
                                Console.WriteLine("3.Ожидать");
                                Console.WriteLine("4.Обороняться");
                                Console.WriteLine("5.Сдаться");
                                Console.WriteLine("6.Статистика");
                                Console.WriteLine("7.Очередь следующего раунда");

                                int r = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();

                                if (r == 1)
                                {
                                    Console.WriteLine("Выберите цель");
                                    int p = Btl.queue[0].Item2;
                                    if (p == 1)
                                    {
                                        Console.WriteLine();
                                        Btl.player2.Show();
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Btl.player1.Show();
                                    }
                                    int u = Convert.ToInt32(Console.ReadLine());
                                    Console.Clear();
                                    Btl.Attack(p, u, Btl.queue[0].GlobInd);
                                }
                                else if (r == 2)
                                {
                                    if (Btl.queue[0].BattleUS.name == "Angel")
                                    {
                                        Console.WriteLine("Доступные способности");
                                        Console.WriteLine("1.Карающий удар");
                                        int w = Convert.ToInt32(Console.ReadLine());
                                        Console.Clear();

                                        if (w == 1)
                                        {
                                            Console.WriteLine("Выберите цель");
                                            int p = Btl.queue[0].Item2;
                                            if (p == 1)
                                            {
                                                Console.WriteLine();
                                                Btl.player1.Show();
                                            }
                                            else
                                            {
                                                Console.WriteLine();
                                                Btl.player2.Show();
                                            }
                                            int u = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();
                                            Btl.Spell(p, u, Btl.queue[0].GlobInd, 1, true);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Incorrect input");
                                        }
                                    }
                                    else if (Btl.queue[0].BattleUS.name == "Arbalester")
                                    {
                                        Console.WriteLine("Доступные способности");
                                        Console.ReadKey();
                                    }
                                    else if (Btl.queue[0].BattleUS.name == "BoneDragon")
                                    {
                                        Console.WriteLine("Доступные способности");
                                        Console.WriteLine("1.Проклятие");
                                        int w = Convert.ToInt32(Console.ReadLine());
                                        Console.Clear();

                                        if (w == 1)
                                        {
                                            Console.WriteLine("Выберите цель");
                                            int p = Btl.queue[0].Item2;
                                            if (p == 1)
                                            {
                                                Console.WriteLine();
                                                Btl.player2.Show();
                                            }
                                            else
                                            {
                                                Console.WriteLine();
                                                Btl.player1.Show();
                                            }
                                            int u = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();
                                            Btl.Spell(p, u, Btl.queue[0].GlobInd, 2, false);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Incorrect input");
                                        }
                                    }
                                    else if (Btl.queue[0].BattleUS.name == "Cyclop")
                                    {
                                        Console.WriteLine("Доступные способности");
                                        Console.ReadKey();
                                    }
                                    else if (Btl.queue[0].BattleUS.name == "Devil")
                                    {
                                        Console.WriteLine("Доступные способности");
                                        Console.WriteLine("1.Ослабление");
                                        int w = Convert.ToInt32(Console.ReadLine());
                                        Console.Clear();

                                        if (w == 1)
                                        {
                                            Console.WriteLine("Выберите цель");
                                            int p = Btl.queue[0].Item2;
                                            if (p == 1)
                                            {
                                                Console.WriteLine();
                                                Btl.player2.Show();
                                            }
                                            else
                                            {
                                                Console.WriteLine();
                                                Btl.player1.Show();
                                            }
                                            int u = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();
                                            Btl.Spell(p, u, Btl.queue[0].GlobInd, 3, false);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Incorrect input");
                                        }
                                    }
                                    else if (Btl.queue[0].BattleUS.name == "Fury")
                                    {
                                        Console.WriteLine("Доступные способности");
                                        Console.ReadKey();
                                    }
                                    else if (Btl.queue[0].BattleUS.name == "Griffin")
                                    {
                                        Console.WriteLine("Доступные способности");
                                        Console.ReadKey();
                                    }
                                    else if (Btl.queue[0].BattleUS.name == "Hydra")
                                    {
                                        Console.WriteLine("Доступные способности");
                                        Console.ReadKey();
                                    }
                                    else if (Btl.queue[0].BattleUS.name == "Lich")
                                    {
                                        Console.WriteLine("Доступные способности");
                                        Console.WriteLine("1.Воскрешение");
                                        Console.WriteLine("2.Отравление");
                                        int w = Convert.ToInt32(Console.ReadLine());
                                        Console.Clear();

                                        if (w == 1)
                                        {
                                            Goto_DedInside_1:
                                            Console.WriteLine("Выберите цель");
                                            int p = Btl.queue[0].Item2;
                                            if (p == 1)
                                            {
                                                Console.WriteLine();
                                                Btl.player1.Show();
                                            }
                                            else
                                            {
                                                Console.WriteLine();
                                                Btl.player2.Show();
                                            }
                                            int u = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();
                                            if (p == 1)
                                                if (Btl.player1.arm[u].BattleUS.dedinside == false)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Цель не является нежитью");
                                                    Console.ReadKey();
                                                    Console.Clear();
                                                    goto Goto_DedInside_1;
                                                }else;
                                            else
                                                if (Btl.player2.arm[u].BattleUS.dedinside == false)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Цель не является нежитью");
                                                Console.ReadKey();
                                                Console.Clear();
                                                goto Goto_DedInside_1;
                                            }
                                            
                                            Btl.Spell(p, u, Btl.queue[0].GlobInd, 5, true);
                                        }
                                        else if (w == 2)
                                        {
                                            Console.WriteLine("Выберите цель");
                                            int p = Btl.queue[0].Item2;
                                            if (p == 1)
                                            {
                                                Console.WriteLine();
                                                Btl.player2.Show();
                                            }
                                            else
                                            {
                                                Console.WriteLine();
                                                Btl.player1.Show();
                                            }
                                            int u = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();
                                            
                                            Btl.Spell(p, u, Btl.queue[0].GlobInd, 7, false);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Incorrect input");
                                        }
                                    }
                                    else if (Btl.queue[0].BattleUS.name == "Shaman")
                                    {
                                        Console.WriteLine("Доступные способности");
                                        Console.WriteLine("1.Ускорение");
                                        int w = Convert.ToInt32(Console.ReadLine());
                                        Console.Clear();

                                        if (w == 1)
                                        {
                                            Console.WriteLine("Выберите цель");
                                            int p = Btl.queue[0].Item2;
                                            if (p == 1)
                                            {
                                                Console.WriteLine();
                                                Btl.player1.Show();
                                            }
                                            else
                                            {
                                                Console.WriteLine();
                                                Btl.player2.Show();
                                            }
                                            int u = Convert.ToInt32(Console.ReadLine());
                                            Console.Clear();
                                            Btl.Spell(p, u, Btl.queue[0].GlobInd, 4, true);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Incorrect input");
                                        }
                                    }
                                    else if (Btl.queue[0].BattleUS.name == "Skeleton")
                                    {
                                        Console.WriteLine("Доступные способности");
                                        Console.ReadKey();
                                    }

                                }
                                else if (r == 3)
                                {
                                    Btl.Wait();
                                }
                                else if (r == 4)
                                {
                                    Btl.Defend();
                                }
                                else if (r == 5)
                                {
                                    Console.WriteLine($"{Btl.queue[0].Player} игрок сдался");
                                    Console.ReadKey();
                                    Console.Clear();

                                    for (int i = 0; i < Ar1.arm.Count; i++)
                                    {
                                        for (int j = 0; j < BArmy1.arm.Count; j++)
                                        {
                                            if (Ar1.arm[i].heroes.name == BArmy1.arm[j].BattleUS.name)
                                            {
                                                if (BArmy1.arm[j].BattleUS.status == 2)
                                                    Ar1.Del(Ar1.arm[i]);
                                                break;
                                            }
                                        }
                                    }

                                    for (int i = 0; i < Ar2.arm.Count; i++)
                                    {
                                        for (int j = 0; j < BArmy2.arm.Count; j++)
                                        {
                                            if (Ar2.arm[i].heroes.name == BArmy2.arm[j].BattleUS.name)
                                            {
                                                if (BArmy2.arm[j].BattleUS.status == 2)
                                                    Ar2.Del(Ar2.arm[i]);
                                                break;
                                            }
                                        }
                                    }
                                    goto start;
                                }
                                else if (r == 6)
                                {
                                    Btl.ShowStats();
                                }
                                else if (r == 7)
                                {
                                    Battle NextQ = Btl;
                                    NextQ.CrQ();
                                    for (int i = 0; i < NextQ.queue.Count; i++)
                                    {
                                        for (int j = 0; j < NextQ.player1.arm.Count; j++)
                                        {
                                            if (NextQ.queue[i].GlobInd == NextQ.player1.arm[j].GlobInd)
                                                if (NextQ.player1.arm[j].BattleUS.hp > 0)
                                                    Console.WriteLine(NextQ.player1.arm[j].BattleUS.name);
                                        }
                                        for (int j = 0; j < NextQ.player2.arm.Count; j++)
                                        {
                                            if (NextQ.queue[i].GlobInd == NextQ.player2.arm[j].GlobInd)
                                                if (NextQ.player2.arm[j].BattleUS.hp > 0)
                                                    Console.WriteLine(NextQ.player2.arm[j].BattleUS.name);
                                        }
                                    }
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect input");
                                }
                            }
                            end:

                            Console.Clear();
                            Console.WriteLine("Рауд окончен");
                            Console.WriteLine("Действие отложенных заклинаний на первую армию:");
                            for (int i = 0; i < Btl.player1.arm.Count; i++)
                                for (int j = 0; j < Btl.player1.arm[i].BattleUS.buff.Count; j++)
                                    if (Btl.player1.arm[i].BattleUS.buff[j].t.ime != -1)
                                    {
                                        Btl.player1.arm[i].BattleUS.buff[j].t.ime--;

                                        if (Btl.player1.arm[i].BattleUS.buff[j].t.ime == 0)
                                            Btl.player1.arm[i].BattleUS.Del_Buff(j);
                                        else
                                            Btl.player1.arm[i].BattleUS.Check(j);
                                    }
                            Console.WriteLine("Действие отложенных заклинаний на вторую армию:");
                            for (int i = 0; i < Btl.player2.arm.Count; i++)
                                for (int j = 0; j < Btl.player2.arm[i].BattleUS.buff.Count; j++)
                                    if (Btl.player2.arm[i].BattleUS.buff[j].t.ime != -1)
                                    {
                                        Btl.player2.arm[i].BattleUS.buff[j].t.ime--;

                                        if (Btl.player2.arm[i].BattleUS.buff[j].t.ime == 0)
                                            Btl.player2.arm[i].BattleUS.Del_Buff(j);
                                        else
                                            Btl.player2.arm[i].BattleUS.Check(j);
                                    }

                            bool flag1 = false;
                            bool flag2 = false;

                            for (int i = 0; i < Btl.player1.arm.Count; i++)
                                if (Btl.player1.arm[i].BattleUS.hp != 0)
                                    flag1 = true;

                            for (int i = 0; i < Btl.player2.arm.Count; i++)
                                if (Btl.player2.arm[i].BattleUS.hp != 0)
                                    flag2 = true;

                            if ((flag1 == false) && (flag2 == false))
                            {
                                Console.WriteLine("Ничья");
                                Console.ReadKey();
                                Console.Clear();

                                Ar1 = new Army();
                                Ar2 = new Army();
                                goto start;
                            }
                            else
                            if (flag1 == false)
                            {
                                Console.WriteLine("Победил второй игрок");
                                Console.ReadKey();
                                Console.Clear();
                                Ar1 = new Army();
                                for (int i = 0; i < Ar2.arm.Count; i++)
                                {
                                    for (int j = 0; j < BArmy2.arm.Count; j++)
                                    {
                                        if (Ar2.arm[i].heroes.name == BArmy2.arm[j].BattleUS.name)
                                        {
                                            if (BArmy2.arm[j].BattleUS.status == 2)
                                                Ar2.Del(Ar2.arm[i]);
                                            break;
                                        }
                                    }
                                }
                                goto start;
                            }
                            else
                            if (flag2 == false)
                            {
                                Console.WriteLine("Победил первый игрок");
                                Console.ReadKey();
                                Console.Clear();
                                for (int i = 0; i < Ar1.arm.Count; i++)
                                {
                                    for (int j = 0; j < BArmy1.arm.Count; j++)
                                    {
                                        if (Ar1.arm[i].heroes.name == BArmy1.arm[j].BattleUS.name)
                                        {
                                            if (BArmy1.arm[j].BattleUS.status == 2)
                                                Ar1.Del(Ar1.arm[i]);
                                            break;
                                        }
                                    }
                                }
                                Ar2 = new Army();
                                goto start;
                            }


                            Console.ReadKey();
                            Console.Clear();
                        }

                    }
                    else if (z == 6)
                    {
                        Environment.Exit(0);
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Incorrect input");
            }
        }
    }
}
