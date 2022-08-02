using System;
using System.Collections.Generic;
using System.Text;

namespace Laba
{
    public class Battle
    {
        private int round = 0;

        private BattleArmy Player1;
        public BattleArmy player1 { get { return Player1; } }

        private BattleArmy Player2;
        public BattleArmy player2 { get { return Player2; } }

        private List<(BattleUnitsStack BattleUS, int Player, int GlobInd, Activ A, Cntr C)> Queue = new List<(BattleUnitsStack, int, int, Activ, Cntr)>();
        public IList<(BattleUnitsStack BattleUS, int Player, int GlobInd, Activ A, Cntr C)> queue { get { return Queue.ToArray(); } }

        public Battle(BattleArmy player1, BattleArmy player2)
        {
            Player1 = new BattleArmy(player1);
            Player2 = new BattleArmy(player2);
        }

        public void CrQ()
        {
            Queue.Clear();

            for (int i = 0; i < player1.arm.Count; i++)
                if (player1.arm[i].BattleUS.status == 1)
                    Player1.arm[i].BattleUS.status = 0;

            for (int i = 0; i < player2.arm.Count; i++)
                if (player2.arm[i].BattleUS.status == 1)
                    Player2.arm[i].BattleUS.status = 0;

            for (int i = 0; i < player1.arm.Count; i++)
                Queue.Add((player1.arm[i].BattleUS, 1, player1.arm[i].GlobInd, new Activ(), new Cntr(false)));

            for (int i = 0; i < player2.arm.Count; i++)
                Queue.Add((player2.arm[i].BattleUS, 2, player2.arm[i].GlobInd, new Activ(), new Cntr(false)));

            ReQ();
        }

        public void ReQ()
        {
            Queue.Sort(new Comp());
        }

        public void Sort1(int x)
        {
            for (int i = x + 1; i < Queue.Count; i++)
            {
                (BattleUnitsStack, int, int, Activ, Cntr) t = Queue[i];
                Queue[i] = Queue[i - 1];
                Queue[i - 1] = t;
            }
        }

        public void Sort2(int x)
        {
            for (int i = x + 1; i < Queue.Count; i++)
                if (Queue[i].BattleUS.status != 1)
                {
                    (BattleUnitsStack, int, int, Activ, Cntr) t = Queue[i];
                    Queue[i] = Queue[i - 1];
                    Queue[i - 1] = t;
                }
                else
                    break;
        }

        public void Sort3(int x)
        {
            for (int i = x; i > 0; i--)
                if (Queue[i].BattleUS.init >= Queue[i - 1].BattleUS.init)
                {
                    (BattleUnitsStack, int, int, Activ, Cntr) t = Queue[i];
                    Queue[i] = Queue[i - 1];
                    Queue[i - 1] = t;
                }
                
        }

