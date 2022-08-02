using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_v2
{
    class My_Queue
    {
        private List<Zhdun> Line= new List<Zhdun>();

        public IList<Zhdun> line { get { return Line.ToArray(); } }

        public My_Queue() 
        { }

        public void Add(Zhdun x)
        {
            Line.Add(x);
        }

        public void Del(int index)
        {
            Line.RemoveAt(index);
        }

        public void Change_Init(int index, double Init)
        {
            if (Line[index].wait || Line[index].move)
                Line[index].init = Init;
            else
                if (Init >= Line[index].init)
                {
                    Line[index].init = Init;

                    for (int i = index; i > 0; i--)
                        if (Line[i].init > Line[i - 1].init)
                        {
                            Zhdun x = Line[i];
                            Line[i] = Line[i - 1];
                            Line[i - 1] = x;
                        }
                        else
                            break;
                }
                else
                {
                    Line[index].init = Init;

                    for (int i = index; i < Line.Count - 1; i++)
                        if ((Line[i].init < Line[i + 1].init) && (Line[i + 1].wait != true) && (Line[i + 1].move != true))
                        {
                            Zhdun x = Line[i];
                            Line[i] = Line[i + 1];
                            Line[i + 1] = x;
                        }
                        else
                            break;
                }
        }

        public void Waiting(int index)
        {
            Line[index].wait = true;
            for (int i = index; i < Line.Count - 1; i++)
                if ((Line[i + 1].wait != true) && (Line[i + 1].move != true))
                {
                    Zhdun x = Line[i];
                    Line[i] = Line[i + 1];
                    Line[i + 1] = x;
                }
                else
                    break;
        }

        public void Moving(int index)
        {
            Line[index].move = true;
            for (int i = index; i < Line.Count - 1; i++)
            {
                Zhdun x = Line[i];
                Line[i] = Line[i + 1];
                Line[i + 1] = x;
            }
        }

        public void Revenge(int index, bool Flag)
        {
            Line[index].revenge = Flag;
        }

        public bool Revenge(int index)
        {
            return Line[index].revenge;
        }

        public void Chenge_Player(int index, int Player)
        {
            Line[index].player = Player;
        }

        public void Sort()
        {
            for (int i = 0; i < Line.Count - 1; i++)
                for (int j = i + 1; j < Line.Count; j++)
                    if (Line[i].init < Line[j].init)
                    {
                        Zhdun x = Line[i];
                        Line[i] = Line[j];
                        Line[j] = x;
                    }
        }

        public void Clear()
        {
            Line.Clear();
        }

        public int Convert_Global_Index_to_Local_Index(int Global_Index)
        {
            for (int i = 0; i < Line.Count; i++)
                if (Line[i].global_index == Global_Index)
                    return i;

            return -1;
        }
    }
}
