using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_v2
{
    class Zhdun
    {
        private int Global_Index;
        private int Player;
        private double Init;
        private bool Wait;
        private bool Move;
        private bool Revenge;

        public int global_index
        {
            get
            {
                return Global_Index;
            }
        }

        public int player
        {
            get
            {
                return Player;
            }
            set
            {
                Player = value;
            }
        }

        public double init
        {
            get
            {
                return Init;
            }
            set
            {
                Init = Math.Max(value, 0);
            }
        }

        public bool wait
        {
            get
            {
                return Wait;
            }
            set
            {
                Wait = value;
            }
        }

        public bool move
        {
            get
            {
                return Move;
            }
            set
            {
                Move = value;
            }
        }

        public bool revenge
        {
            get
            { 
                return Revenge; 
            }
            set
            {
                Revenge = value;
            }
        }

        public Zhdun (int Global_Index, int Player, double Init, bool Wait, bool Move, bool Revenge)
        {
            this.Global_Index = Global_Index;
            this.Player = Player;
            this.Wait = Wait;
            this.Move = Move;
            this.Init = Init;
            this.Revenge = Revenge;
        }

        public Zhdun(int Global_Index, int Player, double Init)
        {
            this.Global_Index = Global_Index;
            this.Player = Player;
            this.Init = Init;
            Wait = false;
            Move = false;
            Revenge = false;
        }

        public Zhdun(Zhdun x)
        {
            Global_Index = x.global_index;
            Player = x.player;
            Init = x.init;
            Wait = x.wait;
            Move = x.move;
        }

    }

}