        public void Sort4(int x)
        {
            for (int i = x; i > 1; i--)
                if (Queue[i].BattleUS.init >= Queue[i - 1].BattleUS.init)
                {
                    (BattleUnitsStack, int, int, Activ, Cntr) t = Queue[i];
                    Queue[i] = Queue[i - 1];
                    Queue[i - 1] = t;
                }
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

        public void Attack(int p, int u, int globint)
        {
            int i;

            if (p == 1)
            {
                for (i = 0; i < Player1.arm.Count; i++)
                {
                    if (Player1.arm[i].GlobInd == globint)
                        break;
                }
                if (u > Player2.arm.Count - 1)
                {
                    Console.Clear();
                    Console.WriteLine("Выход за пределы списка");
                    Console.ReadKey();
                    return;
                }
                if (Player1.arm[i].BattleUS.name == new Hydra().name)
                {
                    for (u = 0; u < player2.arm.Count; u++)
                    {
                        if (player2.arm[u].BattleUS.defence < player1.arm[i].BattleUS.attack)
                            Player2.arm[u].BattleUS.hp = player2.arm[u].BattleUS.hp - player1.arm[i].BattleUS.count * player1.arm[i].BattleUS.damage * (1 + 0.05 * (player1.arm[i].BattleUS.attack - player2.arm[u].BattleUS.defence));
                        else
                            Player2.arm[u].BattleUS.hp = player2.arm[u].BattleUS.hp - player1.arm[i].BattleUS.count * player1.arm[i].BattleUS.damage / (1 + 0.05 * (player1.arm[i].BattleUS.attack - player2.arm[u].BattleUS.defence));
                    }
                }
                else
                {
                    if (player1.arm[i].BattleUS.name == new Arbalester().name)
                    {
                        Player2.arm[u].BattleUS.hp = player2.arm[u].BattleUS.hp - player1.arm[i].BattleUS.count * player1.arm[i].BattleUS.damage * (1 + 0.05 * player1.arm[i].BattleUS.attack);
                    }
                    else 
                    if (player2.arm[u].BattleUS.defence < player1.arm[i].BattleUS.attack)
                        Player2.arm[u].BattleUS.hp = player2.arm[u].BattleUS.hp - player1.arm[i].BattleUS.count * player1.arm[i].BattleUS.damage * (1 + 0.05 * (player1.arm[i].BattleUS.attack - player2.arm[u].BattleUS.defence));
                    else
                        Player2.arm[u].BattleUS.hp = player2.arm[u].BattleUS.hp - player1.arm[i].BattleUS.count * player1.arm[i].BattleUS.damage / (1 + 0.05 * (player1.arm[i].BattleUS.attack - player2.arm[u].BattleUS.defence));
                    int z;
                    for (z = 0; z < queue.Count; z++)
                        if (queue[z].GlobInd == player2.arm[u].GlobInd)
                            break;

                    if ((player1.arm[i].BattleUS.name != new Arbalester().name) 
                        && (player1.arm[i].BattleUS.name != new Lich().name) 
                        && (player1.arm[i].BattleUS.name != new Cyclop().name) 
                        && (player2.arm[u].BattleUS.name != new Arbalester().name) 
                        && (player2.arm[u].BattleUS.name != new Lich().name) 
                        && (player2.arm[u].BattleUS.name != new Cyclop().name)
                        && (player1.arm[i].BattleUS.name != new Fury().name) 
                        && ((Queue[z].C.onter == false) || (player2.arm[u].BattleUS.name == new Griffin().name)))
                    { 
                        if (player1.arm[i].BattleUS.defence < player2.arm[u].BattleUS.attack)
                            Player1.arm[i].BattleUS.hp = player1.arm[i].BattleUS.hp - player2.arm[u].BattleUS.count * player2.arm[u].BattleUS.damage * (1 + 0.05 * (player2.arm[u].BattleUS.attack - player1.arm[i].BattleUS.defence));
                        else
                            Player1.arm[i].BattleUS.hp = player1.arm[i].BattleUS.hp - player2.arm[u].BattleUS.count * player2.arm[u].BattleUS.damage / (1 + 0.05 * (player2.arm[u].BattleUS.attack - player1.arm[i].BattleUS.defence));
                        
                        Queue[z].C.onter = true;
                    }
                }
            }
            else
            {
                for (i = 0; i < Player2.arm.Count; i++)
                {
                    if (Player2.arm[i].GlobInd == globint)
                        break;
                }

                if (u > player1.arm.Count - 1)
                {
                    Console.Clear();
                    Console.WriteLine("Выход за пределы списка");
                    Console.ReadKey();
                    return;
                }

                if (Player2.arm[i].BattleUS.name == new Hydra().name)
                {
                    for (u = 0; u < player1.arm.Count; u++)
                    {
                        if (player1.arm[u].BattleUS.defence < player2.arm[i].BattleUS.attack)
                            Player1.arm[u].BattleUS.hp = player1.arm[u].BattleUS.hp - player2.arm[i].BattleUS.count * player2.arm[i].BattleUS.damage * (1 + 0.05 * (player2.arm[i].BattleUS.attack - player1.arm[u].BattleUS.defence));
                        else
                            Player1.arm[u].BattleUS.hp = player1.arm[u].BattleUS.hp - player2.arm[i].BattleUS.count * player2.arm[i].BattleUS.damage / (1 + 0.05 * (player2.arm[i].BattleUS.attack - player1.arm[u].BattleUS.defence));
                    }
                }
                else
                {
                    if (player2.arm[i].BattleUS.name == new Arbalester().name)
                    {
                        Player1.arm[u].BattleUS.hp = player1.arm[u].BattleUS.hp - player2.arm[i].BattleUS.count * player2.arm[i].BattleUS.damage * (1 + 0.05 * player2.arm[i].BattleUS.attack);
                    }
                    else
                    if (player1.arm[u].BattleUS.defence < player2.arm[i].BattleUS.attack)
                        Player1.arm[u].BattleUS.hp = player1.arm[u].BattleUS.hp - player2.arm[i].BattleUS.count * player2.arm[i].BattleUS.damage * (1 + 0.05 * (player2.arm[i].BattleUS.attack - player1.arm[u].BattleUS.defence));
                    else
                        Player1.arm[u].BattleUS.hp = player1.arm[u].BattleUS.hp - player2.arm[i].BattleUS.count * player2.arm[i].BattleUS.damage / (1 + 0.05 * (player2.arm[i].BattleUS.attack - player1.arm[u].BattleUS.defence));
                    int z;
                    for (z = 0; z < queue.Count; z++)
                        if (queue[z].GlobInd == player1.arm[u].GlobInd)
                            break;

                    if ((player2.arm[i].BattleUS.name != new Arbalester().name)
                        && (player2.arm[i].BattleUS.name != new Lich().name)
                        && (player2.arm[i].BattleUS.name != new Cyclop().name)
                        && (player1.arm[u].BattleUS.name != new Arbalester().name)
                        && (player1.arm[u].BattleUS.name != new Lich().name)
                        && (player1.arm[u].BattleUS.name != new Cyclop().name)
                        && (player2.arm[i].BattleUS.name != new Fury().name)
                        && ((Queue[z].C.onter == false) || (player1.arm[u].BattleUS.name == new Griffin().name)))
                    {
                        if (player2.arm[i].BattleUS.defence < player1.arm[u].BattleUS.attack)
                            Player2.arm[i].BattleUS.hp = player2.arm[i].BattleUS.hp - player1.arm[u].BattleUS.count * player1.arm[u].BattleUS.damage * (1 + 0.05 * (player1.arm[u].BattleUS.attack - player2.arm[i].BattleUS.defence));
                        else
                            Player2.arm[i].BattleUS.hp = player2.arm[i].BattleUS.hp - player1.arm[u].BattleUS.count * player1.arm[u].BattleUS.damage / (1 + 0.05 * (player1.arm[u].BattleUS.attack - player2.arm[i].BattleUS.defence));

                        Queue[z].C.onter = true;
                    }
                }
            }

            Queue[0].A.ctive = false;
            Sort1(0);
        }

        public void Wait()
        {
            Queue[0].BattleUS.status = 1;
            //Queue[0].A.ctive = false;
            Sort2(0);
        }

        public void Defend()
        {
            if (Queue[0].Player == 1)
            {
                int i;
                for (i = 0; i < Player1.arm.Count; i++)
                {
                    if (Player1.arm[i].GlobInd == Queue[0].GlobInd)
                        break;
                }

                Player1.arm[i].BattleUS.Add_Buff(6, Queue[0].BattleUS.count);
            }
            else
            {
                int i;
                for (i = 0; i < Player2.arm.Count; i++)
                {
                    if (Player2.arm[i].GlobInd == Queue[0].GlobInd)
                        break;
                }

                Player2.arm[i].BattleUS.Add_Buff(6, Queue[0].BattleUS.count);
            }

            Queue[0].A.ctive = false;
            Sort1(0);
        }

        public void Spell(int p, int u, int GInd, int spell_ind, bool flag)
        {
            if (spell_ind == 4)
            {
                int x;

                if (p == 1)
                    x = Player1.arm[u].GlobInd;
                else
                    x = Player2.arm[u].GlobInd;

                for (int i = 0; i < Queue.Count; i++)
                    if (Queue[i].GlobInd == x)
                    {
                        Queue[i].BattleUS.init *= 1.4;

                        if (Queue[i].A.ctive)
                            Sort4(i);

                        break;
                    }
            }

            if (flag)
            {
                if (p == 1)
                {
                    Player1.arm[u].BattleUS.Add_Buff(spell_ind, Queue[0].BattleUS.count);
                    Queue[0].A.ctive = false;
                    Sort1(0);
                }
                else
                {
                    Player2.arm[u].BattleUS.Add_Buff(spell_ind, Queue[0].BattleUS.count);
                    Queue[0].A.ctive = false;
                    Sort1(0);
                }
            }
            else
            {
                if (p == 1)
                {
                    Player2.arm[u].BattleUS.Add_Buff(spell_ind, Queue[0].BattleUS.count);
                    Queue[0].A.ctive = false;
                    Sort1(0);
                }
                else
                {
                    Player1.arm[u].BattleUS.Add_Buff(spell_ind, Queue[0].BattleUS.count);
                    Queue[0].A.ctive = false;
                    Sort1(0);
                }
            }
        }

    }
}
