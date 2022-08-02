namespace Lab2 {
    public class Attack : Ability {
        public override double UseAbility(BattleUnitStack attacking, BattleUnitStack defending) {
            double sumDamage;
            if (defending.PDefence >= attacking.PAttack) {
                sumDamage = attacking.PCount * attacking.PDmg * (1 + 0.05 * (attacking.PAttack - defending.PDefence));
            }
            else {
                sumDamage = attacking.PCount * attacking.PDmg / (1 + 0.05 * (defending.PDefence - attacking.PAttack));
            }
            //return damage deal
            //sumDamage = sumDamage;
            return sumDamage;
        }
    }
}