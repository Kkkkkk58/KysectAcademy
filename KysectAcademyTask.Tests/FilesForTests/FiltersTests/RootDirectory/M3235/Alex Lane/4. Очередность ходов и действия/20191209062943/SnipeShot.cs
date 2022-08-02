namespace Lab2 {
    public class SnipeShot : Ability {
        public override double UseAbility(BattleUnitStack attacking, BattleUnitStack defending) {
            return 40;
        }
    }
}