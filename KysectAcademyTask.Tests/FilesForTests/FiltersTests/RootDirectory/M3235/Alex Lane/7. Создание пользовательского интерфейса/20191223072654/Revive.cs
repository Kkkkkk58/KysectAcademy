using System;

namespace Lab2 {
    public class Revive : Ability {
        public override double UseAbility(BattleUnitStack attacking, BattleUnitStack defending) {
            defending.PCount = Math.Max(Convert.ToInt32(1.3 * defending.PCount), defending.MaxCount);
            return 0;
        }
    }
}