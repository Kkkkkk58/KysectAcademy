using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_v2
{
    public enum Effect
    {
        // любой периодический урон наносится в конце раундов, а остальные эффекты во время накладывния
          Buff_Attack, // увеличение атаки цели на 12 на 3 раунда
          Debuff_Attack, // уменьшение атаки цели на 12 на 3 раунда
          Debuff_Defence, // уменьшение защиты на 12 на 3 раунда
          Buff_Init, // увеличение инициативы на 40% на 1 раунд
          Poison, // отнимание 10 жизней в течении 5 раундов и временное уменьшение инициативы на 5
          Hotly, // отнимание 10 жизней в течении 5 раундов и временное уменьшение защиты на 4
          Cold, // отнимание 10 жихней в течении 5 раундов и временное уменьшение атаки на 4
          Stupor, // бездействие в течении 3 раундов или пока юнита не атакуют
          Naked, // броня уменьшена до 0, при увеличенной атаке на 10 в течении 3 ходов 
          Invisible_man, // не может атаковать и быть атаковынным в течении 3 ходов
          Protection // получает +30% к защите и не может атаковать действует один раунд
    }

    public class Info_Effect
    {
        private Effect Name;
        private int Time;
        private double Value;

        public Effect name { get { return Name; } }

        public int time { get { return Time; } }

        public double value { get { return Value; } }

        public Info_Effect(Effect Name, int Time, double Value)
        {
            this.Name = Name;
            this.Time = Math.Max(Time, 0); // время не может быть отрицательно
            this.Value = Value;
        }

        public void Dec_Time()
        {
            Time = Math.Max(Time - 1, 0); // время не может быть отрицательно
        }
    }
}
